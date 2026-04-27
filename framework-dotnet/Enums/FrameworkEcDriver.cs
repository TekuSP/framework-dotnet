namespace FrameworkDotnet.Enums;

/// <summary>
/// Identifies the embedded controller driver implementation.
/// </summary>
public enum FrameworkEcDriver
{
    /// <summary>
    /// The driver could not be determined.
    /// </summary>
    Unknown = -1,

    /// <summary>
    /// Uses the PortIO driver implementation.
    /// </summary>
    Portio = 0,

    /// <summary>
    /// Uses the Chrome EC driver implementation.
    /// </summary>
    CrosEc = 1,

    /// <summary>
    /// Uses the Windows driver implementation.
    /// </summary>
    Windows = 2,
}
