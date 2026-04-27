using ManagedPowerSnapshot = FrameworkDotnet.Snapshots.FrameworkPowerSnapshot;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkPowerSnapshot
{
    internal readonly ManagedPowerSnapshot ToManagedSnapshot()
    {
        var battery = battery_0;
        var batteryState = battery.battery_state;

        return new ManagedPowerSnapshot(
            power_source_state is FrameworkPowerSourceState.AcOnly or FrameworkPowerSourceState.AcAndBattery,
            batteryState != FrameworkBatteryState.NotPresent,
            batteryState is FrameworkBatteryState.Discharging or FrameworkBatteryState.ChargingAndDischarging,
            batteryState is FrameworkBatteryState.Charging or FrameworkBatteryState.ChargingAndDischarging,
            batteryState == FrameworkBatteryState.Critical,
            battery_count,
            0,
            battery.present_voltage,
            battery.present_rate,
            battery.remaining_capacity,
            battery.design_capacity,
            battery.design_voltage,
            battery.last_full_charge_capacity,
            battery.cycle_count,
            battery.charge_percentage,
            battery.GetManufacturer(),
            battery.GetModelNumber(),
            battery.GetSerialNumber(),
            battery.GetBatteryType());
    }
}
