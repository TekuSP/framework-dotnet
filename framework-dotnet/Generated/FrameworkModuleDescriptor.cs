using System;

using ManagedModuleDescriptorSnapshot = FrameworkDotnet.Snapshots.FrameworkModuleDescriptorSnapshot;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkModuleDescriptor
{
    internal readonly ManagedModuleDescriptorSnapshot ToManagedSnapshot()
    {
        return new ManagedModuleDescriptorSnapshot(
            ToManagedIdentity(),
            ToManagedBus(),
            ToManagedSlotKind(),
            ToManagedConfidence(),
            present != 0,
            slot_index,
            (FrameworkDotnet.Enums.FrameworkModuleFlags)flags,
            vendor_id,
            product_id,
            board_id);
    }

    private readonly FrameworkDotnet.Enums.FrameworkModuleIdentity ToManagedIdentity()
    {
        return identity switch
        {
            FrameworkModuleIdentity.None => FrameworkDotnet.Enums.FrameworkModuleIdentity.None,
            FrameworkModuleIdentity.UnknownUsbCOccupant => FrameworkDotnet.Enums.FrameworkModuleIdentity.UnknownUsbCOccupant,
            FrameworkModuleIdentity.DpExpansionCard => FrameworkDotnet.Enums.FrameworkModuleIdentity.DpExpansionCard,
            FrameworkModuleIdentity.HdmiExpansionCard => FrameworkDotnet.Enums.FrameworkModuleIdentity.HdmiExpansionCard,
            FrameworkModuleIdentity.AudioExpansionCard => FrameworkDotnet.Enums.FrameworkModuleIdentity.AudioExpansionCard,
            FrameworkModuleIdentity.Framework16KeyboardModule => FrameworkDotnet.Enums.FrameworkModuleIdentity.Framework16KeyboardModule,
            FrameworkModuleIdentity.Framework16LedMatrix => FrameworkDotnet.Enums.FrameworkModuleIdentity.Framework16LedMatrix,
            FrameworkModuleIdentity.Framework16TouchpadModule => FrameworkDotnet.Enums.FrameworkModuleIdentity.Framework16TouchpadModule,
            FrameworkModuleIdentity.InternalKeyboard => FrameworkDotnet.Enums.FrameworkModuleIdentity.InternalKeyboard,
            FrameworkModuleIdentity.InternalTouchpad => FrameworkDotnet.Enums.FrameworkModuleIdentity.InternalTouchpad,
            FrameworkModuleIdentity.FingerprintReader => FrameworkDotnet.Enums.FrameworkModuleIdentity.FingerprintReader,
            FrameworkModuleIdentity.Touchscreen => FrameworkDotnet.Enums.FrameworkModuleIdentity.Touchscreen,
            FrameworkModuleIdentity.Webcam => FrameworkDotnet.Enums.FrameworkModuleIdentity.Webcam,
            FrameworkModuleIdentity.ExpansionBay => FrameworkDotnet.Enums.FrameworkModuleIdentity.ExpansionBay,
            FrameworkModuleIdentity.ExpansionBayDualInterposer => FrameworkDotnet.Enums.FrameworkModuleIdentity.ExpansionBayDualInterposer,
            FrameworkModuleIdentity.ExpansionBaySingleInterposer => FrameworkDotnet.Enums.FrameworkModuleIdentity.ExpansionBaySingleInterposer,
            FrameworkModuleIdentity.ExpansionBayUmaFans => FrameworkDotnet.Enums.FrameworkModuleIdentity.ExpansionBayUmaFans,
            FrameworkModuleIdentity.ExpansionBaySsdHolder => FrameworkDotnet.Enums.FrameworkModuleIdentity.ExpansionBaySsdHolder,
            FrameworkModuleIdentity.ExpansionBayPcieAccessory => FrameworkDotnet.Enums.FrameworkModuleIdentity.ExpansionBayPcieAccessory,
            FrameworkModuleIdentity.ExpansionBayAmdGpu => FrameworkDotnet.Enums.FrameworkModuleIdentity.ExpansionBayAmdGpu,
            FrameworkModuleIdentity.ExpansionBayNvidiaGpu => FrameworkDotnet.Enums.FrameworkModuleIdentity.ExpansionBayNvidiaGpu,
            FrameworkModuleIdentity.ExpansionBayFanOnly => FrameworkDotnet.Enums.FrameworkModuleIdentity.ExpansionBayFanOnly,
            FrameworkModuleIdentity.UsbAExpansionCard => FrameworkDotnet.Enums.FrameworkModuleIdentity.UsbAExpansionCard,
            FrameworkModuleIdentity.UsbCExpansionCard => FrameworkDotnet.Enums.FrameworkModuleIdentity.UsbCExpansionCard,
            FrameworkModuleIdentity.EthernetExpansionCard => FrameworkDotnet.Enums.FrameworkModuleIdentity.EthernetExpansionCard,
            FrameworkModuleIdentity.Ethernet10GExpansionCard => FrameworkDotnet.Enums.FrameworkModuleIdentity.Ethernet10GExpansionCard,
            FrameworkModuleIdentity.MicroSdExpansionCard => FrameworkDotnet.Enums.FrameworkModuleIdentity.MicroSdExpansionCard,
            FrameworkModuleIdentity.SdExpansionCard => FrameworkDotnet.Enums.FrameworkModuleIdentity.SdExpansionCard,
            FrameworkModuleIdentity.SsdExpansionCard => FrameworkDotnet.Enums.FrameworkModuleIdentity.SsdExpansionCard,
            _ => FrameworkDotnet.Enums.FrameworkModuleIdentity.None,
        };
    }

    private readonly FrameworkDotnet.Enums.FrameworkModuleBus ToManagedBus()
    {
        return bus switch
        {
            FrameworkModuleBus.Unknown => FrameworkDotnet.Enums.FrameworkModuleBus.Unknown,
            FrameworkModuleBus.Ec => FrameworkDotnet.Enums.FrameworkModuleBus.Ec,
            FrameworkModuleBus.Usb => FrameworkDotnet.Enums.FrameworkModuleBus.Usb,
            FrameworkModuleBus.Hid => FrameworkDotnet.Enums.FrameworkModuleBus.Hid,
            FrameworkModuleBus.Composite => FrameworkDotnet.Enums.FrameworkModuleBus.Composite,
            _ => FrameworkDotnet.Enums.FrameworkModuleBus.Unknown,
        };
    }

    private readonly FrameworkDotnet.Enums.FrameworkModuleSlotKind ToManagedSlotKind()
    {
        return slot_kind switch
        {
            FrameworkModuleSlotKind.None => FrameworkDotnet.Enums.FrameworkModuleSlotKind.None,
            FrameworkModuleSlotKind.UsbCPort => FrameworkDotnet.Enums.FrameworkModuleSlotKind.UsbCPort,
            FrameworkModuleSlotKind.InputDeckTopRow => FrameworkDotnet.Enums.FrameworkModuleSlotKind.InputDeckTopRow,
            FrameworkModuleSlotKind.InputDeckTouchpad => FrameworkDotnet.Enums.FrameworkModuleSlotKind.InputDeckTouchpad,
            FrameworkModuleSlotKind.ExpansionBay => FrameworkDotnet.Enums.FrameworkModuleSlotKind.ExpansionBay,
            FrameworkModuleSlotKind.InternalFixed => FrameworkDotnet.Enums.FrameworkModuleSlotKind.InternalFixed,
            FrameworkModuleSlotKind.Detached => FrameworkDotnet.Enums.FrameworkModuleSlotKind.Detached,
            FrameworkModuleSlotKind.UsbCExpansionCardSlot => FrameworkDotnet.Enums.FrameworkModuleSlotKind.UsbCExpansionCardSlot,
            _ => FrameworkDotnet.Enums.FrameworkModuleSlotKind.None,
        };
    }

    private readonly FrameworkDotnet.Enums.FrameworkModuleConfidence ToManagedConfidence()
    {
        return confidence switch
        {
            FrameworkModuleConfidence.Unknown => FrameworkDotnet.Enums.FrameworkModuleConfidence.Unknown,
            FrameworkModuleConfidence.DerivedWeak => FrameworkDotnet.Enums.FrameworkModuleConfidence.DerivedWeak,
            FrameworkModuleConfidence.DerivedStrong => FrameworkDotnet.Enums.FrameworkModuleConfidence.DerivedStrong,
            FrameworkModuleConfidence.Direct => FrameworkDotnet.Enums.FrameworkModuleConfidence.Direct,
            _ => FrameworkDotnet.Enums.FrameworkModuleConfidence.Unknown,
        };
    }
}
