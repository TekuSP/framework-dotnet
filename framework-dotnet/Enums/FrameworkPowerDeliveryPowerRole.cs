namespace FrameworkDotnet.Enums;

/// <summary>
/// Represents the USB PD power role for a port.
/// </summary>
public enum FrameworkPowerDeliveryPowerRole
{
    /// <summary>
    /// Port is consuming power (sink).
    /// </summary>
    Sink = 0,

    /// <summary>
    /// Port is providing power (source).
    /// </summary>
    Source = 1,

    /// <summary>
    /// Power role could not be determined.
    /// </summary>
    Unknown = 2,
}
