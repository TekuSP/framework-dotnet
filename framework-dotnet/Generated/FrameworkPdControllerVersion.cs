using ManagedPowerDeliveryControllerImageSnapshot = FrameworkDotnet.Snapshots.FrameworkPowerDeliveryControllerImageSnapshot;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkPdControllerVersion
{
    internal readonly ManagedPowerDeliveryControllerImageSnapshot ToManagedSnapshot()
    {
        return new ManagedPowerDeliveryControllerImageSnapshot(
            @base.ToManagedSnapshot(),
            app.ToManagedSnapshot());
    }
}
