namespace FrameworkDotnet.Enums;

/// <summary>
/// Represents the current battery state.
/// </summary>
public enum FrameworkBatteryState : byte
{
    /// <summary>
    /// No battery is present.
    /// </summary>
    NotPresent = 0,

    /// <summary>
    /// The battery is present and idle.
    /// </summary>
    Idle = 1,

    /// <summary>
    /// The battery is present and charging.
    /// </summary>
    Charging = 2,

    /// <summary>
    /// The battery is present and discharging.
    /// </summary>
    Discharging = 3,

    /// <summary>
    /// The battery is simultaneously charging and discharging.
    /// </summary>
    ChargingAndDischarging = 4,

    /// <summary>
    /// The battery is in a critical charge state.
    /// </summary>
    Critical = 5,
}