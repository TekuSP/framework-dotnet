namespace FrameworkDotnet.Enums;

/// <summary>
/// Identifies a fixed USB Power Delivery controller slot reported by the embedded controller.
/// </summary>
/// <remarks>
/// The slot order is fixed by the native probe order and never varies: index 0 is <see cref="Right01"/>, index 1 is <see cref="Left23"/> and index 2 is <see cref="Back"/>.
/// Framework laptops populate <see cref="Right01"/> and <see cref="Left23"/>; Framework Desktop populates <see cref="Back"/> only.
/// Because the populated slots are not contiguous across platform families, always test the presence flag on a controller before reading its firmware versions.
/// </remarks>
public enum FrameworkPowerDeliveryControllerSlot
{
    /// <summary>
    /// The controller driving the right-hand pair of USB-C ports, reported in slot index 0.
    /// </summary>
    Right01 = 0,

    /// <summary>
    /// The controller driving the left-hand pair of USB-C ports, reported in slot index 1.
    /// </summary>
    Left23 = 1,

    /// <summary>
    /// The rear controller of a Framework Desktop, reported in slot index 2.
    /// </summary>
    Back = 2,
}
