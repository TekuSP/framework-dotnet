using ManagedModuleInventorySnapshot = FrameworkDotnet.Snapshots.FrameworkModuleInventorySnapshot;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkModuleInventory
{
    internal readonly ManagedModuleInventorySnapshot ToManagedSnapshot()
    {
        return new ManagedModuleInventorySnapshot(
            usb_c_slot_count,
            input_top_row_count,
            detached_count,
            usb_c_slot_0.ToManagedSnapshot(),
            usb_c_slot_1.ToManagedSnapshot(),
            usb_c_slot_2.ToManagedSnapshot(),
            usb_c_slot_3.ToManagedSnapshot(),
            usb_c_slot_4.ToManagedSnapshot(),
            usb_c_slot_5.ToManagedSnapshot(),
            input_top_row_0.ToManagedSnapshot(),
            input_top_row_1.ToManagedSnapshot(),
            input_top_row_2.ToManagedSnapshot(),
            input_top_row_3.ToManagedSnapshot(),
            input_top_row_4.ToManagedSnapshot(),
            input_touchpad.ToManagedSnapshot(),
            internal_keyboard.ToManagedSnapshot(),
            internal_touchpad.ToManagedSnapshot(),
            fingerprint_reader.ToManagedSnapshot(),
            touchscreen.ToManagedSnapshot(),
            webcam.ToManagedSnapshot(),
            expansion_bay.ToManagedSnapshot(),
            detached_0.ToManagedSnapshot(),
            detached_1.ToManagedSnapshot(),
            detached_2.ToManagedSnapshot(),
            detached_3.ToManagedSnapshot());
    }
}
