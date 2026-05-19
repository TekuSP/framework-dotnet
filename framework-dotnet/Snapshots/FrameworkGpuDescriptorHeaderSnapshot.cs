using System;
using System.Collections.Generic;

using FrameworkDotnet.Attributes;
using FrameworkDotnet.Enums;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents the parsed header portion of a GPU descriptor blob.
/// </summary>
[FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "Upstream framework-system currently documents the expansion-bay GPU descriptor surface on Framework Laptop 16 only.")]
public sealed record FrameworkGpuDescriptorHeaderSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkGpuDescriptorHeaderSnapshot"/> class.
    /// </summary>
    /// <param name="rawMagicBytes">The raw four-byte descriptor signature.</param>
    /// <param name="bayType">The derived expansion bay type classification from the descriptor header magic.</param>
    /// <param name="descriptorVersion">The descriptor format version.</param>
    /// <param name="hardwareVersion">The hardware version reported by the descriptor.</param>
    /// <param name="serial">The decoded descriptor serial value.</param>
    /// <param name="header">The raw descriptor header bytes.</param>
    /// <param name="payload">The raw descriptor payload bytes following the fixed header.</param>
    public FrameworkGpuDescriptorHeaderSnapshot(IReadOnlyList<byte> rawMagicBytes, FrameworkGpuDescriptorMagic bayType, Version descriptorVersion, Version hardwareVersion, string serial, IReadOnlyList<byte> header, IReadOnlyList<byte> payload)
    {
        ArgumentNullException.ThrowIfNull(rawMagicBytes);
        ArgumentNullException.ThrowIfNull(descriptorVersion);
        ArgumentNullException.ThrowIfNull(hardwareVersion);
        ArgumentNullException.ThrowIfNull(serial);
        ArgumentNullException.ThrowIfNull(header);
        ArgumentNullException.ThrowIfNull(payload);

        RawMagicBytes = rawMagicBytes;
        BayType = bayType;
        DescriptorVersion = descriptorVersion;
        HardwareVersion = hardwareVersion;
        Serial = serial;
        Header = header;
        Payload = payload;
    }

    /// <summary>
    /// Gets the raw four-byte descriptor signature.
    /// </summary>
    public IReadOnlyList<byte> RawMagicBytes { get; init; }

    /// <summary>
    /// Gets the derived expansion bay type classification from the descriptor header magic.
    /// </summary>
    public FrameworkGpuDescriptorMagic BayType { get; init; }

    /// <summary>
    /// Gets the descriptor format version.
    /// </summary>
    public Version DescriptorVersion { get; init; }

    /// <summary>
    /// Gets the hardware version reported by the descriptor.
    /// </summary>
    public Version HardwareVersion { get; init; }

    /// <summary>
    /// Gets the decoded descriptor serial value.
    /// </summary>
    public string Serial { get; init; }

    /// <summary>
    /// Gets the raw descriptor header bytes.
    /// </summary>
    public IReadOnlyList<byte> Header { get; init; }

    /// <summary>
    /// Gets the raw descriptor payload bytes following the fixed header.
    /// </summary>
    public IReadOnlyList<byte> Payload { get; init; }

    public override string ToString()
    {
        var rawMagicBytes = RawMagicBytes is byte[] rawMagicArray
            ? rawMagicArray
            : [.. RawMagicBytes];
        var rawMagicDisplay = Convert.ToHexString(rawMagicBytes);
        var bayTypeDisplay = BayType switch
        {
            FrameworkGpuDescriptorMagic.Unknown => rawMagicDisplay,
            FrameworkGpuDescriptorMagic.NoExpansionBay => "No Expansion Bay",
            _ => $"{BayType} ({rawMagicDisplay})",
        };

        var headerBytes = Header is byte[] headerArray
            ? headerArray
            : [.. Header];
        var headerDisplay = Convert.ToHexString(headerBytes);
        var payloadDisplay = Payload.Count == 0
            ? "<none>"
            : Convert.ToHexString(Payload is byte[] payloadArray ? payloadArray : [.. Payload]);

        return $"GPU Descriptor Header Snapshot: Bay Type: {bayTypeDisplay}, Descriptor Version: {DescriptorVersion}, Hardware Version: {HardwareVersion}, Serial: {Serial}, Header: {headerDisplay}, Payload: {payloadDisplay}";
    }
}
