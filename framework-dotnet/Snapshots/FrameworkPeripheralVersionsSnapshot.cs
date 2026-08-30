using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents the firmware versions of one category of USB or HID peripherals.
/// </summary>
/// <remarks>
/// <para>
/// The native layer reports a fixed table of eight slots for every category, filled from slot zero
/// upwards, and states how many of them it populated in <see cref="Count"/>. Enumerate
/// <see cref="ReportedPeripherals"/> rather than indexing blindly; the individual slot properties exist
/// only to mirror the native layout.
/// </para>
/// <para>
/// A category with nothing connected reports a <see cref="Count"/> of zero. That is a normal reading and
/// not an error: a system with no audio expansion card fitted, or a camera the host could not enumerate,
/// answers successfully with an empty table.
/// </para>
/// </remarks>
public sealed record FrameworkPeripheralVersionsSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkPeripheralVersionsSnapshot"/> class.
    /// </summary>
    /// <param name="count">The number of populated slots reported by the native layer.</param>
    /// <param name="peripheral_0">The first peripheral slot.</param>
    /// <param name="peripheral_1">The second peripheral slot.</param>
    /// <param name="peripheral_2">The third peripheral slot.</param>
    /// <param name="peripheral_3">The fourth peripheral slot.</param>
    /// <param name="peripheral_4">The fifth peripheral slot.</param>
    /// <param name="peripheral_5">The sixth peripheral slot.</param>
    /// <param name="peripheral_6">The seventh peripheral slot.</param>
    /// <param name="peripheral_7">The eighth peripheral slot.</param>
    public FrameworkPeripheralVersionsSnapshot(byte count, FrameworkPeripheralVersionSnapshot peripheral_0, FrameworkPeripheralVersionSnapshot peripheral_1, FrameworkPeripheralVersionSnapshot peripheral_2, FrameworkPeripheralVersionSnapshot peripheral_3, FrameworkPeripheralVersionSnapshot peripheral_4, FrameworkPeripheralVersionSnapshot peripheral_5, FrameworkPeripheralVersionSnapshot peripheral_6, FrameworkPeripheralVersionSnapshot peripheral_7)
    {
        Count = count;
        Peripheral_0 = peripheral_0;
        Peripheral_1 = peripheral_1;
        Peripheral_2 = peripheral_2;
        Peripheral_3 = peripheral_3;
        Peripheral_4 = peripheral_4;
        Peripheral_5 = peripheral_5;
        Peripheral_6 = peripheral_6;
        Peripheral_7 = peripheral_7;
    }

    /// <summary>
    /// Gets the number of populated slots reported by the native layer.
    /// </summary>
    /// <remarks>
    /// Slots are filled contiguously from index zero, so the populated slots are the first
    /// <see cref="Count"/> entries of <see cref="Peripherals"/>. The value never exceeds eight, the fixed
    /// table size of the native report; a category with more devices attached is truncated.
    /// </remarks>
    public byte Count { get; init; }

    /// <summary>
    /// Gets the first peripheral slot.
    /// </summary>
    public FrameworkPeripheralVersionSnapshot Peripheral_0 { get; init; }

    /// <summary>
    /// Gets the second peripheral slot.
    /// </summary>
    public FrameworkPeripheralVersionSnapshot Peripheral_1 { get; init; }

    /// <summary>
    /// Gets the third peripheral slot.
    /// </summary>
    public FrameworkPeripheralVersionSnapshot Peripheral_2 { get; init; }

    /// <summary>
    /// Gets the fourth peripheral slot.
    /// </summary>
    public FrameworkPeripheralVersionSnapshot Peripheral_3 { get; init; }

    /// <summary>
    /// Gets the fifth peripheral slot.
    /// </summary>
    public FrameworkPeripheralVersionSnapshot Peripheral_4 { get; init; }

    /// <summary>
    /// Gets the sixth peripheral slot.
    /// </summary>
    public FrameworkPeripheralVersionSnapshot Peripheral_5 { get; init; }

    /// <summary>
    /// Gets the seventh peripheral slot.
    /// </summary>
    public FrameworkPeripheralVersionSnapshot Peripheral_6 { get; init; }

    /// <summary>
    /// Gets the eighth peripheral slot.
    /// </summary>
    public FrameworkPeripheralVersionSnapshot Peripheral_7 { get; init; }

    /// <summary>
    /// Gets all peripheral slots in index order, populated or not.
    /// </summary>
    public IReadOnlyList<FrameworkPeripheralVersionSnapshot> Peripherals => [Peripheral_0, Peripheral_1, Peripheral_2, Peripheral_3, Peripheral_4, Peripheral_5, Peripheral_6, Peripheral_7];

    /// <summary>
    /// Gets the populated peripheral slots in index order.
    /// </summary>
    /// <seealso cref="Count"/>
    public IEnumerable<FrameworkPeripheralVersionSnapshot> ReportedPeripherals => Peripherals.Take(Count);

    /// <inheritdoc/>
    public override string ToString()
    {
        return Count == 0
            ? "Peripheral Versions: None Reported"
            : $"Peripheral Versions: Count: {Count.ToString(CultureInfo.InvariantCulture)}, Peripherals: {string.Join(", ", ReportedPeripherals)}";
    }
}
