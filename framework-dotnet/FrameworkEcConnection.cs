using System;

using FrameworkDotnet.Enums;
using FrameworkDotnet.Interfaces;
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

    /// <summary>
    /// Gets the active driver for the current connection.
    /// </summary>
    /// <returns>The active EC driver.</returns>
    public FrameworkEcDriver GetActiveDriver()
    {
        unsafe
        {
            Native.FrameworkEcDriver driver;
            Native.NativeMethods.framework_ec_get_active_driver(HandlePointer, &driver).ThrowIfError();
            return (FrameworkEcDriver)(int)driver;
        }
    }

    /// <summary>
    /// Gets firmware build information.
    /// </summary>
    /// <returns>The firmware build information string.</returns>
    public string GetBuildInfo()
    {
        unsafe
        {
            Native.FrameworkByteBuffer buffer = default;
            Native.NativeMethods.framework_ec_get_build_info(HandlePointer, &buffer).ThrowIfError();

            try
            {
                return buffer.ToUtf8String();
            }
            finally
            {
                buffer.Free();
            }
        }
    }

    /// <summary>
    /// Gets the current EC flash snapshot.
    /// </summary>
    /// <returns>The EC flash snapshot.</returns>
    public FrameworkEcFlashSnapshot GetFlashSnapshot()
    {
        unsafe
        {
            Native.FrameworkEcFlashVersions versions;
            Native.NativeMethods.framework_ec_get_flash_versions(HandlePointer, &versions).ThrowIfError();
            return new FrameworkEcFlashSnapshot((FrameworkEcCurrentImage)(int)versions.current_image, versions.GetRoVersion(), versions.GetRwVersion());
        }
    }

    /// <summary>
    /// Gets the current power snapshot.
    /// </summary>
    /// <returns>The current power snapshot.</returns>
    public FrameworkPowerSnapshot GetPowerSnapshot()
    {
        unsafe
        {
            Native.FrameworkPowerSnapshot snapshot;
            Native.NativeMethods.framework_ec_get_power_snapshot(HandlePointer, &snapshot).ThrowIfError();
            return new FrameworkPowerSnapshot(
                snapshot.ac_present != 0,
                snapshot.battery_present != 0,
                snapshot.discharging != 0,
                snapshot.charging != 0,
                snapshot.level_critical != 0,
                snapshot.battery_count,
                snapshot.current_battery_index,
                snapshot.present_voltage,
                snapshot.present_rate,
                snapshot.remaining_capacity,
                snapshot.design_capacity,
                snapshot.design_voltage,
                snapshot.last_full_charge_capacity,
                snapshot.cycle_count,
                snapshot.charge_percentage,
                snapshot.GetManufacturer(),
                snapshot.GetModelNumber(),
                snapshot.GetSerialNumber(),
                snapshot.GetBatteryType());
        }
    }

    /// <summary>
    /// Gets the current fan capabilities snapshot.
    /// </summary>
    /// <returns>The fan capabilities snapshot.</returns>
    public FrameworkFanCapabilitiesSnapshot GetFanCapabilitiesSnapshot()
    {
        unsafe
        {
            Native.FrameworkFanCapabilities capabilities;
            Native.NativeMethods.framework_ec_get_fan_capabilities(HandlePointer, &capabilities).ThrowIfError();
            return new FrameworkFanCapabilitiesSnapshot(
                capabilities.fan_count,
                capabilities.supports_fan_control != 0,
                capabilities.supports_thermal_reporting != 0);
        }
    }

    /// <summary>
    /// Gets the current thermal snapshot.
    /// </summary>
    /// <returns>The thermal snapshot.</returns>
    public FrameworkThermalSnapshot GetThermalSnapshot()
    {
        unsafe
        {
            Native.FrameworkThermalSnapshot snapshot;
            Native.NativeMethods.framework_ec_get_thermal_snapshot(HandlePointer, &snapshot).ThrowIfError();
            return new FrameworkThermalSnapshot(
                snapshot.fan_count,
                new FrameworkTemperatureReading((FrameworkTemperatureState)(int)snapshot.temperature_0.state, snapshot.temperature_0.celsius),
                new FrameworkTemperatureReading((FrameworkTemperatureState)(int)snapshot.temperature_1.state, snapshot.temperature_1.celsius),
                new FrameworkTemperatureReading((FrameworkTemperatureState)(int)snapshot.temperature_2.state, snapshot.temperature_2.celsius),
                new FrameworkTemperatureReading((FrameworkTemperatureState)(int)snapshot.temperature_3.state, snapshot.temperature_3.celsius),
                new FrameworkTemperatureReading((FrameworkTemperatureState)(int)snapshot.temperature_4.state, snapshot.temperature_4.celsius),
                new FrameworkTemperatureReading((FrameworkTemperatureState)(int)snapshot.temperature_5.state, snapshot.temperature_5.celsius),
                new FrameworkTemperatureReading((FrameworkTemperatureState)(int)snapshot.temperature_6.state, snapshot.temperature_6.celsius),
                new FrameworkTemperatureReading((FrameworkTemperatureState)(int)snapshot.temperature_7.state, snapshot.temperature_7.celsius),
                snapshot.fan_rpm_0,
                snapshot.fan_rpm_1,
                snapshot.fan_rpm_2,
                snapshot.fan_rpm_3,
                snapshot.fan_present_0 != 0,
                snapshot.fan_present_1 != 0,
                snapshot.fan_present_2 != 0,
                snapshot.fan_present_3 != 0,
                snapshot.fan_stalled_0 != 0,
                snapshot.fan_stalled_1 != 0,
                snapshot.fan_stalled_2 != 0,
                snapshot.fan_stalled_3 != 0);
        }
    }

    /// <summary>
    /// Sets the fan speed target in RPM.
    /// </summary>
    /// <param name="fanIndex">The zero-based fan index.</param>
    /// <param name="rpm">The target RPM.</param>
    public void SetFanRpm(int fanIndex, uint rpm)
    {
        unsafe
        {
            Native.NativeMethods.framework_ec_set_fan_rpm(HandlePointer, fanIndex, rpm).ThrowIfError();
        }
    }

    /// <summary>
    /// Sets the fan duty cycle.
    /// </summary>
    /// <param name="fanIndex">The zero-based fan index.</param>
    /// <param name="percent">The duty cycle percentage.</param>
    public void SetFanDuty(int fanIndex, uint percent)
    {
        unsafe
        {
            Native.NativeMethods.framework_ec_set_fan_duty(HandlePointer, fanIndex, percent).ThrowIfError();
        }
    }

    /// <summary>
    /// Restores automatic fan control for the specified fan.
    /// </summary>
    /// <param name="fanIndex">The zero-based fan index.</param>
    public void RestoreAutoFanControl(int fanIndex)
    {
        unsafe
        {
            Native.NativeMethods.framework_ec_restore_auto_fan_control(HandlePointer, fanIndex).ThrowIfError();
        }
    }

    internal static unsafe FrameworkEcConnection OpenDefault()
    {
        return Create((IntPtr)Native.NativeMethods.OpenDefaultOrThrow());
    }

    internal static unsafe FrameworkEcConnection OpenWithDriver(FrameworkEcDriver driver)
    {
        return Create((IntPtr)Native.NativeMethods.OpenWithDriverOrThrow((Native.FrameworkEcDriver)(int)driver));
    }

    protected override unsafe bool ReleaseHandle()
    {
        Native.NativeMethods.framework_ec_close((Native.FrameworkEcHandle*)handle);
        return true;
    }

    private static FrameworkEcConnection Create(IntPtr nativeHandle)
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
