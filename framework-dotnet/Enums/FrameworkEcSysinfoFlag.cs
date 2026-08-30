namespace FrameworkDotnet.Enums;

/// <summary>
/// Represents the system information flags reported by the embedded controller.
/// </summary>
[System.Flags]
public enum FrameworkEcSysinfoFlag : uint
{
    /// <summary>
    /// Write protect is asserted and debug features are disabled.
    /// </summary>
    Locked = 0x01,

    /// <summary>
    /// The embedded controller is locked even though write protect is deasserted.
    /// </summary>
    ForceLocked = 0x02,

    /// <summary>
    /// Jumping to another firmware image is enabled.
    /// </summary>
    JumpEnabled = 0x04,

    /// <summary>
    /// The embedded controller jumped to the image it is currently running.
    /// </summary>
    JumpedToCurrentImage = 0x08,

    /// <summary>
    /// The embedded controller will reboot when the host shuts down.
    /// </summary>
    RebootAtShutdown = 0x10,

    /// <summary>
    /// The system is in manual recovery mode.
    /// </summary>
    InManualRecovery = 0x20,
}
