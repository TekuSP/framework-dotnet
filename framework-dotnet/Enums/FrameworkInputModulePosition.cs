namespace FrameworkDotnet.Enums;

/// <summary>
/// Represents the physical position of an input-deck module on Framework Laptop 16 (the 8-wide input-deck MUX).
/// Mirrors the native input-deck slots; <see cref="Unknown"/> for any module that is not input-deck-mounted or on
/// platforms that do not report a deck position.
/// </summary>
public enum FrameworkInputModulePosition : ushort
{
    /// <summary>Not an input-deck-mounted module, or the platform reports no deck position.</summary>
    Unknown = 0,

    /// <summary>Top-row slot 0 (far left).</summary>
    TopRow0 = 1,

    /// <summary>Top-row slot 1.</summary>
    TopRow1 = 2,

    /// <summary>Top-row slot 2.</summary>
    TopRow2 = 3,

    /// <summary>Top-row slot 3.</summary>
    TopRow3 = 4,

    /// <summary>Top-row slot 4 (far right).</summary>
    TopRow4 = 5,

    /// <summary>Touchpad in the lower section.</summary>
    Touchpad = 6,

    /// <summary>The hub board all input modules connect through.</summary>
    HubBoard = 7,
}
