namespace FrameworkDotnet.Enums;

/// <summary>
/// Represents the application a USB Power Delivery controller firmware targets.
/// </summary>
public enum FrameworkPdApplication
{
    /// <summary>
    /// The firmware targets a notebook application.
    /// </summary>
    Notebook = 0,

    /// <summary>
    /// The firmware targets a monitor application.
    /// </summary>
    Monitor = 1,

    /// <summary>
    /// The firmware targets the AA application variant.
    /// </summary>
    AA = 2,

    /// <summary>
    /// The controller reported an application value that is not valid.
    /// </summary>
    Invalid = 3,
}
