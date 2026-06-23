using FrameworkDotnet.Attributes;

namespace FrameworkDotnet.Enums;

/// <summary>
/// Represents the best-effort classification for a detected module or slot occupant.
/// </summary>
public enum FrameworkModuleIdentity
{
    /// <summary>
    /// No module or identity is assigned.
    /// </summary>
    None = 0,

    /// <summary>
    /// Something is present in a USB-C slot, but the type could not be identified.
    /// </summary>
    UnknownUsbCOccupant = 1,

    /// <summary>
    /// DisplayPort expansion card.
    /// </summary>
    DpExpansionCard = 2,

    /// <summary>
    /// HDMI expansion card.
    /// </summary>
    HdmiExpansionCard = 3,

    /// <summary>
    /// Audio expansion card.
    /// </summary>
    AudioExpansionCard = 4,

    /// <summary>
    /// Framework 16 keyboard top-row or input module.
    /// </summary>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "This module identity is specific to Framework Laptop 16 input modules.")]
    Framework16KeyboardModule = 5,

    /// <summary>
    /// Framework 16 LED matrix top-row module.
    /// </summary>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "This module identity is specific to Framework Laptop 16 input modules.")]
    Framework16LedMatrix = 6,

    /// <summary>
    /// Framework 16 touchpad or input module.
    /// </summary>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "This module identity is specific to Framework Laptop 16 input modules.")]
    Framework16TouchpadModule = 7,

    /// <summary>
    /// Built-in keyboard.
    /// </summary>
    InternalKeyboard = 8,

    /// <summary>
    /// Built-in touchpad.
    /// </summary>
    InternalTouchpad = 9,

    /// <summary>
    /// Fingerprint reader.
    /// </summary>
    FingerprintReader = 10,

    /// <summary>
    /// Internal touchscreen.
    /// </summary>
    Touchscreen = 11,

    /// <summary>
    /// Internal webcam.
    /// </summary>
    Webcam = 12,

    /// <summary>
    /// Expansion bay present with only a generic classification.
    /// </summary>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "This module identity is specific to Framework Laptop 16 expansion-bay inventory.")]
    ExpansionBay = 13,

    /// <summary>
    /// Expansion bay with a dual interposer board.
    /// </summary>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "This module identity is specific to Framework Laptop 16 expansion-bay inventory.")]
    ExpansionBayDualInterposer = 14,

    /// <summary>
    /// Expansion bay with a single interposer board.
    /// </summary>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "This module identity is specific to Framework Laptop 16 expansion-bay inventory.")]
    ExpansionBaySingleInterposer = 15,

    /// <summary>
    /// Expansion bay with a UMA or fan board.
    /// </summary>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "This module identity is specific to Framework Laptop 16 expansion-bay inventory.")]
    ExpansionBayUmaFans = 16,

    /// <summary>
    /// Expansion bay SSD holder module.
    /// </summary>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "This module identity is specific to Framework Laptop 16 expansion-bay inventory.")]
    ExpansionBaySsdHolder = 17,

    /// <summary>
    /// Expansion bay PCIe accessory module.
    /// </summary>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "This module identity is specific to Framework Laptop 16 expansion-bay inventory.")]
    ExpansionBayPcieAccessory = 18,

    /// <summary>
    /// Expansion bay AMD GPU module.
    /// </summary>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "This module identity is specific to Framework Laptop 16 expansion-bay inventory.")]
    ExpansionBayAmdGpu = 19,

    /// <summary>
    /// Expansion bay NVIDIA GPU module.
    /// </summary>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "This module identity is specific to Framework Laptop 16 expansion-bay inventory.")]
    ExpansionBayNvidiaGpu = 20,

    /// <summary>
    /// Expansion bay fan-only module.
    /// </summary>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "This module identity is specific to Framework Laptop 16 expansion-bay inventory.")]
    ExpansionBayFanOnly = 21,

    /// <summary>
    /// USB-A expansion card (USB hub).
    /// </summary>
    UsbAExpansionCard = 22,

    /// <summary>
    /// USB-C expansion card (passive passthrough).
    /// </summary>
    UsbCExpansionCard = 23,

    /// <summary>
    /// Ethernet 2.5G expansion card (Realtek RTL8156B).
    /// </summary>
    EthernetExpansionCard = 24,

    /// <summary>
    /// Ethernet 10G expansion card (WisdPi).
    /// </summary>
    Ethernet10GExpansionCard = 25,

    /// <summary>
    /// MicroSD expansion card.
    /// </summary>
    MicroSdExpansionCard = 26,

    /// <summary>
    /// Full-size SD expansion card.
    /// </summary>
    SdExpansionCard = 27,

    /// <summary>
    /// NVMe storage expansion card.
    /// </summary>
    SsdExpansionCard = 28,
}
