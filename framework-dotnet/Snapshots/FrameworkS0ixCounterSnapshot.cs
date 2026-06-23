using System.Globalization;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents the S0ix (modern standby) sleep entry counter reported by the EC.
/// </summary>
public sealed record FrameworkS0ixCounterSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkS0ixCounterSnapshot"/> class.
    /// </summary>
    /// <param name="s0ixCount">The number of times the system has entered S0ix since the last counter reset.</param>
    public FrameworkS0ixCounterSnapshot(uint s0ixCount)
    {
        S0ixCount = s0ixCount;
    }

    /// <summary>
    /// Gets the number of times the system has entered S0ix (modern standby) since the last counter reset.
    /// </summary>
    public uint S0ixCount { get; init; }

    public override string ToString()
    {
        return $"S0ix Counter: {S0ixCount.ToString(CultureInfo.InvariantCulture)}";
    }
}
