using System;
using System.Collections.Generic;

using FrameworkDotnet.Attributes;
using FrameworkDotnet.Enums;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents an AMD GPU expansion-bay snapshot.
/// </summary>
[FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "This expansion-bay snapshot classification is specific to Framework Laptop 16 inventory.")]
public sealed record FrameworkAmdGpuExpansionBaySnapshot : FrameworkGpuExpansionBaySnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkAmdGpuExpansionBaySnapshot"/> class.
    /// </summary>
    /// <param name="snapshot">The snapshot to classify.</param>
    public FrameworkAmdGpuExpansionBaySnapshot(FrameworkExpansionBaySnapshot snapshot)
        : base(snapshot, FrameworkModuleIdentity.ExpansionBayAmdGpu)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkAmdGpuExpansionBaySnapshot"/> class with an attached GPU descriptor.
    /// </summary>
    /// <param name="snapshot">The snapshot to classify.</param>
    /// <param name="gpuDescriptor">The parsed GPU descriptor tuple.</param>
    internal FrameworkAmdGpuExpansionBaySnapshot(FrameworkExpansionBaySnapshot snapshot, (IReadOnlyList<byte> RawMagicBytes, FrameworkGpuDescriptorMagic BayType, Version DescriptorVersion, Version HardwareVersion, string Serial, IReadOnlyList<byte> Header, IReadOnlyList<byte> Payload) gpuDescriptor)
        : base(snapshot, FrameworkModuleIdentity.ExpansionBayAmdGpu, gpuDescriptor)
    {
    }
}
