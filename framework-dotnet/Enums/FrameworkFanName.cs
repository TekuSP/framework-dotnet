using FrameworkDotnet.Attributes;

namespace FrameworkDotnet.Enums;

/// <summary>
/// Represents the platform-specific role name for a fan slot.
/// </summary>
public enum FrameworkFanName : ushort
{
    /// <summary>
    /// Platform family could not be determined; slot role is indeterminate.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// Family known but no specific name assigned to this slot.
    /// </summary>
    Generic = 1,

    /// <summary>
    /// First fan on Framework 12, 13, or Desktop (APU/CPU fan, slot 0).
    /// </summary>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework12, FrameworkPlatformFamily.Framework13, FrameworkPlatformFamily.FrameworkDesktop, Message = "ApuFan is assigned on Framework 12, 13, and Desktop. Framework 16 uses LeftFan/RightFan instead.")]
    ApuFan = 2,

    /// <summary>
    /// Framework 16 left fan (slot 0).
    /// </summary>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "LeftFan is specific to Framework Laptop 16.")]
    LeftFan = 3,

    /// <summary>
    /// Framework 16 right fan (slot 1).
    /// </summary>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "RightFan is specific to Framework Laptop 16.")]
    RightFan = 4,

    /// <summary>
    /// Framework Desktop front fan (slot 1).
    /// </summary>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.FrameworkDesktop, Message = "FrontFan is specific to Framework Desktop.")]
    FrontFan = 5,

    /// <summary>
    /// Framework Desktop third fan (slot 2).
    /// </summary>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.FrameworkDesktop, Message = "ThirdFan is specific to Framework Desktop.")]
    ThirdFan = 6,
}
