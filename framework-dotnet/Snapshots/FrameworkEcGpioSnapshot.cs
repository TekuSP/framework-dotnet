using System.Globalization;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents a single general-purpose input/output line exposed by the Framework embedded controller.
/// </summary>
/// <remarks>
/// A snapshot is a point-in-time reading. The <see cref="IsHigh"/> level is sampled at the moment the
/// embedded controller answers the host command and is not refreshed afterwards.
/// </remarks>
public sealed record FrameworkEcGpioSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkEcGpioSnapshot"/> class.
    /// </summary>
    /// <param name="index">The zero-based position of the line within the embedded controller GPIO table.</param>
    /// <param name="name">The firmware-assigned name of the line.</param>
    /// <param name="isHigh">A value indicating whether the line reads as logic high.</param>
    /// <param name="flags">The raw firmware-defined configuration bitmask for the line.</param>
    public FrameworkEcGpioSnapshot(int index, string name, bool isHigh, uint flags)
    {
        Index = index;
        Name = name;
        IsHigh = isHigh;
        Flags = flags;
    }

    /// <summary>
    /// Gets the zero-based position of the line within the embedded controller GPIO table.
    /// </summary>
    public int Index { get; init; }

    /// <summary>
    /// Gets the firmware-assigned name of the line.
    /// </summary>
    /// <remarks>
    /// The name is the identifier the by-name read and write APIs expect. Firmware truncates names to
    /// 32 bytes, so every reported name is safe to pass straight back to those APIs.
    /// </remarks>
    public string Name { get; init; }

    /// <summary>
    /// Gets a value indicating whether the line reads as logic high.
    /// </summary>
    public bool IsHigh { get; init; }

    /// <summary>
    /// Gets the raw firmware-defined configuration bitmask for the line.
    /// </summary>
    /// <remarks>
    /// The embedded controller reports the pin configuration word verbatim (direction, drive mode, pull
    /// resistors, interrupt triggers and lock state). The bit layout belongs to the embedded controller
    /// firmware, is not part of the stable native contract, and is therefore surfaced undecoded. Treat it
    /// as diagnostic data and do not branch production logic on individual bits.
    /// </remarks>
    public uint Flags { get; init; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"GPIO {Index.ToString(CultureInfo.InvariantCulture)} ({Name}): {(IsHigh ? "High" : "Low")}, Flags: 0x{Flags.ToString("X8", CultureInfo.InvariantCulture)}";
    }
}
