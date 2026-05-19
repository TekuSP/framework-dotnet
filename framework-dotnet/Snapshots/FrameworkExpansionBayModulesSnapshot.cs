using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using FrameworkDotnet.Attributes;
using FrameworkDotnet.Enums;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents the expansion-bay module snapshot exposed by the EC.
/// </summary>
/// <remarks>
/// The current upstream expansion-bay status surface reports a single bay entry. This managed snapshot keeps a fixed-slot, count-aware aggregate shape so additional bay entries can be added later without replacing the current API pattern. <see cref="ExpansionBay_0"/> may be returned as a more specific <see cref="FrameworkExpansionBaySnapshot"/> subtype, such as <see cref="FrameworkAmdGpuExpansionBaySnapshot"/>, <see cref="FrameworkNvidiaGpuExpansionBaySnapshot"/>, <see cref="FrameworkSsdHolderExpansionBaySnapshot"/>, or <see cref="FrameworkFanOnlyExpansionBaySnapshot"/>.
/// </remarks>
[FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "Upstream framework-system currently documents expansion-bay status support on Framework Laptop 16 only.")]
public sealed record FrameworkExpansionBayModulesSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkExpansionBayModulesSnapshot"/> class.
    /// </summary>
    /// <param name="expansionBayCount">The number of reported expansion-bay entries.</param>
    /// <param name="expansionBay_0">The first expansion-bay snapshot.</param>
    public FrameworkExpansionBayModulesSnapshot(byte expansionBayCount, FrameworkExpansionBaySnapshot expansionBay_0)
    {
        ArgumentNullException.ThrowIfNull(expansionBay_0);

        ExpansionBayCount = expansionBayCount;
        ExpansionBay_0 = expansionBay_0;
    }

    /// <summary>
    /// Gets the number of reported expansion-bay entries.
    /// </summary>
    public byte ExpansionBayCount { get; init; }

    /// <summary>
    /// Gets the first expansion-bay snapshot.
    /// </summary>
    public FrameworkExpansionBaySnapshot ExpansionBay_0 { get; init; }

    /// <summary>
    /// Gets all expansion-bay snapshots in index order.
    /// </summary>
    public IReadOnlyList<FrameworkExpansionBaySnapshot> ExpansionBays => [ExpansionBay_0];

    /// <summary>
    /// Gets the reported expansion-bay snapshots in index order.
    /// </summary>
    public IEnumerable<FrameworkExpansionBaySnapshot> ReportedExpansionBays => ExpansionBays.Take(ExpansionBayCount);

    public override string ToString()
    {
        return $"Expansion Bay Modules Snapshot: Expansion Bay Count: {ExpansionBayCount.ToString(CultureInfo.InvariantCulture)}, Expansion Bays: {string.Join(", ", ReportedExpansionBays)}";
    }
}
