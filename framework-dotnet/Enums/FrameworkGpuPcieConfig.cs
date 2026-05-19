using FrameworkDotnet.Attributes;

namespace FrameworkDotnet.Enums;

/// <summary>
/// Represents the reported PCIe lane and generation configuration for a GPU or accessory path.
/// </summary>
[FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "Upstream framework-system currently documents expansion-bay status support on Framework Laptop 16 only.")]
public enum FrameworkGpuPcieConfig
{
    /// <summary>
    /// The PCIe configuration could not be determined.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// PCIe Gen 4 x1.
    /// </summary>
    Pcie4x1 = 1,

    /// <summary>
    /// PCIe Gen 4 x2.
    /// </summary>
    Pcie4x2 = 2,

    /// <summary>
    /// PCIe Gen 4 x4.
    /// </summary>
    Pcie4x4 = 3,

    /// <summary>
    /// PCIe Gen 5 x4.
    /// </summary>
    Pcie5x4 = 4,
}
