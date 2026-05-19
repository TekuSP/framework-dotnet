using FrameworkDotnet.Attributes;

namespace FrameworkDotnet.Enums;

/// <summary>
/// Represents the known GPU descriptor header magic signatures.
/// </summary>
[FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "Upstream framework-system currently documents the expansion-bay GPU descriptor surface on Framework Laptop 16 only.")]
public enum FrameworkGpuDescriptorMagic
{
    /// <summary>
    /// The raw magic bytes do not match a known descriptor header signature.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// The raw magic bytes are zeroed, indicating that no expansion bay descriptor header is present.
    /// </summary>
    NoExpansionBay = 1,

    /// <summary>
    /// The known Framework expansion bay descriptor header signature.
    /// </summary>
    FrameworkExpansionBay = 2,
}
