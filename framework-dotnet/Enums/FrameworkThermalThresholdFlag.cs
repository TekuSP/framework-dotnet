namespace FrameworkDotnet.Enums;

/// <summary>
/// Represents which thermal thresholds the embedded controller firmware currently has enabled.
/// </summary>
/// <remarks>
/// A clear bit means the firmware has that threshold disabled. Always test this mask instead of
/// comparing the reported Celsius value, because a disabled threshold reads back as -273.
/// </remarks>
[System.Flags]
public enum FrameworkThermalThresholdFlag : uint
{
    /// <summary>
    /// The warning threshold is enabled.
    /// </summary>
    Warn = 0x01,

    /// <summary>
    /// The high-temperature threshold is enabled.
    /// </summary>
    High = 0x02,

    /// <summary>
    /// The halt threshold is enabled.
    /// </summary>
    Halt = 0x04,

    /// <summary>
    /// The warning release threshold is enabled.
    /// </summary>
    WarnRelease = 0x08,

    /// <summary>
    /// The high-temperature release threshold is enabled.
    /// </summary>
    HighRelease = 0x10,

    /// <summary>
    /// The halt release threshold is enabled.
    /// </summary>
    HaltRelease = 0x20,

    /// <summary>
    /// The fan-off threshold is enabled.
    /// </summary>
    FanOff = 0x40,

    /// <summary>
    /// The fan-maximum threshold is enabled.
    /// </summary>
    FanMax = 0x80,
}
