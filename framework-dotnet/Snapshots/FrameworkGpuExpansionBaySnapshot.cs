using System;
using System.Collections.Generic;

using FrameworkDotnet.Attributes;
using FrameworkDotnet.Enums;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents a GPU-oriented expansion-bay snapshot.
/// </summary>
/// <remarks>
/// GPU descriptor details are attached when they are available from the GPU descriptor readback surface, such as via <see cref="FrameworkDotnet.Interfaces.IFrameworkEcConnection.GetExpansionBayModulesSnapshot"/>.
/// </remarks>
[FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "Upstream framework-system currently documents expansion-bay inventory classifications on Framework Laptop 16 only.")]
public abstract record FrameworkGpuExpansionBaySnapshot : FrameworkPcieExpansionBaySnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkGpuExpansionBaySnapshot"/> class.
    /// </summary>
    /// <param name="snapshot">The snapshot to classify.</param>
    /// <param name="identity">The GPU-oriented classification.</param>
    protected FrameworkGpuExpansionBaySnapshot(FrameworkExpansionBaySnapshot snapshot, FrameworkModuleIdentity identity)
        : base(snapshot, identity)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkGpuExpansionBaySnapshot"/> class.
    /// </summary>
    /// <param name="snapshot">The snapshot to classify.</param>
    /// <param name="identity">The GPU-oriented classification.</param>
    /// <param name="gpuDescriptor">The parsed GPU descriptor details.</param>
    private protected FrameworkGpuExpansionBaySnapshot(FrameworkExpansionBaySnapshot snapshot, FrameworkModuleIdentity identity, (IReadOnlyList<byte> RawMagicBytes, FrameworkGpuDescriptorMagic BayType, Version DescriptorVersion, Version HardwareVersion, string Serial, IReadOnlyList<byte> Header, IReadOnlyList<byte> Payload) gpuDescriptor)
        : base(snapshot, identity)
    {
        ArgumentNullException.ThrowIfNull(gpuDescriptor.RawMagicBytes);
        ArgumentNullException.ThrowIfNull(gpuDescriptor.DescriptorVersion);
        ArgumentNullException.ThrowIfNull(gpuDescriptor.HardwareVersion);
        ArgumentNullException.ThrowIfNull(gpuDescriptor.Serial);
        ArgumentNullException.ThrowIfNull(gpuDescriptor.Header);
        ArgumentNullException.ThrowIfNull(gpuDescriptor.Payload);

        GpuDescriptorRawMagicBytes = gpuDescriptor.RawMagicBytes;
        GpuDescriptorBayType = gpuDescriptor.BayType;
        GpuDescriptorVersion = gpuDescriptor.DescriptorVersion;
        GpuDescriptorHardwareVersion = gpuDescriptor.HardwareVersion;
        GpuDescriptorSerial = gpuDescriptor.Serial;
        GpuDescriptorHeader = gpuDescriptor.Header;
        GpuDescriptorPayload = gpuDescriptor.Payload;
    }

    /// <summary>
    /// Gets a value indicating whether GPU descriptor details are attached to this snapshot.
    /// </summary>
    public bool HasGpuDescriptor => GpuDescriptorRawMagicBytes is not null;

    /// <summary>
    /// Gets the raw four-byte GPU descriptor signature, when available.
    /// </summary>
    public IReadOnlyList<byte>? GpuDescriptorRawMagicBytes { get; init; }

    /// <summary>
    /// Gets the expansion-bay type derived from the GPU descriptor header magic, when available.
    /// </summary>
    public FrameworkGpuDescriptorMagic? GpuDescriptorBayType { get; init; }

    /// <summary>
    /// Gets the GPU descriptor format version, when available.
    /// </summary>
    public Version? GpuDescriptorVersion { get; init; }

    /// <summary>
    /// Gets the GPU descriptor hardware version, when available.
    /// </summary>
    public Version? GpuDescriptorHardwareVersion { get; init; }

    /// <summary>
    /// Gets the decoded GPU descriptor serial value, when available.
    /// </summary>
    public string? GpuDescriptorSerial { get; init; }

    /// <summary>
    /// Gets the raw GPU descriptor header bytes, when available.
    /// </summary>
    public IReadOnlyList<byte>? GpuDescriptorHeader { get; init; }

    /// <summary>
    /// Gets the raw GPU descriptor payload bytes following the fixed header, when available.
    /// </summary>
    public IReadOnlyList<byte>? GpuDescriptorPayload { get; init; }

    public override string ToString()
    {
        string gpuDescriptorDisplay = !HasGpuDescriptor
            ? "<unavailable>"
            : $"Bay Type: {GpuDescriptorBayType}, Descriptor Version: {GpuDescriptorVersion}, Hardware Version: {GpuDescriptorHardwareVersion}, Serial: {GpuDescriptorSerial}";

        return $"{base.ToString()}, GPU Descriptor: {gpuDescriptorDisplay}";
    }
}
