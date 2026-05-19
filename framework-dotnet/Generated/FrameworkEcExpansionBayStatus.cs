using System;

using ManagedExpansionBaySnapshot = FrameworkDotnet.Snapshots.FrameworkExpansionBaySnapshot;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcExpansionBayStatus
{
    internal readonly void FreeOwnedBuffers()
    {
        var serialNumber = serial_number;
        serialNumber.Free();
    }

    internal readonly ManagedExpansionBaySnapshot ToManagedSnapshot()
    {
        var serialNumber = serial_number;

        try
        {
            return new ManagedExpansionBaySnapshot(
                present != 0,
                enabled != 0,
                fault != 0,
                door_closed != 0,
                ToManagedBoard(),
                ToManagedVendor(),
                ToManagedPcieConfiguration(),
                serialNumber.ToUtf8String());
        }
        finally
        {
            serialNumber.Free();
        }
    }

    private readonly FrameworkDotnet.Enums.FrameworkExpansionBayBoard ToManagedBoard()
    {
        return board switch
        {
            FrameworkExpansionBayBoard.Unknown => FrameworkDotnet.Enums.FrameworkExpansionBayBoard.Unknown,
            FrameworkExpansionBayBoard.DualInterposer => FrameworkDotnet.Enums.FrameworkExpansionBayBoard.DualInterposer,
            FrameworkExpansionBayBoard.SingleInterposer => FrameworkDotnet.Enums.FrameworkExpansionBayBoard.SingleInterposer,
            FrameworkExpansionBayBoard.UmaFans => FrameworkDotnet.Enums.FrameworkExpansionBayBoard.UmaFans,
            FrameworkExpansionBayBoard.NoModule => FrameworkDotnet.Enums.FrameworkExpansionBayBoard.NoModule,
            FrameworkExpansionBayBoard.BadConnection => FrameworkDotnet.Enums.FrameworkExpansionBayBoard.BadConnection,
            _ => throw new ArgumentOutOfRangeException(nameof(board), board, "Unhandled expansion bay board.")
        };
    }

    private readonly FrameworkDotnet.Enums.FrameworkExpansionBayVendor ToManagedVendor()
    {
        return vendor switch
        {
            FrameworkExpansionBayVendor.Unknown => FrameworkDotnet.Enums.FrameworkExpansionBayVendor.Unknown,
            FrameworkExpansionBayVendor.Initializing => FrameworkDotnet.Enums.FrameworkExpansionBayVendor.Initializing,
            FrameworkExpansionBayVendor.FanOnly => FrameworkDotnet.Enums.FrameworkExpansionBayVendor.FanOnly,
            FrameworkExpansionBayVendor.SsdHolder => FrameworkDotnet.Enums.FrameworkExpansionBayVendor.SsdHolder,
            FrameworkExpansionBayVendor.PcieAccessory => FrameworkDotnet.Enums.FrameworkExpansionBayVendor.PcieAccessory,
            FrameworkExpansionBayVendor.AmdGpu => FrameworkDotnet.Enums.FrameworkExpansionBayVendor.AmdGpu,
            FrameworkExpansionBayVendor.NvidiaGpu => FrameworkDotnet.Enums.FrameworkExpansionBayVendor.NvidiaGpu,
            _ => throw new ArgumentOutOfRangeException(nameof(vendor), vendor, "Unhandled expansion bay vendor.")
        };
    }

    private readonly FrameworkDotnet.Enums.FrameworkGpuPcieConfig ToManagedPcieConfiguration()
    {
        return config switch
        {
            FrameworkGpuPcieConfig.Unknown => FrameworkDotnet.Enums.FrameworkGpuPcieConfig.Unknown,
            FrameworkGpuPcieConfig.Pcie4x1 => FrameworkDotnet.Enums.FrameworkGpuPcieConfig.Pcie4x1,
            FrameworkGpuPcieConfig.Pcie4x2 => FrameworkDotnet.Enums.FrameworkGpuPcieConfig.Pcie4x2,
            FrameworkGpuPcieConfig.Pcie4x4 => FrameworkDotnet.Enums.FrameworkGpuPcieConfig.Pcie4x4,
            FrameworkGpuPcieConfig.Pcie5x4 => FrameworkDotnet.Enums.FrameworkGpuPcieConfig.Pcie5x4,
            _ => throw new ArgumentOutOfRangeException(nameof(config), config, "Unhandled GPU PCIe configuration.")
        };
    }
}
