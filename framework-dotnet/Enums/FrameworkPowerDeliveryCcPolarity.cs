namespace FrameworkDotnet.Enums;

/// <summary>
/// Represents the USB-C CC pin orientation or debug-accessory mode reported by the PD controller.
/// </summary>
public enum FrameworkPowerDeliveryCcPolarity
{
    /// <summary>
    /// Polarity could not be determined.
    /// </summary>
    Unknown = -1,

    /// <summary>
    /// CC1 is active; cable is in normal orientation.
    /// </summary>
    Cc1 = 0,

    /// <summary>
    /// CC2 is active; cable is flipped.
    /// </summary>
    Cc2 = 1,

    /// <summary>
    /// Debug accessory on CC1.
    /// </summary>
    Cc1Debug = 2,

    /// <summary>
    /// Debug accessory on CC2.
    /// </summary>
    Cc2Debug = 3,
}
