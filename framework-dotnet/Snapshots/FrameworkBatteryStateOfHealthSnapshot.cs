using System.Collections.Generic;
using System.Globalization;

using UnitsNet;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents the Smart Battery state-of-health block read through manufacturer access.
/// </summary>
/// <remarks>
/// The block is only readable once the pack has been unsealed, so this snapshot is only produced when <see cref="FrameworkSmartBatterySnapshot.IsUnsealed"/> is <see langword="true"/>. The first two little-endian 16-bit words of the block carry the remaining health expressed in milliampere-hours and in centiwatt-hours respectively; anything beyond them is vendor specific and is surfaced only through <see cref="RawData"/>.
/// </remarks>
public sealed record FrameworkBatteryStateOfHealthSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkBatteryStateOfHealthSnapshot"/> class.
    /// </summary>
    /// <param name="chargeCapacity">The state-of-health charge capacity, or <see langword="null"/> when the block was too short to carry it.</param>
    /// <param name="energyCapacity">The state-of-health energy capacity, or <see langword="null"/> when the block was too short to carry it.</param>
    /// <param name="rawData">The complete raw state-of-health block exactly as the pack returned it.</param>
    public FrameworkBatteryStateOfHealthSnapshot(ElectricCharge? chargeCapacity, Energy? energyCapacity, IReadOnlyList<byte> rawData)
    {
        ChargeCapacity = chargeCapacity;
        EnergyCapacity = energyCapacity;
        RawData = rawData;
    }

    /// <summary>
    /// Gets the state-of-health charge capacity, or <see langword="null"/> when the block was too short to carry it.
    /// </summary>
    public ElectricCharge? ChargeCapacity { get; init; }

    /// <summary>
    /// Gets the state-of-health energy capacity, or <see langword="null"/> when the block was too short to carry it.
    /// </summary>
    public Energy? EnergyCapacity { get; init; }

    /// <summary>
    /// Gets the complete raw state-of-health block exactly as the pack returned it.
    /// </summary>
    public IReadOnlyList<byte> RawData { get; init; }

    public override string ToString()
    {
        string chargeCapacity = ChargeCapacity.HasValue ? ChargeCapacity.Value.ToString(CultureInfo.InvariantCulture) : "unavailable";
        string energyCapacity = EnergyCapacity.HasValue ? EnergyCapacity.Value.ToString(CultureInfo.InvariantCulture) : "unavailable";

        return $"Battery State Of Health: Charge Capacity: {chargeCapacity}, Energy Capacity: {energyCapacity}, Raw Bytes: {RawData.Count.ToString(CultureInfo.InvariantCulture)}";
    }
}
