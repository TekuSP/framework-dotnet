using FrameworkDotnet.Attributes;
using FrameworkDotnet.Enums;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents a PCIe-accessory expansion-bay snapshot.
/// </summary>
[FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "This expansion-bay snapshot classification is specific to Framework Laptop 16 inventory.")]
public sealed record FrameworkPcieAccessoryExpansionBaySnapshot : FrameworkPcieExpansionBaySnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkPcieAccessoryExpansionBaySnapshot"/> class.
    /// </summary>
    /// <param name="snapshot">The snapshot to classify.</param>
    public FrameworkPcieAccessoryExpansionBaySnapshot(FrameworkExpansionBaySnapshot snapshot)
        : base(snapshot, FrameworkModuleIdentity.ExpansionBayPcieAccessory)
    {
    }
}
