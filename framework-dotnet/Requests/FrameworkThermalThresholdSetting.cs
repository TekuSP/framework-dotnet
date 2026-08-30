using System.Globalization;

using FrameworkDotnet.Enums;

namespace FrameworkDotnet.Requests;

/// <summary>
/// Represents the change requested for a single embedded controller thermal threshold.
/// </summary>
/// <remarks>
/// <para>
/// Writing thermal thresholds is a read-modify-write, and every threshold carries three distinct
/// intents that must stay unambiguous: keep the value firmware currently holds, disable the
/// threshold outright, or set it to an explicit temperature. A plain nullable temperature cannot
/// express all three, so each threshold argument is described by one of these settings instead.
/// </para>
/// <para>
/// The default value of this type is <see cref="KeepCurrent"/>, so a threshold that is not
/// mentioned in a write is never modified.
/// </para>
/// </remarks>
public readonly record struct FrameworkThermalThresholdSetting
{
    private FrameworkThermalThresholdSetting(FrameworkThermalThresholdAction action, UnitsNet.Temperature? temperature)
    {
        Action = action;
        Temperature = temperature;
    }

    /// <summary>
    /// Gets the setting that leaves the threshold exactly as embedded controller firmware currently
    /// holds it. This is also the default value of the type.
    /// </summary>
    public static FrameworkThermalThresholdSetting KeepCurrent => default;

    /// <summary>
    /// Gets the setting that disables the threshold, so that the embedded controller stops acting
    /// on it. This is distinct from <see cref="KeepCurrent"/>, which changes nothing.
    /// </summary>
    public static FrameworkThermalThresholdSetting Disable => new(FrameworkThermalThresholdAction.Disable, null);

    /// <summary>
    /// Gets the requested action for the threshold.
    /// </summary>
    public FrameworkThermalThresholdAction Action { get; }

    /// <summary>
    /// Gets the requested threshold temperature, or <see langword="null"/> when
    /// <see cref="Action"/> is not <see cref="FrameworkThermalThresholdAction.Set"/>.
    /// </summary>
    public UnitsNet.Temperature? Temperature { get; }

    /// <summary>
    /// Creates a setting that enables the threshold and sets it to an explicit temperature.
    /// </summary>
    /// <param name="temperature">The temperature the embedded controller should act on. It must round to at least one degree Celsius, because zero and negative values are reserved by the native ABI for "disable" and "keep current".</param>
    /// <returns>A setting describing the requested temperature.</returns>
    public static FrameworkThermalThresholdSetting FromTemperature(UnitsNet.Temperature temperature)
    {
        return new FrameworkThermalThresholdSetting(FrameworkThermalThresholdAction.Set, temperature);
    }

    /// <summary>
    /// Creates a setting that enables the threshold and sets it to an explicit temperature in
    /// degrees Celsius.
    /// </summary>
    /// <param name="degreesCelsius">The temperature in degrees Celsius. It must round to at least one, because zero and negative values are reserved by the native ABI for "disable" and "keep current".</param>
    /// <returns>A setting describing the requested temperature.</returns>
    public static FrameworkThermalThresholdSetting FromDegreesCelsius(double degreesCelsius)
    {
        return FromTemperature(UnitsNet.Temperature.FromDegreesCelsius(degreesCelsius));
    }

    /// <summary>
    /// Converts a temperature into a setting that enables the threshold at that temperature.
    /// </summary>
    /// <param name="temperature">The temperature the embedded controller should act on.</param>
    public static implicit operator FrameworkThermalThresholdSetting(UnitsNet.Temperature temperature)
    {
        return FromTemperature(temperature);
    }

    /// <summary>
    /// Returns a readable description of the requested change.
    /// </summary>
    /// <returns>A readable description of the requested change.</returns>
    public override string ToString()
    {
        return Action switch
        {
            FrameworkThermalThresholdAction.Disable => "Thermal Threshold Setting: Disable",
            FrameworkThermalThresholdAction.Set => Temperature.HasValue
                ? $"Thermal Threshold Setting: Set to {Temperature.Value.ToString(CultureInfo.InvariantCulture)}"
                : "Thermal Threshold Setting: Set to an unspecified temperature",
            _ => "Thermal Threshold Setting: Keep current",
        };
    }
}
