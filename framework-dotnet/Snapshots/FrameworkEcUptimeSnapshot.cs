using System;
using System.Globalization;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents the EC uptime and reset counters reported by the EC.
/// </summary>
public sealed record FrameworkEcUptimeSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkEcUptimeSnapshot"/> class.
    /// </summary>
    /// <param name="timeSinceEcBoot">The elapsed time since the EC last booted.</param>
    /// <param name="apResetsSinceEcBoot">The number of AP (application processor) resets since the EC last booted.</param>
    /// <param name="ecResetFlags">The raw EC reset flags bitmask from the last EC reset event.</param>
    public FrameworkEcUptimeSnapshot(TimeSpan timeSinceEcBoot, uint apResetsSinceEcBoot, uint ecResetFlags)
    {
        TimeSinceEcBoot = timeSinceEcBoot;
        ApResetsSinceEcBoot = apResetsSinceEcBoot;
        EcResetFlags = ecResetFlags;
    }

    /// <summary>
    /// Gets the elapsed time since the EC last booted.
    /// </summary>
    public TimeSpan TimeSinceEcBoot { get; init; }

    /// <summary>
    /// Gets the number of AP (application processor) resets since the EC last booted.
    /// </summary>
    public uint ApResetsSinceEcBoot { get; init; }

    /// <summary>
    /// Gets the raw EC reset flags bitmask from the last EC reset event.
    /// </summary>
    public uint EcResetFlags { get; init; }

    public override string ToString()
    {
        return $"EC Uptime: Time Since Boot: {TimeSinceEcBoot}, AP Resets: {ApResetsSinceEcBoot.ToString(CultureInfo.InvariantCulture)}, EC Reset Flags: 0x{EcResetFlags.ToString("X8", CultureInfo.InvariantCulture)}";
    }
}
