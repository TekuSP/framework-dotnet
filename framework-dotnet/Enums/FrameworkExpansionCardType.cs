namespace FrameworkDotnet.Enums;

/// <summary>
/// Represents the discriminant type for a card plugged into a numbered USB-C expansion card slot.
/// </summary>
public enum FrameworkExpansionCardType : ushort
{
    /// <summary>
    /// Card type could not be determined.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// DisplayPort expansion card.
    /// </summary>
    DisplayPort = 1,

    /// <summary>
    /// HDMI expansion card.
    /// </summary>
    Hdmi = 2,

    /// <summary>
    /// Audio expansion card.
    /// </summary>
    Audio = 3,

    /// <summary>
    /// USB-A expansion card.
    /// </summary>
    UsbA = 4,

    /// <summary>
    /// USB-C expansion card (passive passthrough).
    /// </summary>
    UsbC = 5,

    /// <summary>
    /// Ethernet 2.5G expansion card (Realtek RTL8156B).
    /// </summary>
    Ethernet = 6,

    /// <summary>
    /// Ethernet 10G expansion card (WisdPi).
    /// </summary>
    Ethernet10G = 7,

    /// <summary>
    /// MicroSD expansion card.
    /// </summary>
    MicroSd = 8,

    /// <summary>
    /// Full-size SD expansion card.
    /// </summary>
    Sd = 9,

    /// <summary>
    /// NVMe storage expansion card.
    /// </summary>
    Ssd = 10,
}
