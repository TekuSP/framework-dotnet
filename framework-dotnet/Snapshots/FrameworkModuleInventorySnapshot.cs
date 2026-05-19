using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents a fixed-size inventory snapshot covering known module slot categories.
/// </summary>
public sealed record FrameworkModuleInventorySnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkModuleInventorySnapshot"/> class.
    /// </summary>
    /// <param name="usbCSlotCount">The number of meaningful USB-C slot entries.</param>
    /// <param name="inputTopRowCount">The number of meaningful top-row or input deck entries.</param>
    /// <param name="detachedCount">The number of meaningful detached entries.</param>
    /// <param name="usbCSlot_0">The first USB-C slot descriptor.</param>
    /// <param name="usbCSlot_1">The second USB-C slot descriptor.</param>
    /// <param name="usbCSlot_2">The third USB-C slot descriptor.</param>
    /// <param name="usbCSlot_3">The fourth USB-C slot descriptor.</param>
    /// <param name="usbCSlot_4">The fifth USB-C slot descriptor.</param>
    /// <param name="usbCSlot_5">The sixth USB-C slot descriptor.</param>
    /// <param name="inputTopRow_0">The first top-row descriptor.</param>
    /// <param name="inputTopRow_1">The second top-row descriptor.</param>
    /// <param name="inputTopRow_2">The third top-row descriptor.</param>
    /// <param name="inputTopRow_3">The fourth top-row descriptor.</param>
    /// <param name="inputTopRow_4">The fifth top-row descriptor.</param>
    /// <param name="inputTouchpad">The input deck touchpad descriptor.</param>
    /// <param name="internalKeyboard">The built-in keyboard descriptor.</param>
    /// <param name="internalTouchpad">The built-in touchpad descriptor.</param>
    /// <param name="fingerprintReader">The fingerprint reader descriptor.</param>
    /// <param name="touchscreen">The touchscreen descriptor.</param>
    /// <param name="webcam">The webcam descriptor.</param>
    /// <param name="expansionBay">The expansion bay descriptor.</param>
    /// <param name="detached_0">The first detached descriptor.</param>
    /// <param name="detached_1">The second detached descriptor.</param>
    /// <param name="detached_2">The third detached descriptor.</param>
    /// <param name="detached_3">The fourth detached descriptor.</param>
    public FrameworkModuleInventorySnapshot(byte usbCSlotCount, byte inputTopRowCount, byte detachedCount, FrameworkModuleDescriptorSnapshot usbCSlot_0, FrameworkModuleDescriptorSnapshot usbCSlot_1, FrameworkModuleDescriptorSnapshot usbCSlot_2, FrameworkModuleDescriptorSnapshot usbCSlot_3, FrameworkModuleDescriptorSnapshot usbCSlot_4, FrameworkModuleDescriptorSnapshot usbCSlot_5, FrameworkModuleDescriptorSnapshot inputTopRow_0, FrameworkModuleDescriptorSnapshot inputTopRow_1, FrameworkModuleDescriptorSnapshot inputTopRow_2, FrameworkModuleDescriptorSnapshot inputTopRow_3, FrameworkModuleDescriptorSnapshot inputTopRow_4, FrameworkModuleDescriptorSnapshot inputTouchpad, FrameworkModuleDescriptorSnapshot internalKeyboard, FrameworkModuleDescriptorSnapshot internalTouchpad, FrameworkModuleDescriptorSnapshot fingerprintReader, FrameworkModuleDescriptorSnapshot touchscreen, FrameworkModuleDescriptorSnapshot webcam, FrameworkModuleDescriptorSnapshot expansionBay, FrameworkModuleDescriptorSnapshot detached_0, FrameworkModuleDescriptorSnapshot detached_1, FrameworkModuleDescriptorSnapshot detached_2, FrameworkModuleDescriptorSnapshot detached_3)
    {
        UsbCSlotCount = usbCSlotCount;
        InputTopRowCount = inputTopRowCount;
        DetachedCount = detachedCount;
        UsbCSlot_0 = usbCSlot_0;
        UsbCSlot_1 = usbCSlot_1;
        UsbCSlot_2 = usbCSlot_2;
        UsbCSlot_3 = usbCSlot_3;
        UsbCSlot_4 = usbCSlot_4;
        UsbCSlot_5 = usbCSlot_5;
        InputTopRow_0 = inputTopRow_0;
        InputTopRow_1 = inputTopRow_1;
        InputTopRow_2 = inputTopRow_2;
        InputTopRow_3 = inputTopRow_3;
        InputTopRow_4 = inputTopRow_4;
        InputTouchpad = inputTouchpad;
        InternalKeyboard = internalKeyboard;
        InternalTouchpad = internalTouchpad;
        FingerprintReader = fingerprintReader;
        Touchscreen = touchscreen;
        Webcam = webcam;
        ExpansionBay = expansionBay;
        Detached_0 = detached_0;
        Detached_1 = detached_1;
        Detached_2 = detached_2;
        Detached_3 = detached_3;
    }

    /// <summary>
    /// Gets the number of meaningful USB-C slot entries.
    /// </summary>
    public byte UsbCSlotCount { get; init; }

    /// <summary>
    /// Gets the number of meaningful top-row or input deck entries.
    /// </summary>
    public byte InputTopRowCount { get; init; }

    /// <summary>
    /// Gets the number of meaningful detached entries.
    /// </summary>
    public byte DetachedCount { get; init; }

    /// <summary>
    /// Gets the first USB-C slot descriptor.
    /// </summary>
    public FrameworkModuleDescriptorSnapshot UsbCSlot_0 { get; init; }

    /// <summary>
    /// Gets the second USB-C slot descriptor.
    /// </summary>
    public FrameworkModuleDescriptorSnapshot UsbCSlot_1 { get; init; }

    /// <summary>
    /// Gets the third USB-C slot descriptor.
    /// </summary>
    public FrameworkModuleDescriptorSnapshot UsbCSlot_2 { get; init; }

    /// <summary>
    /// Gets the fourth USB-C slot descriptor.
    /// </summary>
    public FrameworkModuleDescriptorSnapshot UsbCSlot_3 { get; init; }

    /// <summary>
    /// Gets the fifth USB-C slot descriptor.
    /// </summary>
    public FrameworkModuleDescriptorSnapshot UsbCSlot_4 { get; init; }

    /// <summary>
    /// Gets the sixth USB-C slot descriptor.
    /// </summary>
    public FrameworkModuleDescriptorSnapshot UsbCSlot_5 { get; init; }

    /// <summary>
    /// Gets the first top-row descriptor.
    /// </summary>
    public FrameworkModuleDescriptorSnapshot InputTopRow_0 { get; init; }

    /// <summary>
    /// Gets the second top-row descriptor.
    /// </summary>
    public FrameworkModuleDescriptorSnapshot InputTopRow_1 { get; init; }

    /// <summary>
    /// Gets the third top-row descriptor.
    /// </summary>
    public FrameworkModuleDescriptorSnapshot InputTopRow_2 { get; init; }

    /// <summary>
    /// Gets the fourth top-row descriptor.
    /// </summary>
    public FrameworkModuleDescriptorSnapshot InputTopRow_3 { get; init; }

    /// <summary>
    /// Gets the fifth top-row descriptor.
    /// </summary>
    public FrameworkModuleDescriptorSnapshot InputTopRow_4 { get; init; }

    /// <summary>
    /// Gets the input deck touchpad descriptor.
    /// </summary>
    public FrameworkModuleDescriptorSnapshot InputTouchpad { get; init; }

    /// <summary>
    /// Gets the built-in keyboard descriptor.
    /// </summary>
    public FrameworkModuleDescriptorSnapshot InternalKeyboard { get; init; }

    /// <summary>
    /// Gets the built-in touchpad descriptor.
    /// </summary>
    public FrameworkModuleDescriptorSnapshot InternalTouchpad { get; init; }

    /// <summary>
    /// Gets the fingerprint reader descriptor.
    /// </summary>
    public FrameworkModuleDescriptorSnapshot FingerprintReader { get; init; }

    /// <summary>
    /// Gets the touchscreen descriptor.
    /// </summary>
    public FrameworkModuleDescriptorSnapshot Touchscreen { get; init; }

    /// <summary>
    /// Gets the webcam descriptor.
    /// </summary>
    public FrameworkModuleDescriptorSnapshot Webcam { get; init; }

    /// <summary>
    /// Gets the expansion bay descriptor.
    /// </summary>
    public FrameworkModuleDescriptorSnapshot ExpansionBay { get; init; }

    /// <summary>
    /// Gets the first detached descriptor.
    /// </summary>
    public FrameworkModuleDescriptorSnapshot Detached_0 { get; init; }

    /// <summary>
    /// Gets the second detached descriptor.
    /// </summary>
    public FrameworkModuleDescriptorSnapshot Detached_1 { get; init; }

    /// <summary>
    /// Gets the third detached descriptor.
    /// </summary>
    public FrameworkModuleDescriptorSnapshot Detached_2 { get; init; }

    /// <summary>
    /// Gets the fourth detached descriptor.
    /// </summary>
    public FrameworkModuleDescriptorSnapshot Detached_3 { get; init; }

    /// <summary>
    /// Gets the USB-C slot descriptors in index order.
    /// </summary>
    public IReadOnlyList<FrameworkModuleDescriptorSnapshot> UsbCSlots => [UsbCSlot_0, UsbCSlot_1, UsbCSlot_2, UsbCSlot_3, UsbCSlot_4, UsbCSlot_5];

    /// <summary>
    /// Gets the reported USB-C slot descriptors in index order.
    /// </summary>
    /// <seealso cref="UsbCSlotCount"/>
    public IEnumerable<FrameworkModuleDescriptorSnapshot> ReportedUsbCSlots => UsbCSlots.Take(UsbCSlotCount);

    /// <summary>
    /// Gets the top-row descriptors in index order.
    /// </summary>
    public IReadOnlyList<FrameworkModuleDescriptorSnapshot> InputTopRowModules => [InputTopRow_0, InputTopRow_1, InputTopRow_2, InputTopRow_3, InputTopRow_4];

    /// <summary>
    /// Gets the reported top-row descriptors in index order.
    /// </summary>
    /// <seealso cref="InputTopRowCount"/>
    public IEnumerable<FrameworkModuleDescriptorSnapshot> ReportedInputTopRowModules => InputTopRowModules.Take(InputTopRowCount);

    /// <summary>
    /// Gets the detached descriptors in index order.
    /// </summary>
    public IReadOnlyList<FrameworkModuleDescriptorSnapshot> DetachedModules => [Detached_0, Detached_1, Detached_2, Detached_3];

    /// <summary>
    /// Gets the reported detached descriptors in index order.
    /// </summary>
    /// <seealso cref="DetachedCount"/>
    public IEnumerable<FrameworkModuleDescriptorSnapshot> ReportedDetachedModules => DetachedModules.Take(DetachedCount);

    /// <summary>
    /// Gets the fixed internal and expansion-bay descriptors in index order.
    /// </summary>
    public IReadOnlyList<FrameworkModuleDescriptorSnapshot> FixedModules => [InputTouchpad, InternalKeyboard, InternalTouchpad, FingerprintReader, Touchscreen, Webcam, ExpansionBay];

    /// <summary>
    /// Gets the number of reported fixed internal and expansion-bay descriptors.
    /// </summary>
    public byte FixedModuleCount => (byte)FixedModules.Count(static module => module.IsPresent);

    /// <summary>
    /// Gets the reported fixed internal and expansion-bay descriptors in index order.
    /// </summary>
    /// <seealso cref="FixedModuleCount"/>
    public IEnumerable<FrameworkModuleDescriptorSnapshot> ReportedFixedModules => FixedModules.Where(static module => module.IsPresent);

    /// <summary>
    /// Gets all module descriptors in index order.
    /// </summary>
    public IReadOnlyList<FrameworkModuleDescriptorSnapshot> Modules => [UsbCSlot_0, UsbCSlot_1, UsbCSlot_2, UsbCSlot_3, UsbCSlot_4, UsbCSlot_5, InputTopRow_0, InputTopRow_1, InputTopRow_2, InputTopRow_3, InputTopRow_4, InputTouchpad, InternalKeyboard, InternalTouchpad, FingerprintReader, Touchscreen, Webcam, ExpansionBay, Detached_0, Detached_1, Detached_2, Detached_3];

    /// <summary>
    /// Gets the number of reported modules across all slot groups.
    /// </summary>
    public byte ModuleCount => (byte)(UsbCSlotCount + InputTopRowCount + DetachedCount + FixedModuleCount);

    /// <summary>
    /// Gets the reported module descriptors in index order.
    /// </summary>
    /// <seealso cref="ModuleCount"/>
    public IEnumerable<FrameworkModuleDescriptorSnapshot> ReportedModules => ReportedUsbCSlots.Concat(ReportedInputTopRowModules).Concat(ReportedFixedModules).Concat(ReportedDetachedModules);

    public override string ToString()
    {
        return $"Module Inventory Snapshot: Module Count: {ModuleCount.ToString(CultureInfo.InvariantCulture)}, USB-C Slot Count: {UsbCSlotCount.ToString(CultureInfo.InvariantCulture)}, Input Top Row Count: {InputTopRowCount.ToString(CultureInfo.InvariantCulture)}, Fixed Module Count: {FixedModuleCount.ToString(CultureInfo.InvariantCulture)}, Detached Count: {DetachedCount.ToString(CultureInfo.InvariantCulture)}, USB-C Slots: {string.Join(", ", ReportedUsbCSlots)}, Input Top Row Modules: {string.Join(", ", ReportedInputTopRowModules)}, Fixed Modules: {string.Join(", ", ReportedFixedModules)}, Detached Modules: {string.Join(", ", ReportedDetachedModules)}";
    }
}
