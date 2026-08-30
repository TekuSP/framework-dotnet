namespace FrameworkDotnet.Enums;

/// <summary>
/// Represents the click force threshold applied to a haptic touchpad.
/// </summary>
/// <remarks>
/// The click force is write-only: the firmware never answers a feature report for it, so the
/// current value cannot be read back.
/// </remarks>
public enum FrameworkClickForce
{
    /// <summary>
    /// The touchpad registers a click at a low actuation force.
    /// </summary>
    Low = 1,

    /// <summary>
    /// The touchpad registers a click at a medium actuation force.
    /// </summary>
    Medium = 2,

    /// <summary>
    /// The touchpad registers a click at a high actuation force.
    /// </summary>
    High = 3,
}
