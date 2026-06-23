namespace FrameworkDotnet.Enums;

/// <summary>
/// Represents the USB PD data role for a port.
/// </summary>
public enum FrameworkPowerDeliveryDataRole
{
    /// <summary>
    /// Upstream Facing Port — port acts as a USB device.
    /// </summary>
    Ufp = 0,

    /// <summary>
    /// Downstream Facing Port — port acts as a USB host.
    /// </summary>
    Dfp = 1,

    /// <summary>
    /// Data is disconnected.
    /// </summary>
    Disconnected = 2,

    /// <summary>
    /// Data role could not be determined.
    /// </summary>
    Unknown = 3,
}
