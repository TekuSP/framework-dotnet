namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents a power snapshot returned by the EC.
/// </summary>
public sealed record FrameworkPowerSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkPowerSnapshot"/> class.
    /// </summary>
    /// <param name="acPresent">A value indicating whether AC power is present.</param>
    /// <param name="batteryPresent">A value indicating whether a battery is present.</param>
    /// <param name="discharging">A value indicating whether the battery is discharging.</param>
    /// <param name="charging">A value indicating whether the battery is charging.</param>
    /// <param name="levelCritical">A value indicating whether the battery level is critical.</param>
    /// <param name="batteryCount">The number of batteries reported by the device.</param>
    /// <param name="currentBatteryIndex">The current battery index.</param>
    /// <param name="presentVoltage">The present voltage.</param>
    /// <param name="presentRate">The present rate.</param>
    /// <param name="remainingCapacity">The remaining capacity.</param>
    /// <param name="designCapacity">The design capacity.</param>
    /// <param name="designVoltage">The design voltage.</param>
    /// <param name="lastFullChargeCapacity">The last full charge capacity.</param>
    /// <param name="cycleCount">The cycle count.</param>
    /// <param name="chargePercentage">The charge percentage.</param>
    /// <param name="manufacturer">The battery manufacturer.</param>
    /// <param name="modelNumber">The battery model number.</param>
    /// <param name="serialNumber">The battery serial number.</param>
    /// <param name="batteryType">The battery chemistry or type.</param>
    public FrameworkPowerSnapshot(bool acPresent, bool batteryPresent, bool discharging, bool charging, bool levelCritical, byte batteryCount, byte currentBatteryIndex, uint presentVoltage, uint presentRate, uint remainingCapacity, uint designCapacity, uint designVoltage, uint lastFullChargeCapacity, uint cycleCount, uint chargePercentage, string manufacturer, string modelNumber, string serialNumber, string batteryType)
    {
        AcPresent = acPresent;
        BatteryPresent = batteryPresent;
        Discharging = discharging;
        Charging = charging;
        LevelCritical = levelCritical;
        BatteryCount = batteryCount;
        CurrentBatteryIndex = currentBatteryIndex;
        PresentVoltage = presentVoltage;
        PresentRate = presentRate;
        RemainingCapacity = remainingCapacity;
        DesignCapacity = designCapacity;
        DesignVoltage = designVoltage;
        LastFullChargeCapacity = lastFullChargeCapacity;
        CycleCount = cycleCount;
        ChargePercentage = chargePercentage;
        Manufacturer = manufacturer;
        ModelNumber = modelNumber;
        SerialNumber = serialNumber;
        BatteryType = batteryType;
    }

    /// <summary>
    /// Gets a value indicating whether AC power is present.
    /// </summary>
    public bool AcPresent { get; init; }

    /// <summary>
    /// Gets a value indicating whether a battery is present.
    /// </summary>
    public bool BatteryPresent { get; init; }

    /// <summary>
    /// Gets a value indicating whether the battery is discharging.
    /// </summary>
    public bool Discharging { get; init; }

    /// <summary>
    /// Gets a value indicating whether the battery is charging.
    /// </summary>
    public bool Charging { get; init; }

    /// <summary>
    /// Gets a value indicating whether the battery level is critical.
    /// </summary>
    public bool LevelCritical { get; init; }

    /// <summary>
    /// Gets the number of batteries reported by the device.
    /// </summary>
    public byte BatteryCount { get; init; }

    /// <summary>
    /// Gets the current battery index.
    /// </summary>
    public byte CurrentBatteryIndex { get; init; }

    /// <summary>
    /// Gets the present voltage.
    /// </summary>
    public uint PresentVoltage { get; init; }

    /// <summary>
    /// Gets the present rate.
    /// </summary>
    public uint PresentRate { get; init; }

    /// <summary>
    /// Gets the remaining capacity.
    /// </summary>
    public uint RemainingCapacity { get; init; }

    /// <summary>
    /// Gets the design capacity.
    /// </summary>
    public uint DesignCapacity { get; init; }

    /// <summary>
    /// Gets the design voltage.
    /// </summary>
    public uint DesignVoltage { get; init; }

    /// <summary>
    /// Gets the last full charge capacity.
    /// </summary>
    public uint LastFullChargeCapacity { get; init; }

    /// <summary>
    /// Gets the cycle count.
    /// </summary>
    public uint CycleCount { get; init; }

    /// <summary>
    /// Gets the charge percentage.
    /// </summary>
    public uint ChargePercentage { get; init; }

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
}
