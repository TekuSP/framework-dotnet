using System;

using FrameworkDotnet.Attributes;
using FrameworkDotnet.Enums;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents an expansion bay snapshot returned by the EC.
/// </summary>
[FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "Upstream framework-system currently documents expansion-bay status support on Framework Laptop 16 only.")]
public record FrameworkExpansionBaySnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkExpansionBaySnapshot"/> class.
    /// </summary>
    /// <param name="isPresent">A value indicating whether the expansion bay or module is present.</param>
    /// <param name="isEnabled">A value indicating whether the expansion bay is enabled.</param>
    /// <param name="hasFault">A value indicating whether the expansion bay reports a fault condition.</param>
    /// <param name="isDoorClosed">A value indicating whether the expansion bay door or latch is closed.</param>
    /// <param name="board">The expansion bay board classification.</param>
    /// <param name="vendor">The expansion bay occupant or vendor family classification.</param>
    /// <param name="serialNumber">The reported serial number, when available.</param>
    public FrameworkExpansionBaySnapshot(bool isPresent, bool isEnabled, bool hasFault, bool isDoorClosed, FrameworkExpansionBayBoard board, FrameworkExpansionBayVendor vendor, string serialNumber)
        : this(isPresent ? FrameworkModuleIdentity.ExpansionBay : FrameworkModuleIdentity.None, isPresent, isEnabled, hasFault, isDoorClosed, board, vendor, FrameworkExpansionBayPcieConfiguration.Unknown, serialNumber)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkExpansionBaySnapshot"/> class.
    /// </summary>
    /// <param name="isPresent">A value indicating whether the expansion bay or module is present.</param>
    /// <param name="isEnabled">A value indicating whether the expansion bay is enabled.</param>
    /// <param name="hasFault">A value indicating whether the expansion bay reports a fault condition.</param>
    /// <param name="isDoorClosed">A value indicating whether the expansion bay door or latch is closed.</param>
    /// <param name="board">The expansion bay board classification.</param>
    /// <param name="vendor">The expansion bay occupant or vendor family classification.</param>
    /// <param name="pcieConfiguration">The reported PCIe lane and speed configuration.</param>
    /// <param name="serialNumber">The reported serial number, when available.</param>
    internal FrameworkExpansionBaySnapshot(bool isPresent, bool isEnabled, bool hasFault, bool isDoorClosed, FrameworkExpansionBayBoard board, FrameworkExpansionBayVendor vendor, FrameworkExpansionBayPcieConfiguration pcieConfiguration, string serialNumber)
        : this(isPresent ? FrameworkModuleIdentity.ExpansionBay : FrameworkModuleIdentity.None, isPresent, isEnabled, hasFault, isDoorClosed, board, vendor, pcieConfiguration, serialNumber)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkExpansionBaySnapshot"/> class.
    /// </summary>
    /// <param name="identity">The best-effort expansion-bay classification.</param>
    /// <param name="isPresent">A value indicating whether the expansion bay or module is present.</param>
    /// <param name="isEnabled">A value indicating whether the expansion bay is enabled.</param>
    /// <param name="hasFault">A value indicating whether the expansion bay reports a fault condition.</param>
    /// <param name="isDoorClosed">A value indicating whether the expansion bay door or latch is closed.</param>
    /// <param name="board">The expansion bay board classification.</param>
    /// <param name="vendor">The expansion bay occupant or vendor family classification.</param>
    /// <param name="pcieConfiguration">The reported PCIe configuration.</param>
    /// <param name="serialNumber">The reported serial number, when available.</param>
    protected FrameworkExpansionBaySnapshot(FrameworkModuleIdentity identity, bool isPresent, bool isEnabled, bool hasFault, bool isDoorClosed, FrameworkExpansionBayBoard board, FrameworkExpansionBayVendor vendor, FrameworkExpansionBayPcieConfiguration pcieConfiguration, string serialNumber)
    {
        Identity = identity;
        IsPresent = isPresent;
        IsEnabled = isEnabled;
        HasFault = hasFault;
        IsDoorClosed = isDoorClosed;
        Board = board;
        Vendor = vendor;
        RawPcieConfiguration = pcieConfiguration;
        SerialNumber = serialNumber;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkExpansionBaySnapshot"/> class by copying another snapshot and applying a refined classification.
    /// </summary>
    /// <param name="snapshot">The snapshot to copy.</param>
    /// <param name="identity">The best-effort expansion-bay classification.</param>
    protected FrameworkExpansionBaySnapshot(FrameworkExpansionBaySnapshot snapshot, FrameworkModuleIdentity identity)
    {
        ArgumentNullException.ThrowIfNull(snapshot);

        Identity = identity;
        IsPresent = snapshot.IsPresent;
        IsEnabled = snapshot.IsEnabled;
        HasFault = snapshot.HasFault;
        IsDoorClosed = snapshot.IsDoorClosed;
        Board = snapshot.Board;
        Vendor = snapshot.Vendor;
        RawPcieConfiguration = snapshot.RawPcieConfiguration;
        SerialNumber = snapshot.SerialNumber;
    }

    /// <summary>
    /// Gets the best-effort expansion-bay classification.
    /// </summary>
    public FrameworkModuleIdentity Identity { get; init; }

    /// <summary>
    /// Gets a value indicating whether the expansion bay or module is present.
    /// </summary>
    public bool IsPresent { get; init; }

    /// <summary>
    /// Gets a value indicating whether the expansion bay is enabled.
    /// </summary>
    public bool IsEnabled { get; init; }

    /// <summary>
    /// Gets a value indicating whether the expansion bay reports a fault condition.
    /// </summary>
    public bool HasFault { get; init; }

    /// <summary>
    /// Gets a value indicating whether the expansion bay door or latch is closed.
    /// </summary>
    public bool IsDoorClosed { get; init; }

    /// <summary>
    /// Gets the expansion bay board classification.
    /// </summary>
    public FrameworkExpansionBayBoard Board { get; init; }

    /// <summary>
    /// Gets the expansion bay occupant or vendor family classification.
    /// </summary>
    public FrameworkExpansionBayVendor Vendor { get; init; }

    /// <summary>
    /// Gets the reported serial number, when available.
    /// </summary>
    public string SerialNumber { get; init; }

    /// <summary>
    /// Gets the raw PCIe configuration value reported by the EC before managed classification.
    /// </summary>
    internal FrameworkExpansionBayPcieConfiguration RawPcieConfiguration { get; init; }

    public override string ToString()
    {
        return $"Expansion Bay Snapshot: Identity: {Identity}, Present: {IsPresent}, Enabled: {IsEnabled}, Fault: {HasFault}, Door Closed: {IsDoorClosed}, Board: {Board}, Vendor: {Vendor}, Serial Number: {SerialNumber}";
    }
}
