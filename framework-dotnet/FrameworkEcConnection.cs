using System;
using System.Collections.Generic;

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
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "Upstream framework-system currently documents expansion-bay status support on Framework Laptop 16 only.")]
    public FrameworkExpansionBayModulesSnapshot GetExpansionBayModulesSnapshot()
    {
        FrameworkExpansionBaySnapshot bay = GetExpansionBaySnapshot();

        if (!bay.IsPresent)
        {
            return CreateExpansionBayModulesSnapshot(bay);
        }

        FrameworkModuleDescriptorSnapshot expansionBayModule = GetModuleInventorySnapshot().ExpansionBay;
        FrameworkExpansionBaySnapshot classifiedBay = ClassifyExpansionBaySnapshot(bay, expansionBayModule);

        return CreateExpansionBayModulesSnapshot(classifiedBay);
    }

    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "Upstream framework-system currently documents the expansion-bay GPU descriptor surface on Framework Laptop 16 only.")]
    private (IReadOnlyList<byte> RawMagicBytes, FrameworkGpuDescriptorMagic BayType, Version DescriptorVersion, Version HardwareVersion, string Serial, IReadOnlyList<byte> Header, IReadOnlyList<byte> Payload) GetGpuDescriptor()
    {
        unsafe
        {
            var header = Native.NativeMethods.framework_ec_get_gpu_descriptor_header(HandlePointer).GetValueOrThrow();

            if (header.GetBayType() != FrameworkGpuDescriptorMagic.FrameworkExpansionBay)
            {
                return header.ToManagedDescriptor();
            }

            var descriptor = Native.NativeMethods.framework_ec_read_gpu_descriptor(HandlePointer).GetValueOrThrow();
            return header.ToManagedDescriptor(descriptor);
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
    public FrameworkChassisIntrusionSnapshot GetChassisIntrusion()
    {
        unsafe
        {
            return Native.NativeMethods.framework_ec_get_chassis_intrusion(HandlePointer).GetValueOrThrow();
        }
    }

    /// <inheritdoc/>
    public FrameworkEcUptimeSnapshot GetUptime()
    {
        unsafe
        {
            return Native.NativeMethods.framework_ec_get_uptime(HandlePointer).GetValueOrThrow();
        }
    }

    /// <inheritdoc/>
    public FrameworkS0ixCounterSnapshot GetS0ixCounter()
    {
        unsafe
        {
            return Native.NativeMethods.framework_ec_get_s0ix_counter(HandlePointer).GetValueOrThrow();
        }
    }

    /// <inheritdoc/>
    public void ResetS0ixCounter()
    {
        unsafe
        {
            Native.NativeMethods.framework_ec_reset_s0ix_counter(HandlePointer).ThrowIfFailure();
        }
    }

    /// <inheritdoc/>
    public FrameworkPrivacySwitchesSnapshot GetPrivacySwitches()
    {
        unsafe
        {
            return Native.NativeMethods.framework_ec_get_privacy_switches(HandlePointer).GetValueOrThrow();
        }
    }

    /// <inheritdoc/>
    public FrameworkChargeLimitsSnapshot GetChargeLimits()
    {
        unsafe
        {
            return Native.NativeMethods.framework_ec_get_charge_limits(HandlePointer).GetValueOrThrow();
        }
    }

    /// <inheritdoc/>
    public void SetChargeLimits(Ratio minPercent, Ratio maxPercent)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(minPercent.Percent, 0, nameof(minPercent));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(minPercent.Percent, 100, nameof(minPercent));
        ArgumentOutOfRangeException.ThrowIfLessThan(maxPercent.Percent, 0, nameof(maxPercent));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(maxPercent.Percent, 100, nameof(maxPercent));
        if (minPercent > maxPercent)
            throw new ArgumentOutOfRangeException(nameof(minPercent), minPercent, "Minimum charge limit must not exceed the maximum.");

        unsafe
        {
            Native.NativeMethods.framework_ec_set_charge_limits(HandlePointer, (byte)minPercent.Percent, (byte)maxPercent.Percent).ThrowIfFailure();
        }
    }

    /// <inheritdoc/>
    public void SetChargeCurrentLimit(uint currentMa, int? batterySoc = null)
    {
        if (batterySoc.HasValue)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(batterySoc.Value, nameof(batterySoc));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(batterySoc.Value, 100, nameof(batterySoc));
        }

        unsafe
        {
            Native.NativeMethods.framework_ec_set_charge_current_limit(HandlePointer, currentMa, batterySoc ?? -1).ThrowIfFailure();
        }
    }

    /// <inheritdoc/>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework13, Message = "Upstream framework-system currently documents keyboard-backlight support on Framework Laptop 13 only.")]
    public void SetKeyboardBacklight(Ratio brightness)
    {
        double percent = brightness.Percent;
        ArgumentOutOfRangeException.ThrowIfNegative(percent, nameof(brightness));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(percent, 100.0, nameof(brightness));

        unsafe
        {
            Native.NativeMethods.framework_ec_set_keyboard_backlight(HandlePointer, (byte)Math.Round(percent)).ThrowIfFailure();
        }
    }

    /// <inheritdoc/>
    public void SetFingerprintLed(FrameworkFingerprintLedLevel level)
    {
        if (level == FrameworkFingerprintLedLevel.Unknown || level == FrameworkFingerprintLedLevel.Custom)
        {
            throw new ArgumentOutOfRangeException(nameof(level), level, "Unknown and Custom are get-only levels and cannot be set.");
        }

        unsafe
        {
            Native.NativeMethods.framework_ec_set_fingerprint_led(HandlePointer, (Native.FrameworkFingerprintLedLevel)(int)level).ThrowIfFailure();
        }
    }

    /// <inheritdoc/>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework13, Message = "Tablet mode override is supported on Framework 12 and 13. Framework 16 and Desktop return EcResponse(InvalidCommand).")]
    public void SetTabletMode(FrameworkTabletModeOverride mode)
    {
        unsafe
        {
            Native.NativeMethods.framework_ec_set_tablet_mode(HandlePointer, (Native.FrameworkTabletModeOverride)(int)mode).ThrowIfFailure();
        }
    }

    /// <inheritdoc/>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "Input deck mode control is specific to Framework Laptop 16.")]
    public void SetInputDeckMode(FrameworkDeckStateMode mode)
    {
        unsafe
        {
            Native.NativeMethods.framework_ec_set_input_deck_mode(HandlePointer, (Native.FrameworkDeckStateMode)(int)mode).ThrowIfFailure();
        }
    }

    private static FrameworkExpansionBayModulesSnapshot CreateExpansionBayModulesSnapshot(FrameworkExpansionBaySnapshot bay)
    {
        ArgumentNullException.ThrowIfNull(bay);

        return new FrameworkExpansionBayModulesSnapshot((byte)(bay.IsPresent ? 1 : 0), bay);
    }

    private FrameworkExpansionBaySnapshot ClassifyExpansionBaySnapshot(FrameworkExpansionBaySnapshot bay, FrameworkModuleDescriptorSnapshot expansionBayModule)
    {
        ArgumentNullException.ThrowIfNull(bay);
        ArgumentNullException.ThrowIfNull(expansionBayModule);

        if (!bay.IsPresent)
        {
            return bay;
        }

        return expansionBayModule.Identity switch
        {
            FrameworkModuleIdentity.ExpansionBayDualInterposer => new FrameworkDualInterposerExpansionBaySnapshot(bay),
            FrameworkModuleIdentity.ExpansionBaySingleInterposer => new FrameworkSingleInterposerExpansionBaySnapshot(bay),
            FrameworkModuleIdentity.ExpansionBayUmaFans => new FrameworkUmaFansExpansionBaySnapshot(bay),
            FrameworkModuleIdentity.ExpansionBaySsdHolder => new FrameworkSsdHolderExpansionBaySnapshot(bay),
            FrameworkModuleIdentity.ExpansionBayPcieAccessory => new FrameworkPcieAccessoryExpansionBaySnapshot(bay),
            FrameworkModuleIdentity.ExpansionBayAmdGpu => new FrameworkAmdGpuExpansionBaySnapshot(bay, GetGpuDescriptor()),
            FrameworkModuleIdentity.ExpansionBayNvidiaGpu => new FrameworkNvidiaGpuExpansionBaySnapshot(bay, GetGpuDescriptor()),
            FrameworkModuleIdentity.ExpansionBayFanOnly => new FrameworkFanOnlyExpansionBaySnapshot(bay),
            FrameworkModuleIdentity.ExpansionBay => new FrameworkGenericExpansionBaySnapshot(bay),
            _ => new FrameworkGenericExpansionBaySnapshot(bay),
        };
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
