using FrameworkDotnet.Attributes;
using FrameworkDotnet.Enums;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents a fan-only expansion-bay snapshot.
/// </summary>
[FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "This expansion-bay snapshot classification is specific to Framework Laptop 16 inventory.")]
public sealed record FrameworkFanOnlyExpansionBaySnapshot : FrameworkFanExpansionBaySnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkFanOnlyExpansionBaySnapshot"/> class.
    /// </summary>
    /// <param name="snapshot">The snapshot to classify.</param>
    public FrameworkFanOnlyExpansionBaySnapshot(FrameworkExpansionBaySnapshot snapshot)
        : base(snapshot, FrameworkModuleIdentity.ExpansionBayFanOnly)
    {
    }
}
