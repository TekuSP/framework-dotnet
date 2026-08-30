using System;

using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Requests;
using FrameworkDotnet.Snapshots;

namespace FrameworkDotnet.Interfaces;

/// <summary>
/// Defines the embedded controller thermal control surface: per-sensor threshold configuration,
/// sensor identity, and the authoritative fan count.
/// </summary>
/// <remarks>
/// <para>
/// This surface configures how the embedded controller reacts to temperature. Live temperature and
/// fan readings come from the thermal snapshot on the owning connection, which is the surface
/// intended for polling.
/// </para>
/// <para>
/// A disabled threshold reads back from firmware as -273 degrees Celsius, so every threshold on
/// <see cref="FrameworkThermalThresholdsSnapshot"/> is <see langword="null"/> exactly when its bit
/// is clear in the snapshot's enabled mask. Never infer "disabled" from a temperature value.
/// </para>
/// </remarks>
public interface IFrameworkEcThermalControl
{
    /// <summary>
    /// Gets the thermal threshold configuration the embedded controller holds for one temperature
    /// sensor.
    /// </summary>
    /// <param name="sensorIndex">The temperature sensor slot to read, matching the temperature slot order of the thermal snapshot.</param>
    /// <returns>The thresholds for the requested sensor. Every threshold is <see langword="null"/> when its bit is clear in the returned enabled mask.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the owning connection has been disposed.</exception>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library reports a failure, for example when the sensor index is out of range for the platform.</exception>
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkThermalThresholdsSnapshot GetThresholds(byte sensorIndex);

    /// <summary>
    /// Writes thermal thresholds for one temperature sensor.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The write is a read-modify-write against firmware: any threshold left at its default
    /// <see cref="FrameworkThermalThresholdSetting.KeepCurrent"/> value is preserved exactly as it
    /// is, and so are the three release points, which this call never touches. Calling the method
    /// with no threshold arguments is therefore a no-op host command.
    /// </para>
    /// <para>
    /// Each threshold argument carries one of three unambiguous intents:
    /// <see cref="FrameworkThermalThresholdSetting.KeepCurrent"/> leaves it untouched,
    /// <see cref="FrameworkThermalThresholdSetting.Disable"/> turns it off, and
    /// <see cref="FrameworkThermalThresholdSetting.FromTemperature"/> (or an implicitly converted
    /// temperature) enables it at that temperature. A set temperature must round to at least one
    /// degree Celsius, because the native ABI reserves zero for "disable" and negative values for
    /// "keep current".
    /// </para>
    /// <para>
    /// Raising or disabling <paramref name="high"/> and <paramref name="halt"/> removes thermal
    /// protection the embedded controller would otherwise apply. Treat those two as safety-critical.
    /// </para>
    /// </remarks>
    /// <param name="sensorIndex">The temperature sensor slot to write, matching the temperature slot order of the thermal snapshot.</param>
    /// <param name="warn">The temperature above which the embedded controller warns the application processor. Defaults to keeping the current value.</param>
    /// <param name="high">The temperature above which the embedded controller throttles the application processor. Defaults to keeping the current value.</param>
    /// <param name="halt">The temperature above which the embedded controller shuts the system down. Defaults to keeping the current value.</param>
    /// <param name="fanOff">The temperature setpoint below which no active cooling is required, so the fans stop. This is a temperature, not a fan speed or an RPM limit. Defaults to keeping the current value.</param>
    /// <param name="fanMax">The temperature setpoint above which active cooling runs at maximum, so the fans reach full speed. This is a temperature, not a fan speed or an RPM limit. Defaults to keeping the current value.</param>
    /// <exception cref="ObjectDisposedException">Thrown when the owning connection has been disposed.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when a threshold requests a temperature that is not finite or that does not round to at least one degree Celsius.</exception>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library reports a failure, for example when the sensor index is out of range for the platform.</exception>
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    void SetThresholds(
        byte sensorIndex,
        FrameworkThermalThresholdSetting warn = default,
        FrameworkThermalThresholdSetting high = default,
        FrameworkThermalThresholdSetting halt = default,
        FrameworkThermalThresholdSetting fanOff = default,
        FrameworkThermalThresholdSetting fanMax = default);

    /// <summary>
    /// Gets the identity of one temperature sensor slot: the raw firmware name, the managed sensor
    /// role it maps onto, and the embedded controller's classification tag.
    /// </summary>
    /// <remarks>
    /// Each distinct sensor index costs at most one host command after the first successful read,
    /// after which the answer is cached for the lifetime of this instance. Sensor names do not change while the system is running,
    /// so read them once per session and keep polling the thermal snapshot on the owning connection
    /// for live values. Use <see cref="ClearSensorNameCache"/> if a cached answer must be discarded.
    /// </remarks>
    /// <param name="sensorIndex">The temperature sensor slot to identify, matching the temperature slot order of the thermal snapshot.</param>
    /// <returns>The identity of the requested sensor slot.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the owning connection has been disposed.</exception>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library reports a failure, for example when the sensor index is out of range for the platform.</exception>
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkTemperatureSensorNameSnapshot GetSensorName(byte sensorIndex);

    /// <summary>
    /// Discards every cached temperature sensor name, so that the next
    /// <see cref="GetSensorName(byte)"/> for each slot issues a fresh host command.
    /// </summary>
    /// <remarks>
    /// Sensor names are stable while the system is running, so this is only needed after the
    /// embedded controller has been reflashed or reset underneath the process.
    /// </remarks>
    void ClearSensorNameCache();

    /// <summary>
    /// Gets the number of fans the embedded controller reports.
    /// </summary>
    /// <remarks>
    /// This is more authoritative than the fan count carried on the thermal snapshot and the fan
    /// capabilities snapshot, both of which infer fan presence from a memory-map sentinel value.
    /// Prefer this count when deciding how many fan slots are real; the two agree on healthy
    /// hardware.
    /// </remarks>
    /// <returns>The number of fans reported by the embedded controller.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the owning connection has been disposed.</exception>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library reports a failure.</exception>
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    byte GetFanCount();
}
