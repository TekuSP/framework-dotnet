namespace FrameworkDotnet.Enums;

/// <summary>
/// Identifies the detected Framework product family.
/// </summary>
public enum FrameworkPlatformFamily
{
    /// <summary>
    /// Unknown product family.
    /// </summary>
    Unknown = -1,

    /// <summary>
    /// Framework 12 family.
    /// </summary>
    Framework12 = 0,

    /// <summary>
    /// Framework 13 family.
    /// </summary>
    Framework13 = 1,

    /// <summary>
    /// Framework 16 family.
    /// </summary>
    Framework16 = 2,

    /// <summary>
    /// Framework Desktop family.
    /// </summary>
    FrameworkDesktop = 3,
}
