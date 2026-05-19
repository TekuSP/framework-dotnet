using FrameworkDotnet.Attributes;
using FrameworkDotnet.Enums;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents an interposer-style expansion-bay snapshot.
/// </summary>
[FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "Upstream framework-system currently documents expansion-bay inventory classifications on Framework Laptop 16 only.")]
public abstract record FrameworkInterposerExpansionBaySnapshot : FrameworkExpansionBaySnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkInterposerExpansionBaySnapshot"/> class.
    /// </summary>
    /// <param name="snapshot">The snapshot to classify.</param>
    /// <param name="identity">The interposer classification.</param>
    protected FrameworkInterposerExpansionBaySnapshot(FrameworkExpansionBaySnapshot snapshot, FrameworkModuleIdentity identity)
        : base(snapshot, identity)
    {
    }
}
