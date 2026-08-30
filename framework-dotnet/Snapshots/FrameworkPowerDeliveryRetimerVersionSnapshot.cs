using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents the Parade retimer firmware version reported by the embedded controller.
/// </summary>
/// <remarks>
/// <para>
/// The retimer sits behind the Framework Laptop 16 expansion-bay discrete GPU. The underlying EC command is the
/// expansion-bay GPU PCIe query, which other platform families reject, so reading this on any family other than
/// <see cref="FrameworkDotnet.Enums.FrameworkPlatformFamily.Framework16"/> raises a
/// <see cref="FrameworkDotnet.Exceptions.FrameworkStatusException"/> rather than returning a not-present reading.
/// </para>
/// <para>
/// On a Framework Laptop 16 whose expansion bay carries no compatible discrete GPU, the query succeeds with
/// <see cref="IsPresent"/> set to <see langword="false"/> and an empty <see cref="Version"/>; that is a normal
/// reading and not an error.
/// </para>
/// <para>
/// <see cref="Version"/> is the raw four-byte register payload read over I2C from the retimer, not text. Upstream
/// renders it as a dot-separated hexadecimal quad, which <see cref="VersionString"/> reproduces.
/// </para>
/// </remarks>
public sealed record FrameworkPowerDeliveryRetimerVersionSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkPowerDeliveryRetimerVersionSnapshot"/> class.
    /// </summary>
    /// <param name="isPresent">A value indicating whether a retimer answered the query.</param>
    /// <param name="version">The raw retimer version register bytes, or an empty sequence when no retimer answered.</param>
    /// <exception cref="ArgumentNullException"><paramref name="version"/> is <see langword="null"/>.</exception>
    public FrameworkPowerDeliveryRetimerVersionSnapshot(bool isPresent, byte[] version)
    {
        ArgumentNullException.ThrowIfNull(version);

        IsPresent = isPresent;
        Version = version;
    }

    /// <summary>
    /// Gets a value indicating whether a retimer answered the query.
    /// </summary>
    /// <value><see langword="true"/> if a retimer answered; otherwise, <see langword="false"/>.</value>
    public bool IsPresent { get; init; }

    /// <summary>
    /// Gets the raw retimer version register bytes.
    /// </summary>
    /// <value>
    /// The four bytes read from the retimer version register, or an empty sequence when no retimer answered.
    /// The firmware may return fewer than four bytes, so callers must not index blindly.
    /// </value>
    public IReadOnlyList<byte> Version { get; init; }

    /// <summary>
    /// Gets the retimer version rendered as a dot-separated hexadecimal quad.
    /// </summary>
    /// <value>
    /// A string such as <c>1.2.A.1F</c>, matching the upstream rendering, or an empty string when no retimer
    /// answered or fewer than four bytes were returned.
    /// </value>
    public string VersionString => Version.Count >= 4
        ? string.Join('.', Version.Take(4).Select(component => component.ToString("X", CultureInfo.InvariantCulture)))
        : string.Empty;

    /// <inheritdoc/>
    public override string ToString()
    {
        return IsPresent
            ? $"Retimer Version: {VersionString}"
            : "Retimer Version: Not Present";
    }
}
