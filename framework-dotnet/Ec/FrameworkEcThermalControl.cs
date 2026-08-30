using System;
using System.Collections.Concurrent;

using FrameworkDotnet.Enums;
using FrameworkDotnet.Interfaces;
using FrameworkDotnet.Requests;
using FrameworkDotnet.Snapshots;

using Native = Framework.System.Interop;

namespace FrameworkDotnet.Ec;

/// <summary>
/// Implements the embedded controller thermal control surface on top of a live EC handle.
/// </summary>
/// <remarks>
/// The instance does not own the embedded controller handle. It borrows it through the accessor
/// supplied to the constructor, so the owning connection stays responsible for the handle's
/// lifetime and for rejecting use after disposal.
/// </remarks>
internal sealed class FrameworkEcThermalControl : IFrameworkEcThermalControl
{
    /// <summary>
    /// The native argument that tells the embedded controller to keep a threshold unchanged. Any
    /// negative value works; -1 is used for readability.
    /// </summary>
    private const int KeepCurrentArgument = -1;

    /// <summary>
    /// The native argument that tells the embedded controller to disable a threshold.
    /// </summary>
    private const int DisableArgument = 0;

    private readonly Func<IntPtr> handleAccessor;

    private readonly ConcurrentDictionary<byte, Lazy<FrameworkTemperatureSensorNameSnapshot>> sensorNameCache = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkEcThermalControl"/> class.
    /// </summary>
    /// <param name="handleAccessor">
    /// Returns the owning connection's native embedded controller handle as an
    /// <see cref="IntPtr"/>. It is invoked once per host command rather than captured, so the
    /// owning connection can validate its own state on every call; it is expected to throw
    /// <see cref="ObjectDisposedException"/> once the connection is closed or disposed.
    /// </param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="handleAccessor"/> is <see langword="null"/>.</exception>
    internal FrameworkEcThermalControl(Func<IntPtr> handleAccessor)
    {
        ArgumentNullException.ThrowIfNull(handleAccessor);

        this.handleAccessor = handleAccessor;
    }

    /// <inheritdoc/>
    public FrameworkThermalThresholdsSnapshot GetThresholds(byte sensorIndex)
    {
        unsafe
        {
            return Native.NativeMethods.framework_ec_get_thermal_thresholds(HandlePointer, sensorIndex).GetValueOrThrow();
        }
    }

    /// <inheritdoc/>
    public void SetThresholds(
        byte sensorIndex,
        FrameworkThermalThresholdSetting warn = default,
        FrameworkThermalThresholdSetting high = default,
        FrameworkThermalThresholdSetting halt = default,
        FrameworkThermalThresholdSetting fanOff = default,
        FrameworkThermalThresholdSetting fanMax = default)
    {
        int warnArgument = ToNativeArgument(warn, nameof(warn));
        int highArgument = ToNativeArgument(high, nameof(high));
        int haltArgument = ToNativeArgument(halt, nameof(halt));
        int fanOffArgument = ToNativeArgument(fanOff, nameof(fanOff));
        int fanMaxArgument = ToNativeArgument(fanMax, nameof(fanMax));

        unsafe
        {
            Native.NativeMethods.framework_ec_set_thermal_thresholds(
                HandlePointer,
                sensorIndex,
                warnArgument,
                highArgument,
                haltArgument,
                fanOffArgument,
                fanMaxArgument).ThrowIfFailure();
        }
    }

    /// <inheritdoc/>
    public FrameworkTemperatureSensorNameSnapshot GetSensorName(byte sensorIndex)
    {
        // Touch the accessor before consulting the cache. Every other member reaches the handle on
        // every call, which is how the owning connection raises ObjectDisposedException; a cache hit
        // would otherwise return a stale reading after the connection was disposed.
        _ = handleAccessor();

        // Lazy so that two threads racing on the same index issue one host command rather than two.
        // ConcurrentDictionary.GetOrAdd invokes its factory outside the lock.
        return sensorNameCache.GetOrAdd(
            sensorIndex,
            static (index, self) => new Lazy<FrameworkTemperatureSensorNameSnapshot>(() => self.ReadSensorName(index)),
            this).Value;
    }

    /// <inheritdoc/>
    public void ClearSensorNameCache()
    {
        sensorNameCache.Clear();
    }

    /// <inheritdoc/>
    public byte GetFanCount()
    {
        unsafe
        {
            return Native.NativeMethods.framework_ec_get_fan_count(HandlePointer).GetValueOrThrow();
        }
    }

    /// <summary>
    /// Encodes one threshold setting into the native argument, whose sign carries the caller's
    /// intent: negative keeps the current threshold, zero disables it, and a positive value is
    /// degrees Celsius.
    /// </summary>
    private static int ToNativeArgument(FrameworkThermalThresholdSetting setting, string parameterName)
    {
        switch (setting.Action)
        {
            case FrameworkThermalThresholdAction.KeepCurrent:
                return KeepCurrentArgument;

            case FrameworkThermalThresholdAction.Disable:
                return DisableArgument;

            case FrameworkThermalThresholdAction.Set:
                return ToPositiveCelsius(setting, parameterName);

            default:
                throw new ArgumentOutOfRangeException(parameterName, setting.Action, "The thermal threshold action is not recognized.");
        }
    }

    private static int ToPositiveCelsius(FrameworkThermalThresholdSetting setting, string parameterName)
    {
        if (!setting.Temperature.HasValue)
        {
            throw new ArgumentOutOfRangeException(parameterName, setting, "A threshold that is being set must carry a temperature.");
        }

        double degreesCelsius = setting.Temperature.Value.DegreesCelsius;

        if (double.IsNaN(degreesCelsius) || double.IsInfinity(degreesCelsius))
        {
            throw new ArgumentOutOfRangeException(parameterName, degreesCelsius, "The threshold temperature must be a finite value.");
        }

        double rounded = Math.Round(degreesCelsius, MidpointRounding.AwayFromZero);

        if (rounded < 1 || rounded > int.MaxValue)
        {
            throw new ArgumentOutOfRangeException(parameterName, degreesCelsius, "The threshold temperature must round to at least 1 degree Celsius, because the embedded controller reserves zero for disabling a threshold and negative values for keeping the current one. Use FrameworkThermalThresholdSetting.Disable to disable the threshold.");
        }

        return (int)rounded;
    }

    private FrameworkTemperatureSensorNameSnapshot ReadSensorName(byte sensorIndex)
    {
        unsafe
        {
            return Native.NativeMethods.framework_ec_get_temp_sensor_name(HandlePointer, sensorIndex).GetValueOrThrow();
        }
    }

    private unsafe Native.FrameworkEcHandle* HandlePointer => (Native.FrameworkEcHandle*)handleAccessor();
}
