namespace FrameworkDotnet.Enums;

/// <summary>
/// Represents the static DisplayPort alt-mode capability and version of a numbered expansion card slot (board
/// specification, not the live alt-mode state reported in the Power Delivery alt-mode flags).
/// </summary>
public enum FrameworkDisplayPortCapability : byte
{
    /// <summary>
    /// No DisplayPort alt-mode on this slot.
    /// </summary>
    None = 0,

    /// <summary>
    /// DisplayPort 1.4 / 1.4a (HBR3).
    /// </summary>
    Dp14Hbr3 = 1,

    /// <summary>
    /// DisplayPort 2.0 (UHBR qualifier not documented).
    /// </summary>
    Dp20 = 2,

    /// <summary>
    /// DisplayPort 2.0 UHBR10.
    /// </summary>
    Dp20Uhbr10 = 3,

    /// <summary>
    /// DisplayPort 2.0 UHBR20.
    /// </summary>
    Dp20Uhbr20 = 4,

    /// <summary>
    /// DisplayPort 2.1 (UHBR qualifier not documented).
    /// </summary>
    Dp21 = 5,

    /// <summary>
    /// DisplayPort 2.1 UHBR10.
    /// </summary>
    Dp21Uhbr10 = 6,

    /// <summary>
    /// DisplayPort 2.1 UHBR20.
    /// </summary>
    Dp21Uhbr20 = 7,

    /// <summary>
    /// DisplayPort supported, but the version is not documented in the source matrix.
    /// </summary>
    Supported = 8,
}
