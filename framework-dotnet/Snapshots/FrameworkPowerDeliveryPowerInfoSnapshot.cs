using System.Globalization;

using FrameworkDotnet.Enums;

using UnitsNet;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents the charger negotiation state of a single USB Power Delivery port as reported by the embedded controller.
/// </summary>
/// <remarks>
/// This snapshot describes what the attached charger offers and what the port has negotiated from it. It is distinct from
/// <see cref="FrameworkPowerDeliveryPortStateSnapshot"/>, which is surfaced through the module inventory and describes the USB Type-C link itself
/// (connection state, data role, CC orientation and alt-mode bits). Reading one does not tell you what the other reports.
/// </remarks>
public sealed record FrameworkPowerDeliveryPowerInfoSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkPowerDeliveryPowerInfoSnapshot"/> class.
    /// </summary>
    /// <param name="port">The zero-based port index the reading belongs to.</param>
    /// <param name="supportsDualRole">A value indicating whether the port supports dual-role power.</param>
    /// <param name="role">The negotiated power role of the port.</param>
    /// <param name="chargingType">The way the attached charger is supplying power.</param>
    /// <param name="maximumVoltage">The maximum voltage the attached charger advertises.</param>
    /// <param name="voltage">The voltage currently present on the port.</param>
    /// <param name="maximumCurrent">The maximum current the attached charger advertises.</param>
    /// <param name="currentLimit">The current limit currently in force on the port.</param>
    /// <param name="maximumPower">The maximum negotiated power.</param>
    public FrameworkPowerDeliveryPowerInfoSnapshot(byte port, bool supportsDualRole, FrameworkUsbPowerRole role, FrameworkUsbChargingType chargingType, ElectricPotential maximumVoltage, ElectricPotential voltage, ElectricCurrent maximumCurrent, ElectricCurrent currentLimit, Power maximumPower)
    {
        Port = port;
        SupportsDualRole = supportsDualRole;
        Role = role;
        ChargingType = chargingType;
        MaximumVoltage = maximumVoltage;
        Voltage = voltage;
        MaximumCurrent = maximumCurrent;
        CurrentLimit = currentLimit;
        MaximumPower = maximumPower;
    }

    /// <summary>
    /// Gets the zero-based port index the reading belongs to.
    /// </summary>
    public byte Port { get; init; }

    /// <summary>
    /// Gets a value indicating whether the port supports dual-role power.
    /// </summary>
    public bool SupportsDualRole { get; init; }

    /// <summary>
    /// Gets the negotiated power role of the port.
    /// </summary>
    public FrameworkUsbPowerRole Role { get; init; }

    /// <summary>
    /// Gets the way the attached charger is supplying power.
    /// </summary>
    public FrameworkUsbChargingType ChargingType { get; init; }

    /// <summary>
    /// Gets the maximum voltage the attached charger advertises.
    /// </summary>
    public ElectricPotential MaximumVoltage { get; init; }

    /// <summary>
    /// Gets the voltage currently present on the port.
    /// </summary>
    public ElectricPotential Voltage { get; init; }

    /// <summary>
    /// Gets the maximum current the attached charger advertises.
    /// </summary>
    public ElectricCurrent MaximumCurrent { get; init; }

    /// <summary>
    /// Gets the current limit currently in force on the port.
    /// </summary>
    public ElectricCurrent CurrentLimit { get; init; }

    /// <summary>
    /// Gets the maximum negotiated power.
    /// </summary>
    public Power MaximumPower { get; init; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"Power Delivery Power Info: Port: {Port.ToString(CultureInfo.InvariantCulture)}, Role: {Role}, Charging Type: {ChargingType}, Dual Role: {SupportsDualRole}, Voltage: {Voltage.ToString(CultureInfo.InvariantCulture)} (max {MaximumVoltage.ToString(CultureInfo.InvariantCulture)}), Current Limit: {CurrentLimit.ToString(CultureInfo.InvariantCulture)} (max {MaximumCurrent.ToString(CultureInfo.InvariantCulture)}), Maximum Power: {MaximumPower.ToString(CultureInfo.InvariantCulture)}";
    }
}
