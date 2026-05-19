using FrameworkDotnet.Attributes;
using FrameworkDotnet.Enums;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents an expansion bay snapshot returned by the EC.
/// </summary>
[FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "Upstream framework-system currently documents expansion-bay status support on Framework Laptop 16 only.")]
public sealed record FrameworkExpansionBaySnapshot
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
    /// <param name="pcieConfiguration">The reported PCIe configuration.</param>
    /// <param name="serialNumber">The reported serial number, when available.</param>
    public FrameworkExpansionBaySnapshot(bool isPresent, bool isEnabled, bool hasFault, bool isDoorClosed, FrameworkExpansionBayBoard board, FrameworkExpansionBayVendor vendor, FrameworkGpuPcieConfig pcieConfiguration, string serialNumber)
    {
        IsPresent = isPresent;
        IsEnabled = isEnabled;
        HasFault = hasFault;
        IsDoorClosed = isDoorClosed;
        Board = board;
        Vendor = vendor;
        PcieConfiguration = pcieConfiguration;
        SerialNumber = serialNumber;
    }

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
    /// Gets the reported PCIe configuration.
    /// </summary>
    public FrameworkGpuPcieConfig PcieConfiguration { get; init; }

    /// <summary>
    /// Gets the reported serial number, when available.
    /// </summary>
    public string SerialNumber { get; init; }

    public override string ToString()
    {
        return $"Expansion Bay Snapshot: Present: {IsPresent}, Enabled: {IsEnabled}, Fault: {HasFault}, Door Closed: {IsDoorClosed}, Board: {Board}, Vendor: {Vendor}, PCIe Configuration: {PcieConfiguration}, Serial Number: {SerialNumber}";
    }
}
