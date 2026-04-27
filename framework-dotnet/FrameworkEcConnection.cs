using System;

using FrameworkDotnet.Enums;
using FrameworkDotnet.Interfaces;
using FrameworkDotnet.Responses;
using FrameworkDotnet.Snapshots;

using Microsoft.Win32.SafeHandles;

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
    public FrameworkSetFanRpmResponse SetFanRpm(int fanIndex, uint rpm)
    {
        unsafe
        {
            var result = Native.NativeMethods.framework_ec_set_fan_rpm(HandlePointer, fanIndex, rpm);
            return result.GetValueOrThrow().ToManagedResponse();
        }
    }

    /// <inheritdoc/>
    public FrameworkSetFanDutyResponse SetFanDuty(int fanIndex, uint percent)
    {
        unsafe
        {
            var result = Native.NativeMethods.framework_ec_set_fan_duty(HandlePointer, fanIndex, percent);
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

    private unsafe Native.FrameworkEcHandle* HandlePointer
    {
        get
        {
            ObjectDisposedException.ThrowIf(IsClosed || IsInvalid, this);
            return (Native.FrameworkEcHandle*)handle;
        }
    }
}
