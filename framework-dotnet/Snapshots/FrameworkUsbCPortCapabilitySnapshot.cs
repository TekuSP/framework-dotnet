using System.Globalization;

using FrameworkDotnet.Enums;

using UnitsNet;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents the static, board-defined USB-C capability of a numbered expansion card slot — data lane,
/// DisplayPort version, and charging support. Sourced from Framework's published per-platform expansion-card
/// matrices, not from the live Power Delivery negotiation (which is exposed separately as
/// <see cref="FrameworkPowerDeliveryPortStateSnapshot"/>).
/// </summary>
public sealed record FrameworkUsbCPortCapabilitySnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkUsbCPortCapabilitySnapshot"/> class.
    /// </summary>
    /// <param name="isDocumented">Whether a documented capability matrix covers this slot and platform.</param>
    /// <param name="dataLane">The USB-C data-lane capability.</param>
    /// <param name="displayPort">The DisplayPort alt-mode capability and version.</param>
    /// <param name="supportsPowerDelivery">Whether the slot supports USB Power Delivery charging.</param>
    /// <param name="maxChargePower">The maximum charge power, or zero when undocumented / not a charging port.</param>
    /// <param name="usbAHighPowerDraw">Whether the "higher power consumption" USB-A note applies to this slot.</param>
    public FrameworkUsbCPortCapabilitySnapshot(bool isDocumented, FrameworkUsbCDataLane dataLane, FrameworkDisplayPortCapability displayPort, bool supportsPowerDelivery, Power maxChargePower, bool usbAHighPowerDraw)
    {
        IsDocumented = isDocumented;
        DataLane = dataLane;
        DisplayPort = displayPort;
        SupportsPowerDelivery = supportsPowerDelivery;
        MaxChargePower = maxChargePower;
        UsbAHighPowerDraw = usbAHighPowerDraw;
    }

    /// <summary>
    /// Gets a value indicating whether a documented capability matrix covers this slot and platform. When
    /// <see langword="false"/>, the remaining fields are defaults and the live PD state should be used alone.
    /// </summary>
    public bool IsDocumented { get; init; }

    /// <summary>
    /// Gets the USB-C data-lane capability of the slot.
    /// </summary>
    public FrameworkUsbCDataLane DataLane { get; init; }

    /// <summary>
    /// Gets the DisplayPort alt-mode capability and version of the slot.
    /// </summary>
    public FrameworkDisplayPortCapability DisplayPort { get; init; }

    /// <summary>
    /// Gets a value indicating whether the slot supports USB Power Delivery charging. Slots that are wired for a
    /// fixed low-power limit (e.g. 900 mA) report <see langword="false"/>.
    /// </summary>
    public bool SupportsPowerDelivery { get; init; }

    /// <summary>
    /// Gets the maximum charge power, or <see cref="Power.Zero"/> when the slot is not a charging port or the
    /// wattage is not documented in the source matrix.
    /// </summary>
    public Power MaxChargePower { get; init; }

    /// <summary>
    /// Gets a value indicating whether the "higher power consumption" USB-A note applies to this slot.
    /// </summary>
    public bool UsbAHighPowerDraw { get; init; }

    public override string ToString()
    {
        return $"USB-C Capability: Documented: {IsDocumented}, Data Lane: {DataLane}, DisplayPort: {DisplayPort}, Supports PD: {SupportsPowerDelivery}, Max Charge: {MaxChargePower.ToString(CultureInfo.InvariantCulture)}, USB-A High Power: {UsbAHighPowerDraw}";
    }
}
