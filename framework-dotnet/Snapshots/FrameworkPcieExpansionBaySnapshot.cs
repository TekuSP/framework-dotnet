using FrameworkDotnet.Attributes;
using FrameworkDotnet.Enums;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents an expansion-bay snapshot that uses the expansion-bay PCIe path.
/// </summary>
[FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "Upstream framework-system currently documents expansion-bay inventory classifications on Framework Laptop 16 only.")]
public abstract record FrameworkPcieExpansionBaySnapshot : FrameworkExpansionBaySnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkPcieExpansionBaySnapshot"/> class.
    /// </summary>
    /// <param name="snapshot">The snapshot to classify.</param>
    /// <param name="identity">The PCIe-path expansion-bay classification.</param>
    protected FrameworkPcieExpansionBaySnapshot(FrameworkExpansionBaySnapshot snapshot, FrameworkModuleIdentity identity)
        : base(snapshot, identity)
    {
        PcieConfiguration = snapshot.RawPcieConfiguration;
    }

    /// <summary>
    /// Gets the reported expansion-bay PCIe lane and generation configuration.
    /// </summary>
    public FrameworkExpansionBayPcieConfiguration PcieConfiguration { get; init; }

    public override string ToString()
    {
        return $"{base.ToString()}, PCIe Configuration: {PcieConfiguration}";
    }
}
