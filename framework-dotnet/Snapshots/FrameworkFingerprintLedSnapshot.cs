using System.Globalization;

using FrameworkDotnet.Enums;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents a fingerprint LED snapshot returned by the EC.
/// </summary>
public sealed record FrameworkFingerprintLedSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkFingerprintLedSnapshot"/> class.
    /// </summary>
    /// <param name="rawLevel">The raw firmware-reported LED level byte.</param>
    /// <param name="level">The interpreted LED level or mode.</param>
    public FrameworkFingerprintLedSnapshot(byte rawLevel, FrameworkFingerprintLedLevel level)
    {
        RawLevel = rawLevel;
        Level = level;
    }

    /// <summary>
    /// Gets the raw firmware-reported LED level byte.
    /// </summary>
    public byte RawLevel { get; init; }

    /// <summary>
    /// Gets the interpreted LED level or mode.
    /// </summary>
    public FrameworkFingerprintLedLevel Level { get; init; }

    public override string ToString()
    {
        return $"Fingerprint LED Snapshot: Level: {Level}, Raw Level: {RawLevel.ToString(CultureInfo.InvariantCulture)}";
    }
}
