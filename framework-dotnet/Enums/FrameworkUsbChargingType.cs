namespace FrameworkDotnet.Enums;

/// <summary>
/// Represents how an attached charger is supplying power to a USB-C port.
/// </summary>
public enum FrameworkUsbChargingType
{
    /// <summary>
    /// No charger is supplying power.
    /// </summary>
    None = 0,

    /// <summary>
    /// Power is supplied through a negotiated USB Power Delivery contract.
    /// </summary>
    Pd = 1,

    /// <summary>
    /// Power is supplied through USB Type-C current advertisement.
    /// </summary>
    TypeC = 2,

    /// <summary>
    /// Power is supplied through a proprietary charging scheme.
    /// </summary>
    Proprietary = 3,

    /// <summary>
    /// Power is supplied by a USB Battery Charging 1.2 dedicated charging port.
    /// </summary>
    Bc12Dcp = 4,

    /// <summary>
    /// Power is supplied by a USB Battery Charging 1.2 charging downstream port.
    /// </summary>
    Bc12Cdp = 5,

    /// <summary>
    /// Power is supplied by a USB Battery Charging 1.2 standard downstream port.
    /// </summary>
    Bc12Sdp = 6,

    /// <summary>
    /// Power is supplied by a charger that does not match any of the classified types.
    /// </summary>
    Other = 7,

    /// <summary>
    /// Power is supplied over VBUS without a recognised charging protocol.
    /// </summary>
    VBus = 8,

    /// <summary>
    /// The charging type could not be determined.
    /// </summary>
    Unknown = 9,
}
