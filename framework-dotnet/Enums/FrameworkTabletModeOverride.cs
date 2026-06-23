using FrameworkDotnet.Attributes;

namespace FrameworkDotnet.Enums;

/// <summary>
/// Represents a tablet mode override directive sent to the EC.
/// </summary>
/// <remarks>Returns an EC response failure on Framework 16 and Desktop platforms that lack a tablet hinge sensor.</remarks>
[FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework13, Message = "Tablet mode is supported on Framework 12 and 13. Framework 16 and Desktop return EcResponse(InvalidCommand).")]
public enum FrameworkTabletModeOverride
{
    /// <summary>
    /// Let the EC determine tablet mode from the hinge sensor.
    /// </summary>
    Default = 0,

    /// <summary>
    /// Force the system into tablet mode regardless of hinge position.
    /// </summary>
    ForceTablet = 1,

    /// <summary>
    /// Force the system into clamshell (laptop) mode regardless of hinge position.
    /// </summary>
    ForceClamshell = 2,
}
