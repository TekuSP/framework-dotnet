namespace FrameworkDotnet.Enums;

/// <summary>
/// Represents which firmware image a USB Power Delivery controller is currently running.
/// </summary>
public enum FrameworkPdFwMode
{
    /// <summary>
    /// The running firmware image could not be determined.
    /// </summary>
    Unknown = -1,

    /// <summary>
    /// The controller is running its boot loader.
    /// </summary>
    BootLoader = 0,

    /// <summary>
    /// The controller is running the backup firmware image.
    /// </summary>
    BackupFw = 1,

    /// <summary>
    /// The controller is running the main firmware image.
    /// </summary>
    MainFw = 2,
}
