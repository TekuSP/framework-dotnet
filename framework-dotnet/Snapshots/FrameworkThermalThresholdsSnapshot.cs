using System.Globalization;

using FrameworkDotnet.Enums;

using UnitsNet;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents the thermal threshold configuration the embedded controller holds for one temperature
/// sensor.
/// </summary>
/// <remarks>
/// <para>
/// Embedded controller firmware stores thresholds in Kelvin with zero meaning "disabled", so a
/// disabled threshold arrives over the native ABI as -273 degrees Celsius. Never test a reported
/// temperature to decide whether a threshold is active: every threshold on this snapshot is
/// <see langword="null"/> exactly when its bit is clear in <see cref="EnabledThresholds"/>, which
/// is the only authoritative source.
/// </para>
/// <para>
/// <see cref="FanOff"/> and <see cref="FanMax"/> are temperature setpoints, not fan speeds.
/// </para>
/// </remarks>
public sealed record FrameworkThermalThresholdsSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkThermalThresholdsSnapshot"/> class.
    /// </summary>
    /// <param name="sensorIndex">The temperature sensor slot the thresholds belong to.</param>
    /// <param name="enabledThresholds">The set of thresholds the embedded controller currently has enabled.</param>
    /// <param name="warn">The warning threshold, or <see langword="null"/> when it is disabled.</param>
    /// <param name="high">The throttling threshold, or <see langword="null"/> when it is disabled.</param>
    /// <param name="halt">The shutdown threshold, or <see langword="null"/> when it is disabled.</param>
    /// <param name="warnRelease">The release point for <paramref name="warn"/>, or <see langword="null"/> when it is disabled.</param>
    /// <param name="highRelease">The release point for <paramref name="high"/>, or <see langword="null"/> when it is disabled.</param>
    /// <param name="haltRelease">The release point for <paramref name="halt"/>, or <see langword="null"/> when it is disabled.</param>
    /// <param name="fanOff">The temperature below which no active cooling is required, or <see langword="null"/> when it is disabled.</param>
    /// <param name="fanMax">The temperature above which active cooling runs at maximum, or <see langword="null"/> when it is disabled.</param>
    public FrameworkThermalThresholdsSnapshot(
        uint sensorIndex,
        FrameworkThermalThresholdFlag enabledThresholds,
        Temperature? warn,
        Temperature? high,
        Temperature? halt,
        Temperature? warnRelease,
        Temperature? highRelease,
        Temperature? haltRelease,
        Temperature? fanOff,
        Temperature? fanMax)
    {
        SensorIndex = sensorIndex;
        EnabledThresholds = enabledThresholds;
        Warn = warn;
        High = high;
        Halt = halt;
        WarnRelease = warnRelease;
        HighRelease = highRelease;
        HaltRelease = haltRelease;
        FanOff = fanOff;
        FanMax = fanMax;
    }

    /// <summary>
    /// Gets the temperature sensor slot these thresholds belong to. The index matches the
    /// temperature slot order of the thermal snapshot.
    /// </summary>
    public uint SensorIndex { get; init; }

    /// <summary>
    /// Gets the set of thresholds the embedded controller currently has enabled. A clear bit means
    /// firmware has that threshold disabled, and the matching property on this snapshot is
    /// <see langword="null"/>.
    /// </summary>
    public FrameworkThermalThresholdFlag EnabledThresholds { get; init; }

    /// <summary>
    /// Gets the temperature above which the embedded controller warns the application processor, or
    /// <see langword="null"/> when the warning threshold is disabled.
    /// </summary>
    public Temperature? Warn { get; init; }

    /// <summary>
    /// Gets the temperature above which the embedded controller throttles the application
    /// processor, or <see langword="null"/> when the throttling threshold is disabled.
    /// </summary>
    public Temperature? High { get; init; }

    /// <summary>
    /// Gets the temperature above which the embedded controller shuts the system down, or
    /// <see langword="null"/> when the shutdown threshold is disabled.
    /// </summary>
    public Temperature? Halt { get; init; }

    /// <summary>
    /// Gets the temperature at which the <see cref="Warn"/> condition is released, or
    /// <see langword="null"/> when that release point is disabled. Firmware treats a disabled
    /// release point as a default one-degree hysteresis below the threshold itself.
    /// </summary>
    public Temperature? WarnRelease { get; init; }

    /// <summary>
    /// Gets the temperature at which the <see cref="High"/> condition is released, or
    /// <see langword="null"/> when that release point is disabled.
    /// </summary>
    public Temperature? HighRelease { get; init; }

    /// <summary>
    /// Gets the temperature at which the <see cref="Halt"/> condition is released, or
    /// <see langword="null"/> when that release point is disabled.
    /// </summary>
    public Temperature? HaltRelease { get; init; }

    /// <summary>
    /// Gets the temperature below which the embedded controller needs no active cooling, or
    /// <see langword="null"/> when the setpoint is disabled. This is a temperature setpoint at
    /// which the fans stop, not a fan speed or an RPM limit.
    /// </summary>
    public Temperature? FanOff { get; init; }

    /// <summary>
    /// Gets the temperature above which the embedded controller applies maximum active cooling, or
    /// <see langword="null"/> when the setpoint is disabled. This is a temperature setpoint at
    /// which the fans reach full speed, not a fan speed or an RPM limit.
    /// </summary>
    public Temperature? FanMax { get; init; }

    /// <summary>
    /// Determines whether the embedded controller currently has a given threshold enabled.
    /// </summary>
    /// <param name="threshold">The threshold to test. Pass a single flag; passing a combination reports whether every flag in the combination is enabled.</param>
    /// <returns><see langword="true"/> when the threshold is enabled in firmware; otherwise, <see langword="false"/>.</returns>
    public bool IsEnabled(FrameworkThermalThresholdFlag threshold)
    {
        return (EnabledThresholds & threshold) == threshold;
    }

    /// <summary>
    /// Returns a readable description of the thresholds.
    /// </summary>
    /// <returns>A readable description of the thresholds.</returns>
    public override string ToString()
    {
        return $"Thermal Thresholds Snapshot: Sensor Index: {SensorIndex.ToString(CultureInfo.InvariantCulture)}, Enabled: {EnabledThresholds}, Warn: {Describe(Warn)}, High: {Describe(High)}, Halt: {Describe(Halt)}, Warn Release: {Describe(WarnRelease)}, High Release: {Describe(HighRelease)}, Halt Release: {Describe(HaltRelease)}, Fan Off: {Describe(FanOff)}, Fan Max: {Describe(FanMax)}";
    }

    private static string Describe(Temperature? temperature)
    {
        return temperature.HasValue
            ? temperature.Value.ToString(CultureInfo.InvariantCulture)
            : "Disabled";
    }
}
