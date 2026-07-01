namespace FrameworkDotnet.Enums;

/// <summary>
/// Physical position of an EC USB-C Power Delivery port, mirroring upstream framework-system
/// (<c>power.rs get_and_print_pd_info</c>): index 0 = Right Back, 1 = Right Middle/Front, 2 = Left Middle/Front,
/// 3 = Left Back — "Middle" on Framework Laptop 16, "Front" on the other laptops.
/// </summary>
public enum FrameworkUsbCPortPosition : byte
{
    /// <summary>
    /// No documented position for this port/platform.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// Right side, rear-most port.
    /// </summary>
    RightBack = 1,

    /// <summary>
    /// Right side, middle port (Framework Laptop 16).
    /// </summary>
    RightMiddle = 2,

    /// <summary>
    /// Right side, front-most port (Framework Laptop 12/13).
    /// </summary>
    RightFront = 3,

    /// <summary>
    /// Left side, middle port (Framework Laptop 16).
    /// </summary>
    LeftMiddle = 4,

    /// <summary>
    /// Left side, front-most port (Framework Laptop 12/13).
    /// </summary>
    LeftFront = 5,

    /// <summary>
    /// Left side, rear-most port.
    /// </summary>
    LeftBack = 6,

    /// <summary>
    /// The Framework Laptop 16 graphics-module rear USB-C port.
    /// </summary>
    GraphicsModule = 7,
}
