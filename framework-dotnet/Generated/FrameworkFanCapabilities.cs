using ManagedFanCapabilitiesSnapshot = FrameworkDotnet.Snapshots.FrameworkFanCapabilitiesSnapshot;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkFanCapabilities
{
    internal readonly ManagedFanCapabilitiesSnapshot ToManagedSnapshot()
    {
        return new ManagedFanCapabilitiesSnapshot(
            fan_count,
            supports_fan_control != 0,
            supports_thermal_reporting != 0);
    }
}
