namespace FrameworkDotnet.Enums;

/// <summary>
/// Represents how a module was observed or classified.
/// </summary>
public enum FrameworkModuleBus : byte
{
    /// <summary>
    /// The bus or provenance is not known.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// The module was derived from EC data.
    /// </summary>
    Ec = 1,

    /// <summary>
    /// The module was derived from USB enumeration.
    /// </summary>
    Usb = 2,

    /// <summary>
    /// The module was derived from HID enumeration.
    /// </summary>
    Hid = 3,

    /// <summary>
    /// The module classification was derived from multiple sources.
    /// </summary>
    Composite = 4,
}
