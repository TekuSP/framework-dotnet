using System.Globalization;

using FrameworkDotnet.Attributes;
using FrameworkDotnet.Enums;

using UnitsNet;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents a keyboard backlight snapshot returned by the EC.
/// </summary>
[FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework13, Message = "Upstream framework-system currently documents keyboard-backlight support on Framework Laptop 13 only.")]
public sealed record FrameworkKeyboardBacklightSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkKeyboardBacklightSnapshot"/> class.
    /// </summary>
    /// <param name="brightness">The current keyboard backlight brightness.</param>
    public FrameworkKeyboardBacklightSnapshot(Ratio brightness)
    {
        Brightness = brightness;
    }

    /// <summary>
    /// Gets the current keyboard backlight brightness.
    /// </summary>
    public Ratio Brightness { get; init; }

    public override string ToString()
    {
        return $"Keyboard Backlight Snapshot: Brightness: {Brightness.ToString(CultureInfo.InvariantCulture)}";
    }
}
