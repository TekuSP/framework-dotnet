using System.Globalization;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents the chassis intrusion state reported by the EC.
/// </summary>
public sealed record FrameworkChassisIntrusionSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkChassisIntrusionSnapshot"/> class.
    /// </summary>
    /// <param name="currentlyOpen">A value indicating whether the chassis is currently open.</param>
    /// <param name="coinCellEverRemoved">A value indicating whether the coin cell battery was ever removed.</param>
    /// <param name="everOpened">A value indicating whether the chassis was ever opened.</param>
    /// <param name="totalOpened">The total number of times the chassis was opened as reported by EC memory.</param>
    /// <param name="vtrOpenCount">The number of times the chassis was opened since the last VTR (voltage-retained) reset.</param>
    public FrameworkChassisIntrusionSnapshot(bool currentlyOpen, bool coinCellEverRemoved, bool everOpened, byte totalOpened, byte vtrOpenCount)
    {
        CurrentlyOpen = currentlyOpen;
        CoinCellEverRemoved = coinCellEverRemoved;
        EverOpened = everOpened;
        TotalOpened = totalOpened;
        VtrOpenCount = vtrOpenCount;
    }

    /// <summary>
    /// Gets a value indicating whether the chassis is currently open.
    /// </summary>
    public bool CurrentlyOpen { get; init; }

    /// <summary>
    /// Gets a value indicating whether the coin cell battery was ever removed.
    /// </summary>
    public bool CoinCellEverRemoved { get; init; }

    /// <summary>
    /// Gets a value indicating whether the chassis was ever opened.
    /// </summary>
    public bool EverOpened { get; init; }

    /// <summary>
    /// Gets the total number of times the chassis was opened as reported by EC memory.
    /// </summary>
    public byte TotalOpened { get; init; }

    /// <summary>
    /// Gets the number of times the chassis was opened since the last VTR (voltage-retained) reset.
    /// </summary>
    public byte VtrOpenCount { get; init; }

    public override string ToString()
    {
        return $"Chassis Intrusion: Currently Open: {CurrentlyOpen}, Coin Cell Ever Removed: {CoinCellEverRemoved}, Ever Opened: {EverOpened}, Total Opened: {TotalOpened.ToString(CultureInfo.InvariantCulture)}, VTR Open Count: {VtrOpenCount.ToString(CultureInfo.InvariantCulture)}";
    }
}
