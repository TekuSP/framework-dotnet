using System;
using System.Collections.Generic;
using System.Globalization;

using UnitsNet;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents the full Smart Battery data set read from the pack over I2C passthrough.
/// </summary>
/// <remarks>
/// <para>
/// Producing this snapshot costs many I2C round trips and is far slower than <see cref="FrameworkPowerSnapshot"/>. Read it on demand only; never place it in a polling loop.
/// </para>
/// <para>
/// <see cref="IsUnsealed"/> reports whether the manufacturer-access register group was unlocked and read. When it is <see langword="false"/>, <see cref="StateOfHealth"/>, <see cref="Safety"/> and <see cref="LifetimeData"/> are <see langword="null"/> rather than zero-filled.
/// </para>
/// </remarks>
public sealed record FrameworkSmartBatterySnapshot
{
    /// <summary>
    /// The <c>CAPACITY_MODE</c> bit of the Smart Battery <c>BatteryMode</c> register. When set, the
    /// capacity registers count 10 mWh units instead of mAh.
    /// </summary>
    private const ushort CapacityModeMask = 0x8000;

    /// <summary>
    /// The watt-hours one capacity unit represents when the pack reports in energy mode.
    /// </summary>
    private const double EnergyUnitWattHours = 0.01;

    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkSmartBatterySnapshot"/> class.
    /// </summary>
    /// <param name="batteryMode">The raw Smart Battery <c>BatteryMode</c> register.</param>
    /// <param name="serialNumber">The pack serial number.</param>
    /// <param name="manufactureDateRaw">The raw packed Smart Battery <c>ManufactureDate</c> word.</param>
    /// <param name="manufactureDate">The decoded manufacture date, or <see langword="null"/> when the pack reported an unusable date.</param>
    /// <param name="deviceName">The pack device name.</param>
    /// <param name="manufacturerName">The pack manufacturer name.</param>
    /// <param name="deviceChemistry">The pack cell chemistry.</param>
    /// <param name="firmwareVersionRaw">The raw manufacturer-access firmware version block.</param>
    /// <param name="temperature">The pack temperature.</param>
    /// <param name="voltage">The pack terminal voltage.</param>
    /// <param name="cellVoltage_1">The first cell voltage.</param>
    /// <param name="cellVoltage_2">The second cell voltage.</param>
    /// <param name="cellVoltage_3">The third cell voltage.</param>
    /// <param name="cellVoltage_4">The fourth cell voltage.</param>
    /// <param name="current">The instantaneous pack current, negative while discharging.</param>
    /// <param name="averageCurrent">The averaged pack current, negative while discharging.</param>
    /// <param name="cycleCount">The number of charge cycles the pack has recorded.</param>
    /// <param name="relativeStateOfCharge">The charge level relative to the full charge capacity of the pack.</param>
    /// <param name="absoluteStateOfCharge">The charge level relative to the design capacity of the pack.</param>
    /// <param name="remainingCapacityRaw">The raw Smart Battery <c>RemainingCapacity</c> register, in whichever unit <paramref name="batteryMode"/> selects.</param>
    /// <param name="fullChargeCapacityRaw">The raw Smart Battery <c>FullChargeCapacity</c> register, in whichever unit <paramref name="batteryMode"/> selects.</param>
    /// <param name="designCapacityRaw">The raw Smart Battery <c>DesignCapacity</c> register, in whichever unit <paramref name="batteryMode"/> selects.</param>
    /// <param name="designVoltage">The voltage the pack was designed for.</param>
    /// <param name="chargingCurrent">The charging current the pack is currently requesting.</param>
    /// <param name="chargingVoltage">The charging voltage the pack is currently requesting.</param>
    /// <param name="batteryStatus">The raw Smart Battery <c>BatteryStatus</c> register.</param>
    /// <param name="isUnsealed">A value indicating whether the manufacturer-access register group was unlocked and read.</param>
    /// <param name="stateOfHealth">The state-of-health block, or <see langword="null"/> when the pack was not unsealed.</param>
    /// <param name="safety">The operation, safety and permanent-failure words, or <see langword="null"/> when the pack was not unsealed.</param>
    /// <param name="lifetimeData">The raw lifetime data blocks, or <see langword="null"/> when the pack was not unsealed.</param>
    public FrameworkSmartBatterySnapshot(
        ushort batteryMode,
        ushort serialNumber,
        ushort manufactureDateRaw,
        DateOnly? manufactureDate,
        string deviceName,
        string manufacturerName,
        string deviceChemistry,
        IReadOnlyList<byte> firmwareVersionRaw,
        Temperature temperature,
        ElectricPotential voltage,
        ElectricPotential cellVoltage_1,
        ElectricPotential cellVoltage_2,
        ElectricPotential cellVoltage_3,
        ElectricPotential cellVoltage_4,
        ElectricCurrent current,
        ElectricCurrent averageCurrent,
        uint cycleCount,
        Ratio relativeStateOfCharge,
        Ratio absoluteStateOfCharge,
        ushort remainingCapacityRaw,
        ushort fullChargeCapacityRaw,
        ushort designCapacityRaw,
        ElectricPotential designVoltage,
        ElectricCurrent chargingCurrent,
        ElectricPotential chargingVoltage,
        ushort batteryStatus,
        bool isUnsealed,
        FrameworkBatteryStateOfHealthSnapshot? stateOfHealth,
        FrameworkBatterySafetySnapshot? safety,
        FrameworkBatteryLifetimeDataSnapshot? lifetimeData)
    {
        BatteryMode = batteryMode;
        SerialNumber = serialNumber;
        ManufactureDateRaw = manufactureDateRaw;
        ManufactureDate = manufactureDate;
        DeviceName = deviceName;
        ManufacturerName = manufacturerName;
        DeviceChemistry = deviceChemistry;
        FirmwareVersionRaw = firmwareVersionRaw;
        Temperature = temperature;
        Voltage = voltage;
        CellVoltage_1 = cellVoltage_1;
        CellVoltage_2 = cellVoltage_2;
        CellVoltage_3 = cellVoltage_3;
        CellVoltage_4 = cellVoltage_4;
        Current = current;
        AverageCurrent = averageCurrent;
        CycleCount = cycleCount;
        RelativeStateOfCharge = relativeStateOfCharge;
        AbsoluteStateOfCharge = absoluteStateOfCharge;
        RemainingCapacityRaw = remainingCapacityRaw;
        FullChargeCapacityRaw = fullChargeCapacityRaw;
        DesignCapacityRaw = designCapacityRaw;
        DesignVoltage = designVoltage;
        ChargingCurrent = chargingCurrent;
        ChargingVoltage = chargingVoltage;
        BatteryStatus = batteryStatus;
        IsUnsealed = isUnsealed;
        StateOfHealth = stateOfHealth;
        Safety = safety;
        LifetimeData = lifetimeData;
    }

    /// <summary>
    /// Gets the raw Smart Battery <c>BatteryMode</c> register.
    /// </summary>
    /// <remarks>
    /// Bit 15 is the Smart Battery <c>CAPACITY_MODE</c> selector, decoded for you as <see cref="IsCapacityReportedInEnergyUnits"/>. It chooses which of the two parallel capacity property sets this snapshot populates, so callers do not need to test it themselves.
    /// </remarks>
    public ushort BatteryMode { get; init; }

    /// <summary>
    /// Gets the pack serial number.
    /// </summary>
    public ushort SerialNumber { get; init; }

    /// <summary>
    /// Gets the raw packed Smart Battery <c>ManufactureDate</c> word.
    /// </summary>
    public ushort ManufactureDateRaw { get; init; }

    /// <summary>
    /// Gets the decoded manufacture date, or <see langword="null"/> when the pack reported an unusable date.
    /// </summary>
    public DateOnly? ManufactureDate { get; init; }

    /// <summary>
    /// Gets the pack device name.
    /// </summary>
    public string DeviceName { get; init; }

    /// <summary>
    /// Gets the pack manufacturer name.
    /// </summary>
    public string ManufacturerName { get; init; }

    /// <summary>
    /// Gets the pack cell chemistry, for example <c>LION</c>.
    /// </summary>
    public string DeviceChemistry { get; init; }

    /// <summary>
    /// Gets the raw manufacturer-access firmware version block.
    /// </summary>
    /// <remarks>
    /// This is the unmodified response to manufacturer-access sub-command <c>0x0002</c>, which carries the sub-command echo, the device number, the firmware version and the build. The field widths are gas-gauge specific, so the bytes are surfaced verbatim rather than decoded. The block is empty when the pack did not answer.
    /// </remarks>
    public IReadOnlyList<byte> FirmwareVersionRaw { get; init; }

    /// <summary>
    /// Gets the pack temperature.
    /// </summary>
    public Temperature Temperature { get; init; }

    /// <summary>
    /// Gets the pack terminal voltage.
    /// </summary>
    public ElectricPotential Voltage { get; init; }

    /// <summary>
    /// Gets the first cell voltage.
    /// </summary>
    public ElectricPotential CellVoltage_1 { get; init; }

    /// <summary>
    /// Gets the second cell voltage.
    /// </summary>
    public ElectricPotential CellVoltage_2 { get; init; }

    /// <summary>
    /// Gets the third cell voltage.
    /// </summary>
    public ElectricPotential CellVoltage_3 { get; init; }

    /// <summary>
    /// Gets the fourth cell voltage.
    /// </summary>
    public ElectricPotential CellVoltage_4 { get; init; }

    /// <summary>
    /// Gets the instantaneous pack current. The value is negative while the pack is discharging.
    /// </summary>
    public ElectricCurrent Current { get; init; }

    /// <summary>
    /// Gets the averaged pack current. The value is negative while the pack is discharging.
    /// </summary>
    public ElectricCurrent AverageCurrent { get; init; }

    /// <summary>
    /// Gets the number of charge cycles the pack has recorded.
    /// </summary>
    public uint CycleCount { get; init; }

    /// <summary>
    /// Gets the charge level relative to the full charge capacity of the pack.
    /// </summary>
    public Ratio RelativeStateOfCharge { get; init; }

    /// <summary>
    /// Gets the charge level relative to the design capacity of the pack.
    /// </summary>
    public Ratio AbsoluteStateOfCharge { get; init; }

    /// <summary>
    /// Gets a value indicating whether the pack reports its three capacity registers in energy
    /// units rather than charge units.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if the <c>CAPACITY_MODE</c> bit of <see cref="BatteryMode"/> is set,
    /// so the capacity registers count 10 mWh units; otherwise, <see langword="false"/>, so they
    /// count mAh.
    /// </value>
    /// <remarks>
    /// This selects which of the two parallel capacity property sets is populated: the
    /// <see cref="ElectricCharge"/> ones when <see langword="false"/>, the <see cref="Energy"/>
    /// ones when <see langword="true"/>. The unset set is <see langword="null"/>.
    /// </remarks>
    public bool IsCapacityReportedInEnergyUnits => (BatteryMode & CapacityModeMask) != 0;

    /// <summary>
    /// Gets the raw Smart Battery <c>RemainingCapacity</c> register.
    /// </summary>
    /// <value>The register value, in mAh or in 10 mWh units according to <see cref="IsCapacityReportedInEnergyUnits"/>.</value>
    public ushort RemainingCapacityRaw { get; init; }

    /// <summary>
    /// Gets the raw Smart Battery <c>FullChargeCapacity</c> register.
    /// </summary>
    /// <value>The register value, in mAh or in 10 mWh units according to <see cref="IsCapacityReportedInEnergyUnits"/>.</value>
    public ushort FullChargeCapacityRaw { get; init; }

    /// <summary>
    /// Gets the raw Smart Battery <c>DesignCapacity</c> register.
    /// </summary>
    /// <value>The register value, in mAh or in 10 mWh units according to <see cref="IsCapacityReportedInEnergyUnits"/>.</value>
    public ushort DesignCapacityRaw { get; init; }

    /// <summary>
    /// Gets the remaining capacity as a charge.
    /// </summary>
    /// <value>The remaining charge, or <see langword="null"/> when <see cref="IsCapacityReportedInEnergyUnits"/> is <see langword="true"/>.</value>
    /// <seealso cref="RemainingEnergy"/>
    public ElectricCharge? RemainingCapacity => IsCapacityReportedInEnergyUnits
        ? null
        : ElectricCharge.FromMilliampereHours(RemainingCapacityRaw);

    /// <summary>
    /// Gets the capacity of the pack when fully charged, as a charge.
    /// </summary>
    /// <value>The full charge, or <see langword="null"/> when <see cref="IsCapacityReportedInEnergyUnits"/> is <see langword="true"/>.</value>
    /// <seealso cref="FullChargeEnergy"/>
    public ElectricCharge? FullChargeCapacity => IsCapacityReportedInEnergyUnits
        ? null
        : ElectricCharge.FromMilliampereHours(FullChargeCapacityRaw);

    /// <summary>
    /// Gets the capacity the pack was designed for, as a charge.
    /// </summary>
    /// <value>The design charge, or <see langword="null"/> when <see cref="IsCapacityReportedInEnergyUnits"/> is <see langword="true"/>.</value>
    /// <seealso cref="DesignEnergy"/>
    public ElectricCharge? DesignCapacity => IsCapacityReportedInEnergyUnits
        ? null
        : ElectricCharge.FromMilliampereHours(DesignCapacityRaw);

    /// <summary>
    /// Gets the remaining capacity as an energy.
    /// </summary>
    /// <value>The remaining energy, or <see langword="null"/> when <see cref="IsCapacityReportedInEnergyUnits"/> is <see langword="false"/>.</value>
    /// <seealso cref="RemainingCapacity"/>
    public Energy? RemainingEnergy => IsCapacityReportedInEnergyUnits
        ? Energy.FromWattHours(RemainingCapacityRaw * EnergyUnitWattHours)
        : null;

    /// <summary>
    /// Gets the energy the pack holds when fully charged.
    /// </summary>
    /// <value>The full charge energy, or <see langword="null"/> when <see cref="IsCapacityReportedInEnergyUnits"/> is <see langword="false"/>.</value>
    /// <seealso cref="FullChargeCapacity"/>
    public Energy? FullChargeEnergy => IsCapacityReportedInEnergyUnits
        ? Energy.FromWattHours(FullChargeCapacityRaw * EnergyUnitWattHours)
        : null;

    /// <summary>
    /// Gets the energy the pack was designed for.
    /// </summary>
    /// <value>The design energy, or <see langword="null"/> when <see cref="IsCapacityReportedInEnergyUnits"/> is <see langword="false"/>.</value>
    /// <seealso cref="DesignCapacity"/>
    public Energy? DesignEnergy => IsCapacityReportedInEnergyUnits
        ? Energy.FromWattHours(DesignCapacityRaw * EnergyUnitWattHours)
        : null;

    /// <summary>
    /// Gets the voltage the pack was designed for.
    /// </summary>
    public ElectricPotential DesignVoltage { get; init; }

    /// <summary>
    /// Gets the charging current the pack is currently requesting.
    /// </summary>
    public ElectricCurrent ChargingCurrent { get; init; }

    /// <summary>
    /// Gets the charging voltage the pack is currently requesting.
    /// </summary>
    public ElectricPotential ChargingVoltage { get; init; }

    /// <summary>
    /// Gets the raw Smart Battery <c>BatteryStatus</c> register.
    /// </summary>
    public ushort BatteryStatus { get; init; }

    /// <summary>
    /// Gets a value indicating whether the manufacturer-access register group was unlocked and read.
    /// </summary>
    /// <remarks>
    /// When this is <see langword="false"/> the pack answered in sealed mode and <see cref="StateOfHealth"/>, <see cref="Safety"/> and <see cref="LifetimeData"/> are <see langword="null"/>.
    /// </remarks>
    public bool IsUnsealed { get; init; }

    /// <summary>
    /// Gets the state-of-health block, or <see langword="null"/> when the pack was not unsealed.
    /// </summary>
    public FrameworkBatteryStateOfHealthSnapshot? StateOfHealth { get; init; }

    /// <summary>
    /// Gets the operation, safety and permanent-failure words, or <see langword="null"/> when the pack was not unsealed.
    /// </summary>
    public FrameworkBatterySafetySnapshot? Safety { get; init; }

    /// <summary>
    /// Gets the raw lifetime data blocks, or <see langword="null"/> when the pack was not unsealed.
    /// </summary>
    public FrameworkBatteryLifetimeDataSnapshot? LifetimeData { get; init; }

    /// <summary>
    /// Gets the four cell voltages in index order.
    /// </summary>
    public IReadOnlyList<ElectricPotential> CellVoltages => [CellVoltage_1, CellVoltage_2, CellVoltage_3, CellVoltage_4];

    public override string ToString()
    {
        string manufactureDate = ManufactureDate.HasValue ? ManufactureDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) : "unknown";
        string stateOfHealth = StateOfHealth is null ? "sealed" : StateOfHealth.ToString();
        string safety = Safety is null ? "sealed" : Safety.ToString();
        string lifetimeData = LifetimeData is null ? "sealed" : LifetimeData.ToString();

        static string FormatCapacity(ElectricCharge? charge, Energy? energy)
        {
            if (charge.HasValue)
            {
                return charge.Value.ToString(CultureInfo.InvariantCulture);
            }

            return energy.HasValue ? energy.Value.ToString(CultureInfo.InvariantCulture) : "unavailable";
        }

        (string remaining, string full, string design) capacities = (
            FormatCapacity(RemainingCapacity, RemainingEnergy),
            FormatCapacity(FullChargeCapacity, FullChargeEnergy),
            FormatCapacity(DesignCapacity, DesignEnergy));

        return $"Smart Battery: {ManufacturerName} {DeviceName} (SN: {SerialNumber.ToString(CultureInfo.InvariantCulture)}), Chemistry: {DeviceChemistry}, Manufactured: {manufactureDate}, Temperature: {Temperature.ToString(CultureInfo.InvariantCulture)}, Voltage: {Voltage.ToString(CultureInfo.InvariantCulture)}, Cell Voltages: {string.Join(", ", CellVoltages)}, Current: {Current.ToString(CultureInfo.InvariantCulture)}, Average Current: {AverageCurrent.ToString(CultureInfo.InvariantCulture)}, Cycle Count: {CycleCount.ToString(CultureInfo.InvariantCulture)}, Relative Charge: {RelativeStateOfCharge.ToString(CultureInfo.InvariantCulture)}, Absolute Charge: {AbsoluteStateOfCharge.ToString(CultureInfo.InvariantCulture)}, Remaining Capacity: {capacities.remaining}, Full Charge Capacity: {capacities.full}, Design Capacity: {capacities.design}, Design Voltage: {DesignVoltage.ToString(CultureInfo.InvariantCulture)}, Charging Current: {ChargingCurrent.ToString(CultureInfo.InvariantCulture)}, Charging Voltage: {ChargingVoltage.ToString(CultureInfo.InvariantCulture)}, Battery Mode: 0x{BatteryMode.ToString("X4", CultureInfo.InvariantCulture)}, Battery Status: 0x{BatteryStatus.ToString("X4", CultureInfo.InvariantCulture)}, Unsealed: {IsUnsealed}, State Of Health: {stateOfHealth}, Safety: {safety}, Lifetime Data: {lifetimeData}";
    }
}
