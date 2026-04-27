namespace FrameworkDotnet.Enums;

/// <summary>
/// Represents the current system power source state.
/// </summary>
public enum FrameworkPowerSourceState : byte
{
    /// <summary>
    /// No power source information is available.
    /// </summary>
    None = 0,

    /// <summary>
    /// The system is powered only by AC.
    /// </summary>
    AcOnly = 1,

    /// <summary>
    /// The system is powered only by battery.
    /// </summary>
    BatteryOnly = 2,

    /// <summary>
    /// The system is powered by AC with battery present.
    /// </summary>
    AcAndBattery = AcOnly + BatteryOnly,
}
