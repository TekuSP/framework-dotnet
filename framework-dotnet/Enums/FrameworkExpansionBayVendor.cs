using FrameworkDotnet.Attributes;

namespace FrameworkDotnet.Enums;

/// <summary>
/// Represents the occupant or vendor family currently associated with the expansion bay.
/// </summary>
[FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "Upstream framework-system currently documents expansion-bay status support on Framework Laptop 16 only.")]
public enum FrameworkExpansionBayVendor
{
    /// <summary>
    /// The vendor or occupant family could not be identified.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// The bay or module is still initializing.
    /// </summary>
    Initializing = 1,

    /// <summary>
    /// The bay is populated only by a fan assembly.
    /// </summary>
    FanOnly = 2,

    /// <summary>
    /// An SSD holder or storage module is present.
    /// </summary>
    SsdHolder = 3,

    /// <summary>
    /// A generic PCIe accessory module is present.
    /// </summary>
    PcieAccessory = 4,

    /// <summary>
    /// An AMD GPU module is present.
    /// </summary>
    AmdGpu = 5,

    /// <summary>
    /// An NVIDIA GPU module is present.
    /// </summary>
    NvidiaGpu = 6,
}
