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
            fan_0.GetValueOrThrow().ToManagedSnapshot(),
            fan_1.GetValueOrThrow().ToManagedSnapshot(),
            fan_2.GetValueOrThrow().ToManagedSnapshot(),
            fan_3.GetValueOrThrow().ToManagedSnapshot());
    }
}
