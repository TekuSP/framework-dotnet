namespace FrameworkDotnet.Enums;

/// <summary>
/// Represents the marker codes the embedded controller inserts into the port 80 history buffer.
/// </summary>
/// <remarks>
/// These are sentinel entries rather than port 80 codes emitted by the host firmware, so an
/// entry matching one of these values marks a boundary in the history instead of a POST code.
/// </remarks>
public enum FrameworkPort80Event : ushort
{
    /// <summary>
    /// The system resumed from a low-power state at this point in the history.
    /// </summary>
    Resume = 0x1001,

    /// <summary>
    /// The system was reset at this point in the history.
    /// </summary>
    Reset = 0x1002,
}
