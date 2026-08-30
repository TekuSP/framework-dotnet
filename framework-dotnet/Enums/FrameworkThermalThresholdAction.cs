namespace FrameworkDotnet.Enums;

/// <summary>
/// Identifies what a thermal threshold write is asked to do to one individual threshold.
/// </summary>
/// <remarks>
/// <para>
/// Writing thermal thresholds is a read-modify-write against embedded controller firmware, and the
/// native ABI encodes the caller's intent in the sign of each argument: a negative value keeps the
/// threshold exactly as firmware currently holds it, zero disables the threshold, and a positive
/// value is a temperature in degrees Celsius. This enumeration names those three intents so that
/// "keep the current value" and "disable the threshold" can never be confused with one another.
/// </para>
/// <para>
/// Note that the read path uses a different convention: a disabled threshold reads back as -273
/// degrees Celsius, so a reader must consult the enabled mask instead of the reported temperature.
/// </para>
/// </remarks>
public enum FrameworkThermalThresholdAction
{
    /// <summary>
    /// Leaves the threshold exactly as embedded controller firmware currently holds it, whether it
    /// is enabled or disabled. This is the default so that an unspecified threshold is never
    /// changed by accident.
    /// </summary>
    KeepCurrent = 0,

    /// <summary>
    /// Disables the threshold, so that the embedded controller stops acting on it entirely. A
    /// subsequent read reports the threshold's enabled bit as clear.
    /// </summary>
    Disable = 1,

    /// <summary>
    /// Enables the threshold and sets it to an explicit temperature.
    /// </summary>
    Set = 2,
}
