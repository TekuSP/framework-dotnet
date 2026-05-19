using System;

using FrameworkDotnet.Attributes;
using FrameworkDotnet.Enums;
using FrameworkDotnet.Interfaces;
using FrameworkDotnet.Responses;
using FrameworkDotnet.Snapshots;

using Microsoft.Win32.SafeHandles;

using UnitsNet;

using Native = Framework.System.Interop;

namespace FrameworkDotnet;

/// <summary>
/// Represents a safe connection to the Framework embedded controller.
/// </summary>
public sealed class FrameworkEcConnection : SafeHandleZeroOrMinusOneIsInvalid, IFrameworkEcConnection
{
    private FrameworkEcConnection()
        : base(true)
    {
    }

    /// <inheritdoc/>
    public FrameworkEcDriver GetActiveDriver()
    {
        unsafe
        {
            return (FrameworkEcDriver)(int)Native.NativeMethods.framework_ec_get_active_driver(HandlePointer).GetValueOrThrow();
        }
    }

    /// <inheritdoc/>
    public string GetBuildInfo()
    {
        unsafe
        {
            return Native.NativeMethods.framework_ec_get_build_info(HandlePointer).GetValueOrThrow();
        }
    }

    /// <inheritdoc/>
    public FrameworkEcFlashSnapshot GetFlashSnapshot()
    {
        unsafe
        {
            return Native.NativeMethods.framework_ec_get_flash_versions(HandlePointer).GetValueOrThrow().ToManagedSnapshot();
        }
    }

    /// <inheritdoc/>
    public FrameworkPowerSnapshot GetPowerSnapshot()
    {
        unsafe
        {
            return Native.NativeMethods.framework_ec_get_power_snapshot(HandlePointer).GetValueOrThrow().ToManagedSnapshot();
        }
    }

    /// <inheritdoc/>
    public FrameworkFanCapabilitiesSnapshot GetFanCapabilitiesSnapshot()
    {
        unsafe
        {
            return Native.NativeMethods.framework_ec_get_fan_capabilities(HandlePointer).GetValueOrThrow().ToManagedSnapshot();
        }
    }

    /// <inheritdoc/>
    public FrameworkThermalSnapshot GetThermalSnapshot()
    {
        unsafe
        {
            return Native.NativeMethods.framework_ec_get_thermal_snapshot(HandlePointer).GetValueOrThrow().ToManagedSnapshot();
        }
    }

    /// <inheritdoc/>
    public FrameworkEcFeatureFlags GetFeatureFlags()
    {
        unsafe
        {
            return Native.NativeMethods.framework_ec_get_feature_flags(HandlePointer).GetValueOrThrow();
        }
    }

    /// <inheritdoc/>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework13, Message = "Upstream framework-system currently documents keyboard-backlight support on Framework Laptop 13 only.")]
    public FrameworkKeyboardBacklightSnapshot GetKeyboardBacklightSnapshot()
    {
        unsafe
        {
            return Native.NativeMethods.framework_ec_get_keyboard_backlight(HandlePointer).GetValueOrThrow();
        }
    }

    /// <inheritdoc/>
    public FrameworkFingerprintLedSnapshot GetFingerprintLedSnapshot()
    {
        unsafe
        {
            return Native.NativeMethods.framework_ec_get_fingerprint_led(HandlePointer).GetValueOrThrow();
        }
    }

    /// <inheritdoc/>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "Upstream framework-system currently documents expansion-bay status support on Framework Laptop 16 only.")]
    public FrameworkExpansionBaySnapshot GetExpansionBaySnapshot()
    {
        unsafe
        {
            return Native.NativeMethods.framework_ec_get_expansion_bay_status(HandlePointer).GetValueOrThrow();
        }
    }

    /// <inheritdoc/>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "Upstream framework-system currently documents the expansion-bay GPU descriptor surface on Framework Laptop 16 only.")]
    public FrameworkGpuDescriptorHeaderSnapshot GetGpuDescriptorHeaderSnapshot()
    {
        unsafe
        {
            var header = Native.NativeMethods.framework_ec_get_gpu_descriptor_header(HandlePointer).GetValueOrThrow();

            if (header.GetBayType() != FrameworkGpuDescriptorMagic.FrameworkExpansionBay)
            {
                return header.ToManagedSnapshot();
            }

            var descriptor = Native.NativeMethods.framework_ec_read_gpu_descriptor(HandlePointer).GetValueOrThrow();
            return header.ToManagedSnapshot(descriptor);
        }
    }

    /// <inheritdoc/>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "Upstream framework-system currently documents the expansion-bay GPU descriptor surface on Framework Laptop 16 only.")]
    public byte[] ReadGpuDescriptor()
    {
        unsafe
        {
            return Native.NativeMethods.framework_ec_read_gpu_descriptor(HandlePointer).GetValueOrThrow();
        }
    }

    /// <inheritdoc/>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "Upstream framework-system currently documents the expansion-bay GPU descriptor surface on Framework Laptop 16 only.")]
    public bool ValidateGpuDescriptor(byte[] expectedDescriptor)
    {
        ArgumentNullException.ThrowIfNull(expectedDescriptor);

        unsafe
        {
            fixed (byte* expectedDescriptorPointer = expectedDescriptor)
            {
                return Native.NativeMethods.framework_ec_validate_gpu_descriptor(HandlePointer, expectedDescriptorPointer, (uint)expectedDescriptor.Length).GetValueOrThrow();
            }
        }
    }

    /// <inheritdoc/>
    public FrameworkModuleInventorySnapshot GetModuleInventorySnapshot()
    {
        unsafe
        {
            return Native.NativeMethods.framework_ec_get_module_inventory(HandlePointer).GetValueOrThrow();
        }
    }

    /// <inheritdoc/>
    public FrameworkSetFanRpmResponse SetFanRpm(int fanIndex, RotationalSpeed targetSpeed)
    {
        unsafe
        {
            var result = Native.NativeMethods.framework_ec_set_fan_rpm(HandlePointer, fanIndex, ToUInt32(targetSpeed.RevolutionsPerMinute, nameof(targetSpeed)));
            return result.GetValueOrThrow().ToManagedResponse();
        }
    }

    /// <inheritdoc/>
    public FrameworkSetFanDutyResponse SetFanDuty(int fanIndex, Ratio dutyCycle)
    {
        unsafe
        {
            var result = Native.NativeMethods.framework_ec_set_fan_duty(HandlePointer, fanIndex, ToUInt32(dutyCycle.Percent, nameof(dutyCycle)));
            return result.GetValueOrThrow().ToManagedResponse();
        }
    }

    /// <inheritdoc/>
    public FrameworkRestoreAutoFanControlResponse RestoreAutoFanControl(int fanIndex)
    {
        unsafe
        {
            var result = Native.NativeMethods.framework_ec_restore_auto_fan_control(HandlePointer, fanIndex);
            return result.GetValueOrThrow().ToManagedResponse();
        }
    }

    internal static unsafe IFrameworkEcConnection OpenDefault()
    {
        return Create((IntPtr)Native.NativeMethods.OpenDefaultOrThrow());
    }

    internal static unsafe IFrameworkEcConnection OpenWithDriver(FrameworkEcDriver driver)
    {
        return Create((IntPtr)Native.NativeMethods.OpenWithDriverOrThrow((Native.FrameworkEcDriver)(int)driver));
    }

    /// <summary>
    /// Releases the native embedded controller handle associated with this instance.
    /// </summary>
    /// <returns><see langword="true"/> when the release path completes.</returns>
    protected override unsafe bool ReleaseHandle()
    {
        Native.NativeMethods.framework_ec_close((Native.FrameworkEcHandle*)handle);
        return true;
    }

    private static IFrameworkEcConnection Create(IntPtr nativeHandle)
    {
        if (nativeHandle == IntPtr.Zero)
        {
            throw new InvalidOperationException("The Framework EC returned a null handle.");
        }

        var connection = new FrameworkEcConnection();
        connection.SetHandle(nativeHandle);
        return connection;
    }

    private static uint ToUInt32(double value, string paramName)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(value, paramName);

        if (double.IsNaN(value) || double.IsInfinity(value) || value > uint.MaxValue)
        {
            throw new ArgumentOutOfRangeException(paramName, value, "The value must be a finite unsigned 32-bit whole number.");
        }

        if (value != Math.Truncate(value))
        {
            throw new ArgumentOutOfRangeException(paramName, value, "The value must be a whole number.");
        }

        return (uint)value;
    }

    private unsafe Native.FrameworkEcHandle* HandlePointer
    {
        get
        {
            ObjectDisposedException.ThrowIf(IsClosed || IsInvalid, this);
            return (Native.FrameworkEcHandle*)handle;
        }
    }
}
