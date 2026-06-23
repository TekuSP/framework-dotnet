using System;

using UnitsNet;

using ManagedPowerDeliveryPortStateSnapshot = FrameworkDotnet.Snapshots.FrameworkPowerDeliveryPortStateSnapshot;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcPdPortState
{
    internal readonly ManagedPowerDeliveryPortStateSnapshot ToManagedSnapshot()
    {
        return new ManagedPowerDeliveryPortStateSnapshot(
            ToCState(),
            ToPowerRole(),
            ToDataRole(),
            ToCcPolarity(),
            ElectricPotential.FromMillivolts(voltage_mv),
            ElectricCurrent.FromMilliamperes(current_ma),
            has_pd_contract != 0,
            vconn_active != 0,
            epr_active != 0,
            epr_support != 0,
            active_port != 0,
            alt_mode_flags);
    }

    private readonly FrameworkDotnet.Enums.FrameworkPowerDeliveryTypeCState ToCState()
    {
        return c_state switch
        {
            FrameworkPdTypeCState.Nothing => FrameworkDotnet.Enums.FrameworkPowerDeliveryTypeCState.Nothing,
            FrameworkPdTypeCState.Sink => FrameworkDotnet.Enums.FrameworkPowerDeliveryTypeCState.Sink,
            FrameworkPdTypeCState.Source => FrameworkDotnet.Enums.FrameworkPowerDeliveryTypeCState.Source,
            FrameworkPdTypeCState.Debug => FrameworkDotnet.Enums.FrameworkPowerDeliveryTypeCState.Debug,
            FrameworkPdTypeCState.Audio => FrameworkDotnet.Enums.FrameworkPowerDeliveryTypeCState.Audio,
            FrameworkPdTypeCState.PoweredAccessory => FrameworkDotnet.Enums.FrameworkPowerDeliveryTypeCState.PoweredAccessory,
            FrameworkPdTypeCState.Unsupported => FrameworkDotnet.Enums.FrameworkPowerDeliveryTypeCState.Unsupported,
            FrameworkPdTypeCState.Invalid => FrameworkDotnet.Enums.FrameworkPowerDeliveryTypeCState.Invalid,
            _ => throw new ArgumentOutOfRangeException(nameof(c_state), c_state, "Unhandled Power Delivery Type-C state.")
        };
    }

    private readonly FrameworkDotnet.Enums.FrameworkPowerDeliveryPowerRole ToPowerRole()
    {
        return power_role switch
        {
            FrameworkPdPowerRole.Sink => FrameworkDotnet.Enums.FrameworkPowerDeliveryPowerRole.Sink,
            FrameworkPdPowerRole.Source => FrameworkDotnet.Enums.FrameworkPowerDeliveryPowerRole.Source,
            FrameworkPdPowerRole.Unknown => FrameworkDotnet.Enums.FrameworkPowerDeliveryPowerRole.Unknown,
            _ => throw new ArgumentOutOfRangeException(nameof(power_role), power_role, "Unhandled Power Delivery power role.")
        };
    }

    private readonly FrameworkDotnet.Enums.FrameworkPowerDeliveryDataRole ToDataRole()
    {
        return data_role switch
        {
            FrameworkPdDataRole.Ufp => FrameworkDotnet.Enums.FrameworkPowerDeliveryDataRole.Ufp,
            FrameworkPdDataRole.Dfp => FrameworkDotnet.Enums.FrameworkPowerDeliveryDataRole.Dfp,
            FrameworkPdDataRole.Disconnected => FrameworkDotnet.Enums.FrameworkPowerDeliveryDataRole.Disconnected,
            FrameworkPdDataRole.Unknown => FrameworkDotnet.Enums.FrameworkPowerDeliveryDataRole.Unknown,
            _ => throw new ArgumentOutOfRangeException(nameof(data_role), data_role, "Unhandled Power Delivery data role.")
        };
    }

    private readonly FrameworkDotnet.Enums.FrameworkPowerDeliveryCcPolarity ToCcPolarity()
    {
        return cc_polarity switch
        {
            FrameworkPdCcPolarity.Unknown => FrameworkDotnet.Enums.FrameworkPowerDeliveryCcPolarity.Unknown,
            FrameworkPdCcPolarity.Cc1 => FrameworkDotnet.Enums.FrameworkPowerDeliveryCcPolarity.Cc1,
            FrameworkPdCcPolarity.Cc2 => FrameworkDotnet.Enums.FrameworkPowerDeliveryCcPolarity.Cc2,
            FrameworkPdCcPolarity.Cc1Debug => FrameworkDotnet.Enums.FrameworkPowerDeliveryCcPolarity.Cc1Debug,
            FrameworkPdCcPolarity.Cc2Debug => FrameworkDotnet.Enums.FrameworkPowerDeliveryCcPolarity.Cc2Debug,
            _ => throw new ArgumentOutOfRangeException(nameof(cc_polarity), cc_polarity, "Unhandled Power Delivery CC polarity.")
        };
    }
}
