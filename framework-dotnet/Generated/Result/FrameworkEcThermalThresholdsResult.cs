using FrameworkDotnet.Enums;
using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Snapshots;

using UnitsNet;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcThermalThresholdsResult
{
    internal readonly FrameworkThermalThresholdsSnapshot GetValueOrThrow()
    {
        if (status.IsFailure)
        {
            throw FrameworkStatusException.GetCorrectException(status);
        }

        var enabled = (FrameworkThermalThresholdFlag)thresholds.enabled_mask;

        return new FrameworkThermalThresholdsSnapshot(
            sensor_index,
            enabled,
            ToTemperature(enabled, FrameworkThermalThresholdFlag.Warn, thresholds.warn_celsius),
            ToTemperature(enabled, FrameworkThermalThresholdFlag.High, thresholds.high_celsius),
            ToTemperature(enabled, FrameworkThermalThresholdFlag.Halt, thresholds.halt_celsius),
            ToTemperature(enabled, FrameworkThermalThresholdFlag.WarnRelease, thresholds.warn_release_celsius),
            ToTemperature(enabled, FrameworkThermalThresholdFlag.HighRelease, thresholds.high_release_celsius),
            ToTemperature(enabled, FrameworkThermalThresholdFlag.HaltRelease, thresholds.halt_release_celsius),
            ToTemperature(enabled, FrameworkThermalThresholdFlag.FanOff, thresholds.fan_off_celsius),
            ToTemperature(enabled, FrameworkThermalThresholdFlag.FanMax, thresholds.fan_max_celsius));
    }

    /// <summary>
    /// A disabled threshold reads back as -273 degrees Celsius, so the enabled mask is the only
    /// authoritative source for whether a threshold is active. Never test the Celsius value.
    /// </summary>
    private static Temperature? ToTemperature(FrameworkThermalThresholdFlag enabledMask, FrameworkThermalThresholdFlag flag, int celsius)
    {
        return (enabledMask & flag) == flag
            ? Temperature.FromDegreesCelsius(celsius)
            : null;
    }
}
