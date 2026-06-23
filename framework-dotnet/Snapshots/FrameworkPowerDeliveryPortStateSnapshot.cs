using System.Globalization;

using FrameworkDotnet.Enums;

using UnitsNet;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents the full USB Power Delivery port state for an expansion card slot as reported by the EC.
/// </summary>
public sealed record FrameworkPowerDeliveryPortStateSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkPowerDeliveryPortStateSnapshot"/> class.
    /// </summary>
    /// <param name="cState">The physical USB Type-C connection state.</param>
    /// <param name="powerRole">The Power Delivery power role.</param>
    /// <param name="dataRole">The Power Delivery data role.</param>
    /// <param name="ccPolarity">The CC pin orientation.</param>
    /// <param name="voltage">The negotiated voltage.</param>
    /// <param name="current">The negotiated current.</param>
    /// <param name="hasPowerDeliveryContract">A value indicating whether a Power Delivery contract is active.</param>
    /// <param name="vconnActive">A value indicating whether VCONN is active.</param>
    /// <param name="eprActive">A value indicating whether Extended Power Range is active.</param>
    /// <param name="eprSupport">A value indicating whether the port supports Extended Power Range.</param>
    /// <param name="activePort">A value indicating whether this is the active charging port.</param>
    /// <param name="altModeFlags">The raw EC alt-mode status bits.</param>
    public FrameworkPowerDeliveryPortStateSnapshot(FrameworkPowerDeliveryTypeCState cState, FrameworkPowerDeliveryPowerRole powerRole, FrameworkPowerDeliveryDataRole dataRole, FrameworkPowerDeliveryCcPolarity ccPolarity, ElectricPotential voltage, ElectricCurrent current, bool hasPowerDeliveryContract, bool vconnActive, bool eprActive, bool eprSupport, bool activePort, byte altModeFlags)
    {
        CState = cState;
        PowerRole = powerRole;
        DataRole = dataRole;
        CcPolarity = ccPolarity;
        Voltage = voltage;
        Current = current;
        HasPowerDeliveryContract = hasPowerDeliveryContract;
        VconnActive = vconnActive;
        EprActive = eprActive;
        EprSupport = eprSupport;
        ActivePort = activePort;
        AltModeFlags = altModeFlags;
    }

    /// <summary>
    /// Gets the physical USB Type-C connection state.
    /// </summary>
    public FrameworkPowerDeliveryTypeCState CState { get; init; }

    /// <summary>
    /// Gets the Power Delivery power role.
    /// </summary>
    public FrameworkPowerDeliveryPowerRole PowerRole { get; init; }

    /// <summary>
    /// Gets the Power Delivery data role.
    /// </summary>
    public FrameworkPowerDeliveryDataRole DataRole { get; init; }

    /// <summary>
    /// Gets the CC pin orientation.
    /// </summary>
    public FrameworkPowerDeliveryCcPolarity CcPolarity { get; init; }

    /// <summary>
    /// Gets the negotiated voltage.
    /// </summary>
    public ElectricPotential Voltage { get; init; }

    /// <summary>
    /// Gets the negotiated current.
    /// </summary>
    public ElectricCurrent Current { get; init; }

    /// <summary>
    /// Gets a value indicating whether a USB Power Delivery contract is active on this port.
    /// </summary>
    public bool HasPowerDeliveryContract { get; init; }

    /// <summary>
    /// Gets a value indicating whether VCONN is active on this port.
    /// </summary>
    public bool VconnActive { get; init; }

    /// <summary>
    /// Gets a value indicating whether Extended Power Range (EPR) is active.
    /// </summary>
    public bool EprActive { get; init; }

    /// <summary>
    /// Gets a value indicating whether the port supports Extended Power Range (EPR).
    /// </summary>
    public bool EprSupport { get; init; }

    /// <summary>
    /// Gets a value indicating whether this port is the active charging port.
    /// </summary>
    public bool ActivePort { get; init; }

    /// <summary>
    /// Gets the raw EC alt-mode status bits.
    /// </summary>
    /// <remarks>Bit 0: DP/TBT DFP_D, Bit 1: UFP_D, Bit 2: Power Low, Bit 3: Enabled, Bit 4: Multi-Function, Bit 5: USB Config, Bit 6: Exit Request, Bit 7: HPD High.</remarks>
    public byte AltModeFlags { get; init; }

    public override string ToString()
    {
        return $"Power Delivery Port State: C-State: {CState}, Power Role: {PowerRole}, Data Role: {DataRole}, CC Polarity: {CcPolarity}, Voltage: {Voltage.ToString(CultureInfo.InvariantCulture)}, Current: {Current.ToString(CultureInfo.InvariantCulture)}, Contract: {HasPowerDeliveryContract}, VCONN: {VconnActive}, EPR Active: {EprActive}";
    }
}
