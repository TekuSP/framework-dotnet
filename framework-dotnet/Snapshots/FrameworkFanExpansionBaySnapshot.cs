using FrameworkDotnet.Attributes;
using FrameworkDotnet.Enums;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents a fan-oriented expansion-bay snapshot.
/// </summary>
[FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "Upstream framework-system currently documents expansion-bay inventory classifications on Framework Laptop 16 only.")]
public abstract record FrameworkFanExpansionBaySnapshot : FrameworkExpansionBaySnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkFanExpansionBaySnapshot"/> class.
    /// </summary>
    /// <param name="snapshot">The snapshot to classify.</param>
    /// <param name="identity">The fan-oriented classification.</param>
    protected FrameworkFanExpansionBaySnapshot(FrameworkExpansionBaySnapshot snapshot, FrameworkModuleIdentity identity)
        : base(snapshot, identity)
    {
    }
}
