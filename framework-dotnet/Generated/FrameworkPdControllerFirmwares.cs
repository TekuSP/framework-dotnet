using ManagedPowerDeliveryControllerFirmwareSnapshot = FrameworkDotnet.Snapshots.FrameworkPowerDeliveryControllerFirmwareSnapshot;
using ManagedPowerDeliveryControllerSlot = FrameworkDotnet.Enums.FrameworkPowerDeliveryControllerSlot;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkPdControllerFirmwares
{
    internal readonly ManagedPowerDeliveryControllerFirmwareSnapshot ToManagedSnapshot(ManagedPowerDeliveryControllerSlot slot)
    {
        return new ManagedPowerDeliveryControllerFirmwareSnapshot(
            slot,
            present != 0,
            ToFirmwareMode(),
            bootloader.ToManagedSnapshot(),
            backup_fw.ToManagedSnapshot(),
            main_fw.ToManagedSnapshot());
    }

    private readonly FrameworkDotnet.Enums.FrameworkPdFwMode ToFirmwareMode()
    {
        return active_fw switch
        {
            FrameworkPdFwMode.Unknown => FrameworkDotnet.Enums.FrameworkPdFwMode.Unknown,
            FrameworkPdFwMode.BootLoader => FrameworkDotnet.Enums.FrameworkPdFwMode.BootLoader,
            FrameworkPdFwMode.BackupFw => FrameworkDotnet.Enums.FrameworkPdFwMode.BackupFw,
            FrameworkPdFwMode.MainFw => FrameworkDotnet.Enums.FrameworkPdFwMode.MainFw,
            _ => FrameworkDotnet.Enums.FrameworkPdFwMode.Unknown,
        };
    }
}
