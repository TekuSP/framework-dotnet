namespace FrameworkDotnet.Enums;

/// <summary>
/// Represents supported fan features.
/// </summary>
[System.Flags]
public enum FrameworkFanFeaturesState : byte
{
    /// <summary>
    /// No fan features are supported.
    /// </summary>
    None = 0,

    /// <summary>
    /// Manual fan control is supported.
    /// </summary>
    FanControl = 1,

    /// <summary>
    /// Thermal reporting is supported.
    /// </summary>
    ThermalReporting = 2,

    /// <summary>
    /// All currently defined fan features are supported.
    /// </summary>
    All = FanControl + ThermalReporting,
}
