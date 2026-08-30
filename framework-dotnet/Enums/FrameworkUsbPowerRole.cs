namespace FrameworkDotnet.Enums;

/// <summary>
/// Represents the power role a USB Power Delivery port has negotiated.
/// </summary>
public enum FrameworkUsbPowerRole
{
    /// <summary>
    /// Nothing is attached to the port.
    /// </summary>
    Disconnected = 0,

    /// <summary>
    /// The port is providing power to the attached device.
    /// </summary>
    Source = 1,

    /// <summary>
    /// The port is consuming power from the attached device.
    /// </summary>
    Sink = 2,

    /// <summary>
    /// The port is a sink but is not currently drawing charge.
    /// </summary>
    SinkNotCharging = 3,
}
