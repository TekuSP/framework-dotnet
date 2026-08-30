namespace FrameworkDotnet.Enums;

/// <summary>
/// Represents the battery cutoff (ship mode) state reported by the embedded controller.
/// </summary>
public enum FrameworkBatteryCutoffState
{
    /// <summary>
    /// The embedded controller did not answer the cutoff query.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// The battery is connected and not in ship mode.
    /// </summary>
    NotCutOff = 1,

    /// <summary>
    /// The battery has been cut off and is in ship mode.
    /// </summary>
    CutOff = 2,
}
