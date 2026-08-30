using System.Globalization;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents a single per-key keyboard color expressed as 8-bit red, green and blue components.
/// </summary>
/// <remarks>
/// The embedded controller consumes per-key colors as three consecutive bytes in red, green, blue order.
/// This type exists so that a managed caller cannot accidentally supply a byte buffer with the wrong stride:
/// one instance always describes exactly one key.
/// </remarks>
public readonly record struct FrameworkKeyboardColor
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkKeyboardColor"/> struct.
    /// </summary>
    /// <param name="red">The red component.</param>
    /// <param name="green">The green component.</param>
    /// <param name="blue">The blue component.</param>
    public FrameworkKeyboardColor(byte red, byte green, byte blue)
    {
        Red = red;
        Green = green;
        Blue = blue;
    }

    /// <summary>
    /// Gets the red component of the color.
    /// </summary>
    public byte Red { get; init; }

    /// <summary>
    /// Gets the green component of the color.
    /// </summary>
    public byte Green { get; init; }

    /// <summary>
    /// Gets the blue component of the color.
    /// </summary>
    public byte Blue { get; init; }

    /// <summary>
    /// Returns a culture-invariant textual representation of the color.
    /// </summary>
    /// <returns>A string describing the red, green and blue components.</returns>
    public override string ToString()
    {
        return $"Keyboard Color: R: {Red.ToString(CultureInfo.InvariantCulture)}, G: {Green.ToString(CultureInfo.InvariantCulture)}, B: {Blue.ToString(CultureInfo.InvariantCulture)}";
    }
}
