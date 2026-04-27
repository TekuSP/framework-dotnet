using FrameworkDotnet.Enums;

using System.Globalization;

using UnitsNet;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents a battery snapshot returned by the EC.
/// </summary>
public sealed record FrameworkBatterySnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkBatterySnapshot"/> class.
    /// </summary>
    /// <param name="manufacturer">The battery manufacturer.</param>
    /// <param name="modelNumber">The battery model number.</param>
    /// <param name="serialNumber">The battery serial number.</param>
    /// <param name="batteryType">The battery chemistry or type.</param>
    /// <param name="presentVoltage">The present battery voltage.</param>
    /// <param name="presentRate">The present battery rate.</param>
    /// <param name="remainingCapacity">The remaining battery capacity.</param>
    /// <param name="designCapacity">The design battery capacity.</param>
    /// <param name="designVoltage">The design battery voltage.</param>
    /// <param name="lastFullChargeCapacity">The last full charge capacity.</param>
    /// <param name="cycleCount">The battery cycle count.</param>
    /// <param name="chargeLevel">The battery charge level.</param>
    /// <param name="batteryState">The battery state.</param>
    public FrameworkBatterySnapshot(
        string manufacturer,
        string modelNumber,
        string serialNumber,
        string batteryType,
        ElectricPotential presentVoltage,
        ElectricCurrent presentRate,
        ElectricCharge remainingCapacity,
        ElectricCharge designCapacity,
        ElectricPotential designVoltage,
        ElectricCharge lastFullChargeCapacity,
        uint cycleCount,
        Ratio chargeLevel,
        FrameworkBatteryState batteryState)
    {
        Manufacturer = manufacturer;
        ModelNumber = modelNumber;
        SerialNumber = serialNumber;
        BatteryType = batteryType;
        PresentVoltage = presentVoltage;
        PresentRate = presentRate;
        RemainingCapacity = remainingCapacity;
        DesignCapacity = designCapacity;
        DesignVoltage = designVoltage;
        LastFullChargeCapacity = lastFullChargeCapacity;
        CycleCount = cycleCount;
        ChargeLevel = chargeLevel;
        BatteryState = batteryState;
    }

    /// <summary>
    /// Gets the battery manufacturer.
    /// </summary>
    public string Manufacturer { get; init; }

    /// <summary>
    /// Gets the battery model number.
    /// </summary>
    public string ModelNumber { get; init; }

    /// <summary>
    /// Gets the battery serial number.
    /// </summary>
    public string SerialNumber { get; init; }

    /// <summary>
    /// Gets the battery chemistry or type.
    /// </summary>
    public string BatteryType { get; init; }

    /// <summary>
    /// Gets the present battery voltage.
    /// </summary>
    public ElectricPotential PresentVoltage { get; init; }

    /// <summary>
    /// Gets the present battery rate.
    /// </summary>
    public ElectricCurrent PresentRate { get; init; }

    /// <summary>
    /// Gets the remaining battery capacity.
    /// </summary>
    public ElectricCharge RemainingCapacity { get; init; }

    /// <summary>
    /// Gets the design battery capacity.
    /// </summary>
    public ElectricCharge DesignCapacity { get; init; }

    /// <summary>
    /// Gets the design battery voltage.
    /// </summary>
    public ElectricPotential DesignVoltage { get; init; }

    /// <summary>
    /// Gets the last full charge capacity.
    /// </summary>
    public ElectricCharge LastFullChargeCapacity { get; init; }

    /// <summary>
    /// Gets the battery cycle count.
    /// </summary>
    public uint CycleCount { get; init; }

    /// <summary>
    /// Gets the battery charge level.
    /// </summary>
    public Ratio ChargeLevel { get; init; }

    /// <summary>
    /// Gets the battery state.
    /// </summary>
    public FrameworkBatteryState BatteryState { get; init; }

    public override string ToString()
    {
        return $"Battery: {Manufacturer} {ModelNumber} (SN: {SerialNumber}), Type: {BatteryType}, Voltage: {PresentVoltage.ToString(CultureInfo.InvariantCulture)}, Rate: {PresentRate.ToString(CultureInfo.InvariantCulture)}, Remaining Capacity: {RemainingCapacity.ToString(CultureInfo.InvariantCulture)}, Design Capacity: {DesignCapacity.ToString(CultureInfo.InvariantCulture)}, Design Voltage: {DesignVoltage.ToString(CultureInfo.InvariantCulture)}, Last Full Charge Capacity: {LastFullChargeCapacity.ToString(CultureInfo.InvariantCulture)}, Cycle Count: {CycleCount.ToString(CultureInfo.InvariantCulture)}, Charge Level: {ChargeLevel.ToString(CultureInfo.InvariantCulture)}, State: {BatteryState}";
    }
}
