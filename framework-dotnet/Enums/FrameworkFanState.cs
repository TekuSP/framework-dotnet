namespace FrameworkDotnet.Enums;

/// <summary>
/// Represents the state of a fan reading.
/// </summary>
public enum FrameworkFanState
{
    /// <summary>
    /// The fan reading is valid.
    /// </summary>
    Ok = 0,

    /// <summary>
    /// The fan is not present.
    /// </summary>
    NotPresent = 1,

    /// <summary>
    /// The fan is stalled.
    /// </summary>
    Stalled = 2,
}
