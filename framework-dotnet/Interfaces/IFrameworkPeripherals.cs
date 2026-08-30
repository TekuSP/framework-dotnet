using System;

using FrameworkDotnet.Attributes;
using FrameworkDotnet.Enums;
using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Exceptions.StatusCodes;
using FrameworkDotnet.Snapshots;

using UnitsNet;

namespace FrameworkDotnet.Interfaces;

/// <summary>
/// Defines the Framework peripheral operations that reach HID and USB devices directly.
/// </summary>
/// <remarks>
/// <para>
/// None of these operations involve the embedded controller. They enumerate and talk to the touchscreen,
/// the haptic touchpad, cameras, input modules, USB hubs, the audio expansion card and NVMe drives over
/// their own transports, so no <see cref="IFrameworkEcConnection"/> is needed and none of them can be
/// affected by an embedded controller driver being unavailable.
/// </para>
/// <para>
/// Every member issues synchronous device input/output and opens the underlying device for the duration
/// of the call. Keep all of them off the UI thread. <see cref="GetAudioCardVersion"/> is the slowest by a
/// wide margin and is called out separately on its own member.
/// </para>
/// <para>
/// Access to raw HID and USB devices is permission-gated by the host operating system. On Linux these
/// calls require a udev rule granting access to the device node, or elevated privileges; without it a
/// device that is physically present is reported as absent rather than raising a distinct error.
/// </para>
/// </remarks>
public interface IFrameworkPeripherals
{
    /// <summary>
    /// Reads the charge level of the stylus paired with the touchscreen.
    /// </summary>
    /// <returns>A snapshot describing the stylus charge level.</returns>
    /// <remarks>
    /// The query is answered by the touchscreen controller over HID. A successful read where
    /// <see cref="FrameworkStylusBatterySnapshot.IsPresent"/> is <see langword="false"/> means no stylus is
    /// paired, or the touchscreen does not report a stylus battery; that is a normal outcome and does not
    /// raise an exception.
    /// </remarks>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library reports a failure status.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkStylusBatterySnapshot GetStylusBattery();

    /// <summary>
    /// Enables or disables touch input on the touchscreen.
    /// </summary>
    /// <param name="enabled"><see langword="true"/> to enable touch input; <see langword="false"/> to disable it.</param>
    /// <remarks>
    /// <para>
    /// The setting is applied to the touchscreen controller over HID and persists until it is changed again
    /// or the controller is power-cycled. There is no matching read: the firmware does not report the
    /// current state, so the caller must track it if the state matters.
    /// </para>
    /// <para>
    /// Disabling touch input removes an input device from the running system. On a convertible or tablet
    /// with no other pointing device attached this can leave the machine without usable input until the
    /// setting is reversed or the system is power-cycled.
    /// </para>
    /// </remarks>
    /// <exception cref="FrameworkDataUnavailableStatusException">Thrown when no supported touchscreen answered the request.</exception>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library reports any other failure status.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    void SetTouchscreenEnabled(bool enabled);

    /// <summary>
    /// Sets the haptic feedback intensity of a haptic touchpad.
    /// </summary>
    /// <param name="intensity">The requested intensity, which must be exactly 0, 25, 50, 75 or 100 percent.</param>
    /// <remarks>
    /// <para>
    /// This is a write-only control and is deliberately not modelled as a settable property. The touchpad
    /// firmware accepts the HID SET_FEATURE report that carries the intensity but never answers the
    /// matching GET_FEATURE report, so the current value cannot be read back and no round trip exists.
    /// Track the last value written if the application needs to display it.
    /// </para>
    /// <para>
    /// The HID descriptor advertises a logical range of 0 to 100, but the Boreas haptic firmware implements
    /// only five discrete steps and rejects anything else. Requests are validated against those five steps
    /// before they reach the device.
    /// </para>
    /// <para>
    /// Only haptic touchpads answer this report. On a system fitted with a conventional touchpad the
    /// request fails rather than being silently ignored.
    /// </para>
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="intensity"/> is not exactly 0, 25, 50, 75 or 100 percent, or is not a finite value.</exception>
    /// <exception cref="FrameworkDataUnavailableStatusException">Thrown when no haptic touchpad answered the request.</exception>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library reports any other failure status.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    void SetTouchpadHapticIntensity(Ratio intensity);

    /// <summary>
    /// Sets the click force threshold of a haptic touchpad.
    /// </summary>
    /// <param name="force">The actuation force at which the touchpad registers a click.</param>
    /// <remarks>
    /// <para>
    /// This is a write-only control and is deliberately not modelled as a settable property, for the same
    /// reason as <see cref="SetTouchpadHapticIntensity(Ratio)"/>: the firmware accepts the HID SET_FEATURE
    /// report but never answers the matching GET_FEATURE report, so there is no way to read the threshold
    /// back. Track the last value written if the application needs to display it.
    /// </para>
    /// <para>
    /// Only haptic touchpads answer this report. On a system fitted with a conventional touchpad the
    /// request fails rather than being silently ignored.
    /// </para>
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="force"/> is not a defined <see cref="FrameworkClickForce"/> value.</exception>
    /// <exception cref="FrameworkDataUnavailableStatusException">Thrown when no haptic touchpad answered the request.</exception>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library reports any other failure status.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    void SetTouchpadClickForce(FrameworkClickForce force);

    /// <summary>
    /// Reads the firmware versions of the connected Framework cameras.
    /// </summary>
    /// <returns>A snapshot holding one slot per detected camera.</returns>
    /// <remarks>
    /// The version is decoded from the USB <c>bcdDevice</c> descriptor field, so the enumeration itself does
    /// not open the device. A system whose camera is disabled by the hardware privacy switch reports no
    /// camera at all, which is a normal reading rather than an error.
    /// </remarks>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library reports a failure status.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkPeripheralVersionsSnapshot GetCameraVersions();

    /// <summary>
    /// Reads the firmware versions of the connected Framework 16 input modules.
    /// </summary>
    /// <returns>A snapshot holding one slot per detected input module.</returns>
    /// <remarks>
    /// Input modules are a Framework Laptop 16 concept: the keyboard, numeric pad, macropad, spacers and
    /// the LED matrix modules all report through this call. Other platform families have no input modules
    /// to enumerate and answer successfully with an empty table.
    /// </remarks>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library reports a failure status.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "Input modules, including the LED matrix, are specific to Framework Laptop 16. Other platform families report an empty table.")]
    FrameworkPeripheralVersionsSnapshot GetInputModuleVersions();

    /// <summary>
    /// Reads the firmware versions of the USB hubs fitted to the system.
    /// </summary>
    /// <returns>A snapshot holding one slot per detected hub.</returns>
    /// <remarks>
    /// This covers the Realtek and Genesys hubs Framework systems use internally. Which hubs are present,
    /// and how many, varies by platform generation, so treat the result as discovery output rather than a
    /// fixed inventory.
    /// </remarks>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library reports a failure status.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkPeripheralVersionsSnapshot GetUsbHubVersions();

    /// <summary>
    /// Reads the firmware version of the audio expansion card.
    /// </summary>
    /// <returns>A snapshot holding a single populated slot when an audio card is fitted, or an empty table when none is.</returns>
    /// <remarks>
    /// <para>
    /// <b>This call must be kept off the UI thread.</b> Unlike the other version queries it cannot read a
    /// USB descriptor: it performs a Synaptics CAPE exchange over HID control transfers, which claims the
    /// card's HID interface for the duration of the call. A card in a wedged state does not answer, and the
    /// bounded retry loop then runs for up to roughly three seconds before giving up. The call blocks the
    /// calling thread for that whole period.
    /// </para>
    /// <para>
    /// While the interface is claimed, other software cannot talk to the card. Call this on demand, never
    /// on a timer, and never concurrently with itself.
    /// </para>
    /// <para>
    /// A system with no audio expansion card fitted answers successfully with an empty table, which is a
    /// normal reading rather than an error.
    /// </para>
    /// </remarks>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library reports a failure status.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkPeripheralVersionsSnapshot GetAudioCardVersion();

    /// <summary>
    /// Reads the model number and firmware revision of the NVMe drive at the given device node.
    /// </summary>
    /// <param name="devicePath">The path of the NVMe device node, for example <c>/dev/nvme0</c>.</param>
    /// <returns>A snapshot holding the drive's identity strings.</returns>
    /// <remarks>
    /// <para>
    /// <b>This operation is Linux only.</b> The readback issues an NVMe admin passthrough ioctl, which the
    /// underlying native library compiles only for Linux. On Windows, and on every other host operating
    /// system, the call always fails with <see cref="FrameworkNotSupportedStatusException"/> no matter what
    /// path is supplied. That is a permanent capability gap for the platform, not a transient failure, so
    /// there is no point retrying; gate the feature on the host operating system instead.
    /// </para>
    /// <para>
    /// The path is passed to the native layer as UTF-8 bytes plus a length rather than as a terminated
    /// string, so it may contain any character the file system accepts except an embedded null.
    /// </para>
    /// <para>
    /// Opening an NVMe device node and issuing an admin passthrough command normally requires elevated
    /// privileges.
    /// </para>
    /// </remarks>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="devicePath"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="devicePath"/> is empty or contains an embedded null character.</exception>
    /// <exception cref="FrameworkNotSupportedStatusException">Thrown on every host operating system other than Linux, where the native library contains no NVMe implementation.</exception>
    /// <exception cref="FrameworkDataUnavailableStatusException">Thrown when the drive could not be opened or did not answer the identify command.</exception>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library reports any other failure status.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkNvmeVersionSnapshot GetNvmeVersion(string devicePath);
}
