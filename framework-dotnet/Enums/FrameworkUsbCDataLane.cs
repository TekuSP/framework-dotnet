namespace FrameworkDotnet.Enums;

/// <summary>
/// Represents the static USB-C data-lane capability of a numbered expansion card slot (board specification, not
/// the live negotiated link).
/// </summary>
public enum FrameworkUsbCDataLane : byte
{
    /// <summary>
    /// Capability is not documented for this slot or platform.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// USB 2.0 only.
    /// </summary>
    Usb2 = 1,

    /// <summary>
    /// USB 3.2 (generation unspecified).
    /// </summary>
    Usb32 = 2,

    /// <summary>
    /// USB 3.2 Gen 2x1 (10 Gbps).
    /// </summary>
    Usb32Gen2x1 = 3,

    /// <summary>
    /// USB 3.2 Gen 2x2 (20 Gbps).
    /// </summary>
    Usb32Gen2x2 = 4,

    /// <summary>
    /// USB4.
    /// </summary>
    Usb4 = 5,

    /// <summary>
    /// Thunderbolt 4.
    /// </summary>
    Thunderbolt4 = 6,
}
