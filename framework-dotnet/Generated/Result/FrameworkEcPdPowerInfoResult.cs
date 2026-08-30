using System;

using FrameworkDotnet.Exceptions;

using UnitsNet;

using ManagedPowerDeliveryPowerInfoSnapshot = FrameworkDotnet.Snapshots.FrameworkPowerDeliveryPowerInfoSnapshot;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcPdPowerInfoResult
{
    internal readonly ManagedPowerDeliveryPowerInfoSnapshot GetValueOrThrow()
    {
        if (status.IsFailure)
        {
            throw FrameworkStatusException.GetCorrectException(status);
        }

        return new ManagedPowerDeliveryPowerInfoSnapshot(
            port,
            dualrole != 0,
            ToPowerRole(),
            ToChargingType(),
            ElectricPotential.FromMillivolts(voltage_max_mv),
            ElectricPotential.FromMillivolts(voltage_now_mv),
            ElectricCurrent.FromMilliamperes(current_max_ma),
            ElectricCurrent.FromMilliamperes(current_lim_ma),
            Power.FromMicrowatts(max_power_uw));
    }

    private readonly FrameworkDotnet.Enums.FrameworkUsbPowerRole ToPowerRole()
    {
        return role switch
        {
            FrameworkUsbPowerRole.Disconnected => FrameworkDotnet.Enums.FrameworkUsbPowerRole.Disconnected,
            FrameworkUsbPowerRole.Source => FrameworkDotnet.Enums.FrameworkUsbPowerRole.Source,
            FrameworkUsbPowerRole.Sink => FrameworkDotnet.Enums.FrameworkUsbPowerRole.Sink,
            FrameworkUsbPowerRole.SinkNotCharging => FrameworkDotnet.Enums.FrameworkUsbPowerRole.SinkNotCharging,
            _ => throw new ArgumentOutOfRangeException(nameof(role), role, "The native layer reported a Power Delivery power role that is not recognized by the managed API."),
        };
    }

    private readonly FrameworkDotnet.Enums.FrameworkUsbChargingType ToChargingType()
    {
        return charging_type switch
        {
            FrameworkUsbChargingType.None => FrameworkDotnet.Enums.FrameworkUsbChargingType.None,
            FrameworkUsbChargingType.Pd => FrameworkDotnet.Enums.FrameworkUsbChargingType.Pd,
            FrameworkUsbChargingType.TypeC => FrameworkDotnet.Enums.FrameworkUsbChargingType.TypeC,
            FrameworkUsbChargingType.Proprietary => FrameworkDotnet.Enums.FrameworkUsbChargingType.Proprietary,
            FrameworkUsbChargingType.Bc12Dcp => FrameworkDotnet.Enums.FrameworkUsbChargingType.Bc12Dcp,
            FrameworkUsbChargingType.Bc12Cdp => FrameworkDotnet.Enums.FrameworkUsbChargingType.Bc12Cdp,
            FrameworkUsbChargingType.Bc12Sdp => FrameworkDotnet.Enums.FrameworkUsbChargingType.Bc12Sdp,
            FrameworkUsbChargingType.Other => FrameworkDotnet.Enums.FrameworkUsbChargingType.Other,
            FrameworkUsbChargingType.VBus => FrameworkDotnet.Enums.FrameworkUsbChargingType.VBus,
            FrameworkUsbChargingType.Unknown => FrameworkDotnet.Enums.FrameworkUsbChargingType.Unknown,
            _ => FrameworkDotnet.Enums.FrameworkUsbChargingType.Unknown,
        };
    }
}
