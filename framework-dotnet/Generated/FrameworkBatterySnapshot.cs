using System;

using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Snapshots;

using UnitsNet;

using ManagedBatterySnapshot = FrameworkDotnet.Snapshots.FrameworkBatterySnapshot;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkBatterySnapshot
{
    internal readonly FrameworkBatterySnapshot GetValueOrThrow()
    {
        switch (battery_state)
        {
            case FrameworkBatteryState.NotPresent:
            case FrameworkBatteryState.Critical:
            case FrameworkBatteryState.Idle:
            case FrameworkBatteryState.Charging:
            case FrameworkBatteryState.Discharging:
            case FrameworkBatteryState.ChargingAndDischarging:
                return this;
            default:
                throw new ArgumentOutOfRangeException(nameof(battery_state), battery_state, "Unhandled battery state.");
        }
    }

    internal readonly ManagedBatterySnapshot ToManagedSnapshot()
    {
        return new ManagedBatterySnapshot(
            GetManufacturer(),
            GetModelNumber(),
            GetSerialNumber(),
            GetBatteryType(),
            ElectricPotential.FromMillivolts(present_voltage),
            ElectricCurrent.FromMilliamperes(present_rate),
            ElectricCharge.FromMilliampereHours(remaining_capacity),
            ElectricCharge.FromMilliampereHours(design_capacity),
            ElectricPotential.FromMillivolts(design_voltage),
            ElectricCharge.FromMilliampereHours(last_full_charge_capacity),
            cycle_count,
            Ratio.FromPercent(charge_percentage),
            (FrameworkDotnet.Enums.FrameworkBatteryState)(int)battery_state);
    }
    internal readonly string GetManufacturer()
    {
        var value = manufacturer;
        return value.ToUtf8StringAndFree();
    }

    internal readonly string GetModelNumber()
    {
        var value = model_number;
        return value.ToUtf8StringAndFree();
    }

    internal readonly string GetSerialNumber()
    {
        var value = serial_number;
        return value.ToUtf8StringAndFree();
    }

    internal readonly string GetBatteryType()
    {
        var value = battery_type;
        return value.ToUtf8StringAndFree();
    }
}
