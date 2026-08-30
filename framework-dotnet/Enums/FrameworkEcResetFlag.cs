namespace FrameworkDotnet.Enums;

/// <summary>
/// Represents the reasons recorded by the embedded controller for its most recent reset.
/// </summary>
[System.Flags]
public enum FrameworkEcResetFlag : uint
{
    /// <summary>
    /// The reset had a cause that is not covered by any other flag.
    /// </summary>
    Other = 0x00000001,

    /// <summary>
    /// The reset was triggered through the reset pin.
    /// </summary>
    ResetPin = 0x00000002,

    /// <summary>
    /// The reset was caused by a brownout.
    /// </summary>
    Brownout = 0x00000004,

    /// <summary>
    /// The reset was caused by the supply being powered on.
    /// </summary>
    PowerOn = 0x00000008,

    /// <summary>
    /// The reset was caused by the watchdog timer.
    /// </summary>
    Watchdog = 0x00000010,

    /// <summary>
    /// The reset was a soft reset requested in firmware.
    /// </summary>
    Soft = 0x00000020,

    /// <summary>
    /// The reset resumed the controller from hibernation.
    /// </summary>
    Hibernate = 0x00000040,

    /// <summary>
    /// The reset was triggered by a real-time clock alarm.
    /// </summary>
    RtcAlarm = 0x00000080,

    /// <summary>
    /// The reset was triggered through a wake pin.
    /// </summary>
    WakePin = 0x00000100,

    /// <summary>
    /// The reset was caused by a low battery condition.
    /// </summary>
    LowBattery = 0x00000200,

    /// <summary>
    /// The reset was caused by a jump between firmware images.
    /// </summary>
    Sysjump = 0x00000400,

    /// <summary>
    /// The reset was a hard reset.
    /// </summary>
    Hard = 0x00000800,

    /// <summary>
    /// The application processor was off across the reset.
    /// </summary>
    ApOff = 0x00001000,

    /// <summary>
    /// The reset flags were preserved across the reset.
    /// </summary>
    Preserved = 0x00002000,

    /// <summary>
    /// The reset resumed the controller from a USB resume event.
    /// </summary>
    UsbResume = 0x00004000,

    /// <summary>
    /// The reset was triggered by the debug detection module.
    /// </summary>
    Rdd = 0x00008000,

    /// <summary>
    /// The reset was triggered by the reset box.
    /// </summary>
    Rbox = 0x00010000,

    /// <summary>
    /// The reset was triggered by a security event.
    /// </summary>
    Security = 0x00020000,

    /// <summary>
    /// The reset was triggered by the application processor watchdog.
    /// </summary>
    ApWatchdog = 0x00040000,

    /// <summary>
    /// The controller was asked to stay in the read-only image after the reset.
    /// </summary>
    StayInRo = 0x00080000,

    /// <summary>
    /// The reset was caused by early firmware selection.
    /// </summary>
    Efs = 0x00100000,

    /// <summary>
    /// The application processor was idle across the reset.
    /// </summary>
    ApIdle = 0x00200000,

    /// <summary>
    /// The reset was the initial power-up of the controller.
    /// </summary>
    InitialPwr = 0x00400000,
}
