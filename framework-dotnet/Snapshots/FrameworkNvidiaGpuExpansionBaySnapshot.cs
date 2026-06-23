using System;
using System.Collections.Generic;

using FrameworkDotnet.Attributes;
using FrameworkDotnet.Enums;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents an NVIDIA GPU expansion-bay snapshot.
/// </summary>
[FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "This expansion-bay snapshot classification is specific to Framework Laptop 16 inventory.")]
public sealed record FrameworkNvidiaGpuExpansionBaySnapshot : FrameworkGpuExpansionBaySnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkNvidiaGpuExpansionBaySnapshot"/> class.
    /// </summary>
    /// <param name="snapshot">The snapshot to classify.</param>
    public FrameworkNvidiaGpuExpansionBaySnapshot(FrameworkExpansionBaySnapshot snapshot)
        : base(snapshot, FrameworkModuleIdentity.ExpansionBayNvidiaGpu)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkNvidiaGpuExpansionBaySnapshot"/> class with an attached GPU descriptor.
    /// </summary>
    /// <param name="snapshot">The snapshot to classify.</param>
    /// <param name="gpuDescriptor">The parsed GPU descriptor tuple.</param>
    internal FrameworkNvidiaGpuExpansionBaySnapshot(FrameworkExpansionBaySnapshot snapshot, (IReadOnlyList<byte> RawMagicBytes, FrameworkGpuDescriptorMagic BayType, Version DescriptorVersion, Version HardwareVersion, string Serial, IReadOnlyList<byte> Header, IReadOnlyList<byte> Payload) gpuDescriptor)
        : base(snapshot, FrameworkModuleIdentity.ExpansionBayNvidiaGpu, gpuDescriptor)
    {
    }
}
