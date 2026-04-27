using FrameworkDotnet.Generated;
using ManagedPowerSnapshot = FrameworkDotnet.Snapshots.FrameworkPowerSnapshot;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkPowerSnapshot
{
    public readonly string GetManufacturer()
    {
        fixed (byte* value = manufacturer)
        {
            return InteropStringHelpers.ReadNullTerminatedUtf8(value, 8);
        }
    }

    public readonly string GetModelNumber()
    {
        fixed (byte* value = model_number)
        {
            return InteropStringHelpers.ReadNullTerminatedUtf8(value, 8);
        }
    }

    public readonly string GetSerialNumber()
    {
        fixed (byte* value = serial_number)
        {
            return InteropStringHelpers.ReadNullTerminatedUtf8(value, 8);
        }
    }

    public readonly string GetBatteryType()
    {
        fixed (byte* value = battery_type)
        {
            return InteropStringHelpers.ReadNullTerminatedUtf8(value, 8);
        }
    }

    internal readonly ManagedPowerSnapshot ToManagedSnapshot()
    {
        return new ManagedPowerSnapshot(
            ac_present != 0,
            battery_present != 0,
            discharging != 0,
            charging != 0,
            level_critical != 0,
            battery_count,
            current_battery_index,
            present_voltage,
            present_rate,
            remaining_capacity,
            design_capacity,
            design_voltage,
            last_full_charge_capacity,
            cycle_count,
            charge_percentage,
            GetManufacturer(),
            GetModelNumber(),
            GetSerialNumber(),
            GetBatteryType());
    }
}
