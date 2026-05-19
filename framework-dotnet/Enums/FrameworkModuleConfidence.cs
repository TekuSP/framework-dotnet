namespace FrameworkDotnet.Enums;

/// <summary>
/// Represents the confidence level for a module classification.
/// </summary>
public enum FrameworkModuleConfidence : byte
{
    /// <summary>
    /// The confidence level was not set.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// The classification is a weak heuristic inference.
    /// </summary>
    DerivedWeak = 1,

    /// <summary>
    /// The classification is a strong heuristic inference.
    /// </summary>
    DerivedStrong = 2,

    /// <summary>
    /// The classification was directly observed or explicitly reported.
    /// </summary>
    Direct = 3,
}
