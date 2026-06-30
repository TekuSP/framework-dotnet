using UnitsNet;

using ManagedUsbCPortCapabilitySnapshot = FrameworkDotnet.Snapshots.FrameworkUsbCPortCapabilitySnapshot;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkUsbCPortCapability
{
    internal readonly ManagedUsbCPortCapabilitySnapshot ToManagedSnapshot()
    {
        return new ManagedUsbCPortCapabilitySnapshot(
            known != 0,
            ToManagedDataLane(),
            ToManagedDisplayPort(),
            supports_pd != 0,
            Power.FromWatts(max_charge_watts),
            usb_a_high_power != 0);
    }

    private readonly FrameworkDotnet.Enums.FrameworkUsbCDataLane ToManagedDataLane()
    {
        return data_lane switch
        {
            FrameworkUsbCDataLane.Unknown => FrameworkDotnet.Enums.FrameworkUsbCDataLane.Unknown,
            FrameworkUsbCDataLane.Usb2 => FrameworkDotnet.Enums.FrameworkUsbCDataLane.Usb2,
            FrameworkUsbCDataLane.Usb32 => FrameworkDotnet.Enums.FrameworkUsbCDataLane.Usb32,
            FrameworkUsbCDataLane.Usb32Gen2x1 => FrameworkDotnet.Enums.FrameworkUsbCDataLane.Usb32Gen2x1,
            FrameworkUsbCDataLane.Usb32Gen2x2 => FrameworkDotnet.Enums.FrameworkUsbCDataLane.Usb32Gen2x2,
            FrameworkUsbCDataLane.Usb4 => FrameworkDotnet.Enums.FrameworkUsbCDataLane.Usb4,
            FrameworkUsbCDataLane.Thunderbolt4 => FrameworkDotnet.Enums.FrameworkUsbCDataLane.Thunderbolt4,
            _ => FrameworkDotnet.Enums.FrameworkUsbCDataLane.Unknown,
        };
    }

    private readonly FrameworkDotnet.Enums.FrameworkDisplayPortCapability ToManagedDisplayPort()
    {
        return displayport switch
        {
            FrameworkDisplayPortCapability.None => FrameworkDotnet.Enums.FrameworkDisplayPortCapability.None,
            FrameworkDisplayPortCapability.Dp14Hbr3 => FrameworkDotnet.Enums.FrameworkDisplayPortCapability.Dp14Hbr3,
            FrameworkDisplayPortCapability.Dp20 => FrameworkDotnet.Enums.FrameworkDisplayPortCapability.Dp20,
            FrameworkDisplayPortCapability.Dp20Uhbr10 => FrameworkDotnet.Enums.FrameworkDisplayPortCapability.Dp20Uhbr10,
            FrameworkDisplayPortCapability.Dp20Uhbr20 => FrameworkDotnet.Enums.FrameworkDisplayPortCapability.Dp20Uhbr20,
            FrameworkDisplayPortCapability.Dp21 => FrameworkDotnet.Enums.FrameworkDisplayPortCapability.Dp21,
            FrameworkDisplayPortCapability.Dp21Uhbr10 => FrameworkDotnet.Enums.FrameworkDisplayPortCapability.Dp21Uhbr10,
            FrameworkDisplayPortCapability.Dp21Uhbr20 => FrameworkDotnet.Enums.FrameworkDisplayPortCapability.Dp21Uhbr20,
            FrameworkDisplayPortCapability.Supported => FrameworkDotnet.Enums.FrameworkDisplayPortCapability.Supported,
            _ => FrameworkDotnet.Enums.FrameworkDisplayPortCapability.None,
        };
    }
}
