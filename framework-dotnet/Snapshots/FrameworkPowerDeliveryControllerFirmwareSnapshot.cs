using FrameworkDotnet.Enums;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents the three firmware images stored on a single USB Power Delivery controller.
/// </summary>
/// <remarks>
/// The controller occupies a fixed slot, so an unpopulated slot is still reported. When <see cref="IsPresent"/> is <see langword="false"/> the controller
/// is not fitted on this platform and <see cref="ActiveFirmware"/>, <see cref="BootLoader"/>, <see cref="BackupFirmware"/> and <see cref="MainFirmware"/> carry no meaningful data.
/// </remarks>
public sealed record FrameworkPowerDeliveryControllerFirmwareSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkPowerDeliveryControllerFirmwareSnapshot"/> class.
    /// </summary>
    /// <param name="slot">The fixed slot the controller occupies.</param>
    /// <param name="isPresent">A value indicating whether the slot is populated on this platform.</param>
    /// <param name="activeFirmware">The firmware image the controller is currently running.</param>
    /// <param name="bootLoader">The version of the boot loader image.</param>
    /// <param name="backupFirmware">The version of the backup firmware image.</param>
    /// <param name="mainFirmware">The version of the main firmware image.</param>
    public FrameworkPowerDeliveryControllerFirmwareSnapshot(FrameworkPowerDeliveryControllerSlot slot, bool isPresent, FrameworkPdFwMode activeFirmware, FrameworkPowerDeliveryControllerImageSnapshot bootLoader, FrameworkPowerDeliveryControllerImageSnapshot backupFirmware, FrameworkPowerDeliveryControllerImageSnapshot mainFirmware)
    {
        Slot = slot;
        IsPresent = isPresent;
        ActiveFirmware = activeFirmware;
        BootLoader = bootLoader;
        BackupFirmware = backupFirmware;
        MainFirmware = mainFirmware;
    }

    /// <summary>
    /// Gets the fixed slot the controller occupies.
    /// </summary>
    public FrameworkPowerDeliveryControllerSlot Slot { get; init; }

    /// <summary>
    /// Gets a value indicating whether the slot is populated on this platform.
    /// </summary>
    /// <remarks>This flag is authoritative. Read the firmware versions only when it is <see langword="true"/>.</remarks>
    public bool IsPresent { get; init; }

    /// <summary>
    /// Gets the firmware image the controller is currently running.
    /// </summary>
    public FrameworkPdFwMode ActiveFirmware { get; init; }

    /// <summary>
    /// Gets the version of the boot loader image.
    /// </summary>
    public FrameworkPowerDeliveryControllerImageSnapshot BootLoader { get; init; }

    /// <summary>
    /// Gets the version of the backup firmware image.
    /// </summary>
    public FrameworkPowerDeliveryControllerImageSnapshot BackupFirmware { get; init; }

    /// <summary>
    /// Gets the version of the main firmware image.
    /// </summary>
    public FrameworkPowerDeliveryControllerImageSnapshot MainFirmware { get; init; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return IsPresent
            ? $"Power Delivery Controller {Slot}: Active: {ActiveFirmware}, Boot Loader: [{BootLoader}], Backup: [{BackupFirmware}], Main: [{MainFirmware}]"
            : $"Power Delivery Controller {Slot}: Not Present";
    }
}
