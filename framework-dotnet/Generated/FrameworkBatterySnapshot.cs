using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Snapshots;

using ManagedBatterySnapshot = FrameworkDotnet.Snapshots.FrameworkBatterSnapshot;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkBatterySnapshot
{
    internal FrameworkBatterySnapshot GetValueOrThrow()
    {
        switch (battery_state)
        {
            case FrameworkBatteryState.NotPresent:
                break;
            case FrameworkBatteryState.Critical:
                break;
            case FrameworkBatteryState.Idle:
            case FrameworkBatteryState.Charging:
            case FrameworkBatteryState.Discharging:
            case FrameworkBatteryState.ChargingAndDischarging:
                return this;
            default:
                break;
        }
        throw FrameworkBatteryStateException.GetCorrectException(battery_state);
    }

    internal ManagedBatterySnapshot ToManagedSnapshot()
    {
        return new ManagedBatterySnapshot(GetManufacturer(), GetModelNumber(), GetSerialNumber(), GetBatteryType(), (FrameworkDotnet.Enums.FrameworkBatteryState)(int)battery_state);
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
