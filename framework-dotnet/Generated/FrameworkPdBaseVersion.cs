using ManagedPowerDeliveryBaseVersionSnapshot = FrameworkDotnet.Snapshots.FrameworkPowerDeliveryBaseVersionSnapshot;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkPdBaseVersion
{
    internal readonly ManagedPowerDeliveryBaseVersionSnapshot ToManagedSnapshot()
    {
        return new ManagedPowerDeliveryBaseVersionSnapshot(
            major,
            minor,
            patch,
            build_number);
    }
}
