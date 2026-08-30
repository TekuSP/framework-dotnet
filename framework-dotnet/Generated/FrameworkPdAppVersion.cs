using ManagedPowerDeliveryApplicationVersionSnapshot = FrameworkDotnet.Snapshots.FrameworkPowerDeliveryApplicationVersionSnapshot;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkPdAppVersion
{
    internal readonly ManagedPowerDeliveryApplicationVersionSnapshot ToManagedSnapshot()
    {
        return new ManagedPowerDeliveryApplicationVersionSnapshot(
            ToApplication(),
            major,
            minor,
            circuit);
    }

    private readonly FrameworkDotnet.Enums.FrameworkPdApplication ToApplication()
    {
        return application switch
        {
            FrameworkPdApplication.Notebook => FrameworkDotnet.Enums.FrameworkPdApplication.Notebook,
            FrameworkPdApplication.Monitor => FrameworkDotnet.Enums.FrameworkPdApplication.Monitor,
            FrameworkPdApplication.AA => FrameworkDotnet.Enums.FrameworkPdApplication.AA,
            FrameworkPdApplication.Invalid => FrameworkDotnet.Enums.FrameworkPdApplication.Invalid,
            _ => FrameworkDotnet.Enums.FrameworkPdApplication.Invalid,
        };
    }
}
