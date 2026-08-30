namespace FrameworkDotnet.Enums;

/// <summary>
/// Identifies how the embedded controller classifies a temperature sensor slot.
/// </summary>
/// <remarks>
/// The values mirror the raw EC sensor-type tag reported alongside a temperature sensor's firmware
/// name. Firmware that reports a tag this version does not recognize surfaces as the raw numeric
/// value cast onto this enumeration, so callers should treat unnamed values as "unclassified"
/// rather than assuming the set is closed.
/// </remarks>
public enum FrameworkTemperatureSensorType : byte
{
    /// <summary>
    /// The slot carries no usable sensor and the embedded controller ignores it.
    /// </summary>
    Ignored = 0,

    /// <summary>
    /// The sensor measures the CPU or SoC package.
    /// </summary>
    Cpu = 1,

    /// <summary>
    /// The sensor measures the mainboard.
    /// </summary>
    Board = 2,

    /// <summary>
    /// The sensor measures the chassis skin.
    /// </summary>
    Case = 3,

    /// <summary>
    /// The sensor measures the battery pack.
    /// </summary>
    Battery = 4,
}
