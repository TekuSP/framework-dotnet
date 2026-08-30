using System;
using System.Globalization;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents the panic data the embedded controller saved from its last crash.
/// </summary>
/// <remarks>
/// <para>
/// The payload is kept as an opaque blob on purpose: the per-architecture decode structures are
/// private to the upstream firmware headers, so there is no stable managed shape to project them
/// onto. <see cref="Architecture"/> and <see cref="StructVersion"/> identify which decoder a
/// caller would need.
/// </para>
/// <para>
/// A snapshot with an empty <see cref="Data"/> means the controller simply has no stored panic;
/// that is a normal, healthy reading rather than a failure.
/// </para>
/// </remarks>
public sealed record FrameworkEcPanicInfoSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkEcPanicInfoSnapshot"/> class.
    /// </summary>
    /// <param name="data">The raw panic blob exactly as the controller reported it.</param>
    /// <param name="architecture">The architecture tag read from the blob header.</param>
    /// <param name="structVersion">The structure version read from the blob header.</param>
    /// <param name="flags">The panic data flags read from the blob header.</param>
    /// <param name="isValid">A value indicating whether the blob trailer is self-consistent.</param>
    /// <param name="structSize">The structure size the controller reported in the blob trailer.</param>
    /// <param name="magic">The magic value read from the blob trailer.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="data"/> is <see langword="null"/>.</exception>
    public FrameworkEcPanicInfoSnapshot(byte[] data, byte architecture, byte structVersion, byte flags, bool isValid, uint structSize, uint magic)
    {
        ArgumentNullException.ThrowIfNull(data);

        Data = data;
        Architecture = architecture;
        StructVersion = structVersion;
        Flags = flags;
        IsValid = isValid;
        StructSize = structSize;
        Magic = magic;
    }

    /// <summary>
    /// Gets the raw panic blob exactly as the controller reported it.
    /// </summary>
    /// <remarks>
    /// The array is a private copy owned by this snapshot; the native buffer it came from has
    /// already been released.
    /// </remarks>
    public byte[] Data { get; init; }

    /// <summary>
    /// Gets the architecture tag read from the blob header.
    /// </summary>
    /// <remarks>
    /// Selects which per-architecture layout the remainder of <see cref="Data"/> follows.
    /// </remarks>
    public byte Architecture { get; init; }

    /// <summary>
    /// Gets the structure version read from the blob header.
    /// </summary>
    public byte StructVersion { get; init; }

    /// <summary>
    /// Gets the panic data flags read from the blob header.
    /// </summary>
    public byte Flags { get; init; }

    /// <summary>
    /// Gets a value indicating whether the blob trailer is self-consistent.
    /// </summary>
    /// <remarks>
    /// This is <see langword="true"/> only when the trailer magic matches and the reported
    /// <see cref="StructSize"/> agrees with the length of <see cref="Data"/>. When it is
    /// <see langword="false"/> the header and trailer fields should not be trusted.
    /// </remarks>
    public bool IsValid { get; init; }

    /// <summary>
    /// Gets the structure size the controller reported in the blob trailer.
    /// </summary>
    public uint StructSize { get; init; }

    /// <summary>
    /// Gets the magic value read from the blob trailer.
    /// </summary>
    /// <remarks>
    /// A valid trailer carries <c>0x21636E50</c>, the little-endian encoding of "Pnc!".
    /// </remarks>
    public uint Magic { get; init; }

    /// <summary>
    /// Gets a value indicating whether the controller reported any stored panic data at all.
    /// </summary>
    public bool HasPanicData => Data.Length > 0;

    /// <inheritdoc/>
    public override string ToString()
    {
        if (!HasPanicData)
        {
            return "EC Panic Info: No stored panic";
        }

        return $"EC Panic Info: Length: {Data.Length.ToString(CultureInfo.InvariantCulture)} B, Architecture: {Architecture.ToString(CultureInfo.InvariantCulture)}, Struct Version: {StructVersion.ToString(CultureInfo.InvariantCulture)}, Flags: 0x{Flags.ToString("X2", CultureInfo.InvariantCulture)}, Valid: {IsValid}, Struct Size: {StructSize.ToString(CultureInfo.InvariantCulture)}, Magic: 0x{Magic.ToString("X8", CultureInfo.InvariantCulture)}";
    }
}
