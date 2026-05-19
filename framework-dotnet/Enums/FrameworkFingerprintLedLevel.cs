namespace FrameworkDotnet.Enums;

/// <summary>
/// Represents the interpreted fingerprint LED intensity or mode.
/// </summary>
public enum FrameworkFingerprintLedLevel
{
    /// <summary>
    /// The raw LED level could not be mapped.
    /// </summary>
    Unknown = -1,

    /// <summary>
    /// High brightness.
    /// </summary>
    High = 0,

    /// <summary>
    /// Medium brightness.
    /// </summary>
    Medium = 1,

    /// <summary>
    /// Low brightness.
    /// </summary>
    Low = 2,

    /// <summary>
    /// Very low brightness.
    /// </summary>
    UltraLow = 3,

    /// <summary>
    /// A custom or nonstandard LED level was reported.
    /// </summary>
    Custom = 254,

    /// <summary>
    /// Automatic firmware-managed LED mode.
    /// </summary>
    Auto = 255,
}
