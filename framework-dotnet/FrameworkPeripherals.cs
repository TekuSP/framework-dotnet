using System;
using System.Text;

using FrameworkDotnet.Attributes;
using FrameworkDotnet.Enums;
using FrameworkDotnet.Interfaces;
using FrameworkDotnet.Snapshots;

using UnitsNet;

using Native = Framework.System.Interop;

namespace FrameworkDotnet;

/// <summary>
/// Provides a safe entry point for the Framework peripherals that are reached over HID and USB directly.
/// </summary>
/// <remarks>
/// <para>
/// The class holds no state and owns no native resource, so instances are cheap to create and every member
/// is safe to call from any thread. The underlying devices are not: two threads issuing device input/output
/// to the same peripheral at once contend for the same interface, so serialise calls that target one device
/// and never call <see cref="GetAudioCardVersion"/> concurrently with itself.
/// </para>
/// <para>
/// No member touches the embedded controller, so none of them needs an <see cref="IFrameworkEcConnection"/>
/// and none of them is affected by embedded controller driver availability.
/// </para>
/// </remarks>
public class FrameworkPeripherals : IFrameworkPeripherals
{
    /// <inheritdoc/>
    public FrameworkStylusBatterySnapshot GetStylusBattery()
    {
        unsafe
        {
            return Native.NativeMethods.framework_get_stylus_battery().GetValueOrThrow();
        }
    }

    /// <inheritdoc/>
    public void SetTouchscreenEnabled(bool enabled)
    {
        unsafe
        {
            Native.NativeMethods.framework_touchscreen_enable(enabled).ThrowIfFailure();
        }
    }

    /// <inheritdoc/>
    public void SetTouchpadHapticIntensity(Ratio intensity)
    {
        byte level = ToHapticIntensityLevel(intensity);

        unsafe
        {
            Native.NativeMethods.framework_touchpad_set_haptic_intensity(level).ThrowIfFailure();
        }
    }

    /// <inheritdoc/>
    public void SetTouchpadClickForce(FrameworkClickForce force)
    {
        if (!Enum.IsDefined(force))
        {
            throw new ArgumentOutOfRangeException(nameof(force), force, "The click force must be one of the defined threshold levels; the touchpad firmware accepts no other value.");
        }

        unsafe
        {
            Native.NativeMethods.framework_touchpad_set_click_force((Native.FrameworkClickForce)(int)force).ThrowIfFailure();
        }
    }

    /// <inheritdoc/>
    public FrameworkPeripheralVersionsSnapshot GetCameraVersions()
    {
        unsafe
        {
            return Native.NativeMethods.framework_get_camera_versions().GetValueOrThrow();
        }
    }

    /// <inheritdoc/>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "Input modules, including the LED matrix, are specific to Framework Laptop 16. Other platform families report an empty table.")]
    public FrameworkPeripheralVersionsSnapshot GetInputModuleVersions()
    {
        unsafe
        {
            return Native.NativeMethods.framework_get_input_module_versions().GetValueOrThrow();
        }
    }

    /// <inheritdoc/>
    public FrameworkPeripheralVersionsSnapshot GetUsbHubVersions()
    {
        unsafe
        {
            return Native.NativeMethods.framework_get_usb_hub_versions().GetValueOrThrow();
        }
    }

    /// <inheritdoc/>
    public FrameworkPeripheralVersionsSnapshot GetAudioCardVersion()
    {
        unsafe
        {
            return Native.NativeMethods.framework_get_audio_card_version().GetValueOrThrow();
        }
    }

    /// <inheritdoc/>
    public FrameworkNvmeVersionSnapshot GetNvmeVersion(string devicePath)
    {
        ArgumentException.ThrowIfNullOrEmpty(devicePath);

        if (devicePath.Contains('\0', StringComparison.Ordinal))
        {
            throw new ArgumentException("The NVMe device path must not contain an embedded null character.", nameof(devicePath));
        }

        byte[] pathBytes = Encoding.UTF8.GetBytes(devicePath);

        unsafe
        {
            fixed (byte* pathPointer = pathBytes)
            {
                return Native.NativeMethods.framework_get_nvme_version(pathPointer, pathBytes.Length).GetValueOrThrow();
            }
        }
    }

    /// <summary>
    /// Converts a requested haptic intensity into the byte level the touchpad firmware accepts.
    /// </summary>
    /// <param name="intensity">The requested intensity.</param>
    /// <returns>The whole percentage to send to the firmware.</returns>
    /// <remarks>
    /// The HID descriptor advertises a logical range of 0 to 100, but the haptic firmware implements only
    /// the five steps in <see cref="HapticIntensityLevelsPercent"/> and rejects anything else. Validating
    /// here turns a firmware rejection, which is indistinguishable from a missing touchpad at the native
    /// boundary, into an argument error that names the problem.
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="intensity"/> is not finite, or is not one of the five supported steps.</exception>
    private static byte ToHapticIntensityLevel(Ratio intensity)
    {
        double percent = intensity.Percent;

        if (double.IsNaN(percent) || double.IsInfinity(percent))
        {
            throw new ArgumentOutOfRangeException(nameof(intensity), percent, "The touchpad haptic intensity must be a finite percentage.");
        }

        double rounded = Math.Round(percent);

        if (Math.Abs(percent - rounded) <= HapticIntensityTolerancePercent && rounded >= 0.0 && rounded <= 100.0)
        {
            byte level = (byte)rounded;

            if (Array.IndexOf(HapticIntensityLevelsPercent, level) >= 0)
            {
                return level;
            }
        }

        throw new ArgumentOutOfRangeException(nameof(intensity), percent, "The touchpad haptic intensity must be 0, 25, 50, 75 or 100 percent. The haptic firmware implements only those five steps and rejects every other value.");
    }

    /// <summary>
    /// The five haptic intensity steps, in whole percent, that the touchpad firmware implements.
    /// </summary>
    private static readonly byte[] HapticIntensityLevelsPercent = [0, 25, 50, 75, 100];

    /// <summary>
    /// The tolerance, in percent, applied when matching a requested intensity onto a supported step.
    /// </summary>
    /// <remarks>
    /// A <see cref="Ratio"/> built from a decimal fraction rather than a percentage can land a fraction of a
    /// percent away from the intended whole number. The tolerance absorbs that rounding without accepting a
    /// value that was genuinely meant to be a different step.
    /// </remarks>
    private const double HapticIntensityTolerancePercent = 1e-6;
}
