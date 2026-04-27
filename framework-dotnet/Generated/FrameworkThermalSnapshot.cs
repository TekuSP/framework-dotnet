using ManagedThermalSnapshot = FrameworkDotnet.Snapshots.FrameworkThermalSnapshot;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkThermalSnapshot
{
    internal readonly ManagedThermalSnapshot ToManagedSnapshot()
    {
        return new ManagedThermalSnapshot(
            fan_count,
            temperature_0.GetValueOrThrow().ToManagedSnapshot(),
            temperature_1.GetValueOrThrow().ToManagedSnapshot(),
            temperature_2.GetValueOrThrow().ToManagedSnapshot(),
            temperature_3.GetValueOrThrow().ToManagedSnapshot(),
            temperature_4.GetValueOrThrow().ToManagedSnapshot(),
            temperature_5.GetValueOrThrow().ToManagedSnapshot(),
            temperature_6.GetValueOrThrow().ToManagedSnapshot(),
            temperature_7.GetValueOrThrow().ToManagedSnapshot(),
            fan_rpm_0,
            fan_rpm_1,
            fan_rpm_2,
            fan_rpm_3,
            fan_present_0 != 0,
            fan_present_1 != 0,
            fan_present_2 != 0,
            fan_present_3 != 0,
            fan_stalled_0 != 0,
            fan_stalled_1 != 0,
            fan_stalled_2 != 0,
            fan_stalled_3 != 0);
    }
}
