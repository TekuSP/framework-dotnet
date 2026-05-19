namespace FrameworkDotnet.Enums;

/// <summary>
/// Represents additional flags associated with a detected module.
/// </summary>
[System.Flags]
public enum FrameworkModuleFlags : uint
{
    /// <summary>
    /// No additional module flags are reported.
    /// </summary>
    None = 0,

    /// <summary>
    /// The module is built into the system.
    /// </summary>
    BuiltIn = 1U << 0,

    /// <summary>
    /// The module appears active or in use.
    /// </summary>
    Active = 1U << 1,

    /// <summary>
    /// The module or occupant appears electrically or logically connected.
    /// </summary>
    Connected = 1U << 2,

    /// <summary>
    /// A fault condition is present.
    /// </summary>
    Fault = 1U << 3,

    /// <summary>
    /// The classification is ambiguous or tentative.
    /// </summary>
    Ambiguous = 1U << 4,

    /// <summary>
    /// The USB-C path appears to have an active Power Delivery contract.
    /// </summary>
    HasPdContract = 1U << 5,

    /// <summary>
    /// DisplayPort or alternate-mode related state appears active.
    /// </summary>
    DisplayAltMode = 1U << 6,

    /// <summary>
    /// The expansion-bay door-closed signal is asserted.
    /// </summary>
    DoorClosed = 1U << 7,

    /// <summary>
    /// The module or bay is enabled.
    /// </summary>
    Enabled = 1U << 8,

    /// <summary>
    /// All currently defined module flags are present.
    /// </summary>
    All = BuiltIn | Active | Connected | Fault | Ambiguous | HasPdContract | DisplayAltMode | DoorClosed | Enabled,
}
