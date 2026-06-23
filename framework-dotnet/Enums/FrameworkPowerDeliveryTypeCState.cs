namespace FrameworkDotnet.Enums;

/// <summary>
/// Represents the USB Type-C physical connection state reported by the PD controller.
/// </summary>
public enum FrameworkPowerDeliveryTypeCState
{
    /// <summary>
    /// No connection is present.
    /// </summary>
    Nothing = 0,

    /// <summary>
    /// UFP/sink role — device is consuming power or data from this port.
    /// </summary>
    Sink = 1,

    /// <summary>
    /// DFP/source role — device is providing power or data from this port.
    /// </summary>
    Source = 2,

    /// <summary>
    /// Debug accessory mode.
    /// </summary>
    Debug = 3,

    /// <summary>
    /// Audio accessory mode.
    /// </summary>
    Audio = 4,

    /// <summary>
    /// Powered accessory mode.
    /// </summary>
    PoweredAccessory = 5,

    /// <summary>
    /// Unsupported connection state.
    /// </summary>
    Unsupported = 6,

    /// <summary>
    /// Unrecognized EC state value.
    /// </summary>
    Invalid = 7,
}
