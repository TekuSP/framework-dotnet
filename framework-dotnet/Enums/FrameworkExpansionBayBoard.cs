using FrameworkDotnet.Attributes;

namespace FrameworkDotnet.Enums;

/// <summary>
/// Represents the physical expansion bay board type or board-state classification.
/// </summary>
[FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "Upstream framework-system currently documents expansion-bay status support on Framework Laptop 16 only.")]
public enum FrameworkExpansionBayBoard
{
    /// <summary>
    /// The board type could not be identified.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// Dual interposer board.
    /// </summary>
    DualInterposer = 1,

    /// <summary>
    /// Single interposer board.
    /// </summary>
    SingleInterposer = 2,

    /// <summary>
    /// UMA or fan board configuration.
    /// </summary>
    UmaFans = 3,

    /// <summary>
    /// The bay reports that no module is installed.
    /// </summary>
    NoModule = 4,

    /// <summary>
    /// A module or board may be present but the connection or state is faulty.
    /// </summary>
    BadConnection = 5,
}
