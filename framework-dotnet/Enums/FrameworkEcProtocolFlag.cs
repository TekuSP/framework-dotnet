namespace FrameworkDotnet.Enums;

/// <summary>
/// Represents the optional host command protocol capabilities reported by the embedded controller.
/// </summary>
[System.Flags]
public enum FrameworkEcProtocolFlag : uint
{
    /// <summary>
    /// The controller can report an in-progress result for long-running host commands.
    /// </summary>
    InProgressSupported = 0x01,
}
