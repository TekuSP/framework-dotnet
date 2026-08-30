using System.Collections.Generic;
using System.Globalization;

using FrameworkDotnet.Enums;

using UnitsNet;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents the host command protocol capabilities reported by the embedded controller.
/// </summary>
public sealed record FrameworkEcProtocolInfoSnapshot
{
    /// <summary>
    /// The number of bits in <see cref="ProtocolVersionMask"/>, and therefore one past the
    /// highest protocol version the mask can describe.
    /// </summary>
    private const int ProtocolVersionBitCount = 32;

    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkEcProtocolInfoSnapshot"/> class.
    /// </summary>
    /// <param name="protocolVersionMask">The bitmask of supported host command protocol versions, where bit N set means version N is supported.</param>
    /// <param name="maxRequestPacketSize">The largest host command request packet the controller accepts.</param>
    /// <param name="maxResponsePacketSize">The largest host command response packet the controller produces.</param>
    /// <param name="flags">The optional protocol capabilities the controller advertises.</param>
    public FrameworkEcProtocolInfoSnapshot(uint protocolVersionMask, Information maxRequestPacketSize, Information maxResponsePacketSize, FrameworkEcProtocolFlag flags)
    {
        ProtocolVersionMask = protocolVersionMask;
        MaxRequestPacketSize = maxRequestPacketSize;
        MaxResponsePacketSize = maxResponsePacketSize;
        Flags = flags;

        var supportedVersions = new List<int>();
        for (var version = 0; version < ProtocolVersionBitCount; version++)
        {
            if ((protocolVersionMask & (1U << version)) != 0U)
            {
                supportedVersions.Add(version);
            }
        }

        SupportedProtocolVersions = supportedVersions;
    }

    /// <summary>
    /// Gets the bitmask of supported host command protocol versions, where bit N set means
    /// version N is supported.
    /// </summary>
    /// <remarks>
    /// This is a plain version bitmask and is unrelated to <see cref="Flags"/>, which describes
    /// optional protocol capabilities.
    /// </remarks>
    public uint ProtocolVersionMask { get; init; }

    /// <summary>
    /// Gets the supported host command protocol versions in ascending order.
    /// </summary>
    public IReadOnlyList<int> SupportedProtocolVersions { get; init; }

    /// <summary>
    /// Gets the largest host command request packet the controller accepts.
    /// </summary>
    public Information MaxRequestPacketSize { get; init; }

    /// <summary>
    /// Gets the largest host command response packet the controller produces.
    /// </summary>
    public Information MaxResponsePacketSize { get; init; }

    /// <summary>
    /// Gets the optional protocol capabilities the controller advertises.
    /// </summary>
    public FrameworkEcProtocolFlag Flags { get; init; }

    /// <summary>
    /// Determines whether the controller supports a given host command protocol version.
    /// </summary>
    /// <param name="version">The protocol version to test.</param>
    /// <returns><see langword="true"/> when <paramref name="version"/> is supported; otherwise <see langword="false"/>.</returns>
    /// <remarks>
    /// Versions outside the range the bitmask can describe are reported as unsupported rather
    /// than rejected, so a caller can probe freely.
    /// </remarks>
    public bool IsProtocolVersionSupported(int version)
    {
        if (version < 0 || version >= ProtocolVersionBitCount)
        {
            return false;
        }

        return (ProtocolVersionMask & (1U << version)) != 0U;
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"EC Protocol Info: Supported Versions: {string.Join(", ", SupportedProtocolVersions)}, Max Request: {MaxRequestPacketSize.Bytes.ToString(CultureInfo.InvariantCulture)} B, Max Response: {MaxResponsePacketSize.Bytes.ToString(CultureInfo.InvariantCulture)} B, Flags: {Flags}";
    }
}
