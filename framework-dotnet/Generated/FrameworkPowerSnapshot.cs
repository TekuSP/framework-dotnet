using System;

using FrameworkDotnet.Exceptions;

using ManagedPowerSnapshot = FrameworkDotnet.Snapshots.FrameworkPowerSnapshot;
namespace Framework.System.Interop;

internal unsafe partial struct FrameworkPowerSnapshot
{
    private static FrameworkDotnet.Snapshots.FrameworkBatterySnapshot ToManagedBatterySnapshot(byte batteryCount, byte batteryIndex, FrameworkBatterySnapshot battery)
    {
        if (batteryIndex >= batteryCount)
        {
            return battery.ToManagedSnapshot();
        }

        if (battery.battery_state == FrameworkBatteryState.NotPresent)
        {
            throw FrameworkBatteryStateException.GetCorrectException(battery.battery_state);
        }

        return battery.GetValueOrThrow().ToManagedSnapshot();
    }

    internal readonly FrameworkPowerSnapshot GetValueOrThrow()
    {
        switch (power_source_state)
        {
            case FrameworkPowerSourceState.None:
            case FrameworkPowerSourceState.AcOnly:
            case FrameworkPowerSourceState.BatteryOnly:
            case FrameworkPowerSourceState.AcAndBattery:
                return this;
            default:
                throw new ArgumentOutOfRangeException(nameof(power_source_state), power_source_state, "Unhandled power source state.");
        }
    }

    /// <summary>
    /// Converts the current instance to a managed snapshot.
    /// </summary>
    internal readonly ManagedPowerSnapshot ToManagedSnapshot()
    {
        return new ManagedPowerSnapshot(
            (FrameworkDotnet.Enums.FrameworkPowerSourceState)(int)power_source_state,
            battery_count,
            ToManagedBatterySnapshot(battery_count, 0, battery_0));
    }
}
