namespace FrameworkDotnet.Enums;

/// <summary>
/// Represents the state of a temperature reading.
/// </summary>
public enum FrameworkTemperatureState
{
    /// <summary>
    /// The reading is valid.
    /// </summary>
    Ok = 0,

    /// <summary>
    /// The sensor is not present.
    /// </summary>
    NotPresent = 1,

    /// <summary>
    /// The sensor reported an error.
    /// </summary>
    Error = 2,

    /// <summary>
    /// The sensor is not powered.
    /// </summary>
    NotPowered = 3,

    /// <summary>
    /// The sensor is not calibrated.
    /// </summary>
    NotCalibrated = 4,
}
