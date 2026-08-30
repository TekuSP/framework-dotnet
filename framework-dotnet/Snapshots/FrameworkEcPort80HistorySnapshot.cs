using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using FrameworkDotnet.Enums;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents the port 80 POST code history recorded by the embedded controller.
/// </summary>
/// <remarks>
/// <para>
/// The controller keeps POST codes in a wrapping ring buffer. <see cref="Codes"/> is that ring
/// in raw buffer order, and <see cref="CodesNewestFirst"/> walks it backwards from the newest
/// entry so that a caller can read the boot in reverse chronological order without doing the
/// modular arithmetic itself.
/// </para>
/// <para>
/// Entries whose value matches a <see cref="FrameworkPort80Event"/> member are markers the
/// controller inserted rather than POST codes emitted by host firmware. Use
/// <see cref="GetMarkerEvent(ushort)"/> to classify an entry.
/// </para>
/// </remarks>
public sealed record FrameworkEcPort80HistorySnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkEcPort80HistorySnapshot"/> class.
    /// </summary>
    /// <param name="writes">The total number of port 80 writes the controller has recorded since it booted.</param>
    /// <param name="historySize">The size of the controller's history buffer, in entries.</param>
    /// <param name="codes">The history ring in raw buffer order.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="codes"/> is <see langword="null"/>.</exception>
    public FrameworkEcPort80HistorySnapshot(uint writes, uint historySize, IReadOnlyList<ushort> codes)
    {
        ArgumentNullException.ThrowIfNull(codes);

        Writes = writes;
        HistorySize = historySize;
        Codes = codes;

        var count = codes.Count;
        if (count == 0 || writes == 0)
        {
            NewestIndex = -1;
            CodesNewestFirst = [];
            return;
        }

        // `writes` is the next slot the controller will write, so the newest entry is the slot
        // before it. Upstream's own printer walks `tail..head` exclusive of `head` and labels the
        // last code it emits as the newest, which is `codes[(writes - 1) % history_size]`.
        var newestIndex = (int)((writes - 1u) % (uint)count);

        // Before the ring has wrapped only `writes` slots hold real codes; the rest were never
        // written. Upstream clamps the same way with `tail = head.saturating_sub(history_size)`.
        var populated = writes < (uint)count ? (int)writes : count;

        var ordered = new ushort[populated];
        for (var offset = 0; offset < populated; offset++)
        {
            ordered[offset] = codes[((newestIndex - offset) + count) % count];
        }

        NewestIndex = newestIndex;
        CodesNewestFirst = ordered;
    }

    /// <summary>
    /// Gets the total number of port 80 writes the controller has recorded since it booted.
    /// </summary>
    /// <remarks>
    /// This counts every write, so it keeps growing after the ring has wrapped and is therefore
    /// larger than <see cref="HistorySize"/> on any system that has been running for a while.
    /// </remarks>
    public uint Writes { get; init; }

    /// <summary>
    /// Gets the size of the controller's history buffer, in entries.
    /// </summary>
    public uint HistorySize { get; init; }

    /// <summary>
    /// Gets the history ring in raw buffer order.
    /// </summary>
    /// <remarks>
    /// This is the buffer exactly as the controller stores it, so index 0 is the start of the
    /// ring and not the oldest entry. <see cref="NewestIndex"/> points at the newest entry.
    /// </remarks>
    public IReadOnlyList<ushort> Codes { get; init; }

    /// <summary>
    /// Gets the history walked backwards from the newest entry, so that index 0 is the most
    /// recently written POST code.
    /// </summary>
    /// <remarks>
    /// Only slots the controller has actually written are included, so before the ring has
    /// wrapped this is shorter than <see cref="Codes"/> and is empty when <see cref="Writes"/>
    /// is zero.
    /// </remarks>
    public IReadOnlyList<ushort> CodesNewestFirst { get; init; }

    /// <summary>
    /// Gets the index into <see cref="Codes"/> of the newest entry, or <c>-1</c> when the
    /// controller returned no entries or has recorded no writes.
    /// </summary>
    /// <remarks>
    /// Computed as <c>(writes - 1) % history_size</c>. <see cref="Writes"/> is the slot the
    /// controller will write next, so the newest entry is the slot before it; upstream's own
    /// history printer walks an exclusive upper bound and labels the same slot as the newest.
    /// The <c>writes % history_size</c> formula stated in the native ABI comment names the next
    /// write slot, which holds the oldest entry once the ring has wrapped.
    /// </remarks>
    public int NewestIndex { get; init; }

    /// <summary>
    /// Classifies a history entry as one of the marker events the controller inserts.
    /// </summary>
    /// <param name="code">The history entry to classify.</param>
    /// <returns>The marker event the entry represents, or <see langword="null"/> when the entry is an ordinary POST code.</returns>
    public static FrameworkPort80Event? GetMarkerEvent(ushort code)
    {
        return code switch
        {
            (ushort)FrameworkPort80Event.Resume => FrameworkPort80Event.Resume,
            (ushort)FrameworkPort80Event.Reset => FrameworkPort80Event.Reset,
            _ => null,
        };
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        var preview = string.Join(", ", CodesNewestFirst.Take(8).Select(static code => $"0x{code.ToString("X4", CultureInfo.InvariantCulture)}"));

        return $"EC Port 80 History: Writes: {Writes.ToString(CultureInfo.InvariantCulture)}, History Size: {HistorySize.ToString(CultureInfo.InvariantCulture)}, Entries: {Codes.Count.ToString(CultureInfo.InvariantCulture)}, Newest Index: {NewestIndex.ToString(CultureInfo.InvariantCulture)}, Newest First: [{preview}]";
    }
}
