using FrameworkDotnet.Attributes;

namespace FrameworkDotnet.Enums;

/// <summary>
/// Represents the input deck state mode for Framework 16.
/// </summary>
/// <remarks>Only meaningful on Framework 16. On other platforms the EC returns <c>EcResponse(InvalidCommand)</c>.</remarks>
[FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "Input deck mode control is specific to Framework Laptop 16.")]
public enum FrameworkDeckStateMode
{
    /// <summary>
    /// Input deck operates in read-only mode.
    /// </summary>
    ReadOnly = 0,

    /// <summary>
    /// Input deck is required and must be present to proceed.
    /// </summary>
    Required = 1,

    /// <summary>
    /// Force the input deck on.
    /// </summary>
    ForceOn = 2,

    /// <summary>
    /// Force the input deck off.
    /// </summary>
    ForceOff = 4,
}
