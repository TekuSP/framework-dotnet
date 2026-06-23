using System.Globalization;

using UnitsNet;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents the battery charge limit settings reported by the EC.
/// </summary>
public sealed record FrameworkChargeLimitsSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkChargeLimitsSnapshot"/> class.
    /// </summary>
    /// <param name="minPercent">The minimum charge threshold.</param>
    /// <param name="maxPercent">The maximum charge threshold.</param>
    public FrameworkChargeLimitsSnapshot(Ratio minPercent, Ratio maxPercent)
    {
        MinPercent = minPercent;
        MaxPercent = maxPercent;
    }

    /// <summary>
    /// Gets the minimum charge threshold below which the battery will begin charging.
    /// </summary>
    public Ratio MinPercent { get; init; }

    /// <summary>
    /// Gets the maximum charge threshold above which the battery will stop charging.
    /// </summary>
    public Ratio MaxPercent { get; init; }

    public override string ToString()
    {
        return $"Charge Limits: Min: {MinPercent.Percent.ToString(CultureInfo.InvariantCulture)}%, Max: {MaxPercent.Percent.ToString(CultureInfo.InvariantCulture)}%";
    }
}
