namespace FrameworkDotnet.Enums;

/// <summary>
/// Identifies the currently active EC firmware image.
/// </summary>
public enum FrameworkEcCurrentImage
{
    /// <summary>
    /// The active image is unknown.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// The read-only image is active.
    /// </summary>
    Ro = 1,

    /// <summary>
    /// The read-write image is active.
    /// </summary>
    Rw = 2,
}
