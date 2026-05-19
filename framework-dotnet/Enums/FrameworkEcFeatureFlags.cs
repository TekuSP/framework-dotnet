using FrameworkDotnet.Attributes;

namespace FrameworkDotnet.Enums;

/// <summary>
/// Represents optional EC feature flags reported by the native library.
/// </summary>
[System.Flags]
public enum FrameworkEcFeatureFlags : ulong
{
    /// <summary>
    /// No optional EC features are reported.
    /// </summary>
    None = 0,

    /// <summary>
    /// Keyboard-related EC support is available.
    /// </summary>
    Keyboard = 1UL << 0,

    /// <summary>
    /// Keyboard backlight queries are supported.
    /// </summary>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework13, Message = "Upstream framework-system currently documents keyboard-backlight support on Framework Laptop 13 only.")]
    KeyboardBacklight = 1UL << 1,

    /// <summary>
    /// Touchpad-related support is available.
    /// </summary>
    Touchpad = 1UL << 2,

    /// <summary>
    /// Fingerprint reader or fingerprint LED support is available.
    /// </summary>
    Fingerprint = 1UL << 3,

    /// <summary>
    /// Ambient-light functionality is available.
    /// </summary>
    AmbientLight = 1UL << 4,

    /// <summary>
    /// Tablet-mode reporting or support is available.
    /// </summary>
    TabletMode = 1UL << 5,

    /// <summary>
    /// All currently defined EC feature flags are present.
    /// </summary>
    All = Keyboard | KeyboardBacklight | Touchpad | Fingerprint | AmbientLight | TabletMode,
}
