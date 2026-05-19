using FrameworkDotnet.Attributes;

namespace FrameworkDotnet.Enums;

/// <summary>
/// Represents the logical slot category a module belongs to.
/// </summary>
public enum FrameworkModuleSlotKind : byte
{
    /// <summary>
    /// No slot is assigned.
    /// </summary>
    None = 0,

    /// <summary>
    /// USB-C expansion-card slot.
    /// </summary>
    UsbCPort = 1,

    /// <summary>
    /// Framework 16 top-row or input deck slot.
    /// </summary>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "This slot kind is specific to Framework Laptop 16 input modules.")]
    InputDeckTopRow = 2,

    /// <summary>
    /// Framework 16 touchpad or input deck position.
    /// </summary>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "This slot kind is specific to Framework Laptop 16 input modules.")]
    InputDeckTouchpad = 3,

    /// <summary>
    /// Expansion bay slot.
    /// </summary>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "This slot kind is specific to Framework Laptop 16 expansion-bay inventory.")]
    ExpansionBay = 4,

    /// <summary>
    /// Built-in fixed internal component.
    /// </summary>
    InternalFixed = 5,

    /// <summary>
    /// An observed device or module that could not be mapped confidently to a fixed slot.
    /// </summary>
    Detached = 6,
}
