using System;

using FrameworkDotnet.Enums;
using FrameworkDotnet.Interfaces;
using FrameworkDotnet.Snapshots;

using UnitsNet;

using Native = Framework.System.Interop;

namespace FrameworkDotnet.Ec;

/// <summary>
/// Implements the battery facet of an embedded controller connection.
/// </summary>
internal sealed class FrameworkEcBattery : IFrameworkEcBattery
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkEcBattery"/> class.
    /// </summary>
    /// <remarks>
    /// The facet never owns the embedded controller handle. It borrows it through <paramref name="handleAccessor"/>, which the owning <see cref="FrameworkEcConnection"/> supplies as a closure over its own validated handle, so the connection keeps sole responsibility for lifetime and for raising <see cref="ObjectDisposedException"/> once it has been disposed.
    /// </remarks>
    /// <param name="handleAccessor">A callback returning the current embedded controller handle. It is expected to throw <see cref="ObjectDisposedException"/> when the owning connection has been disposed.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="handleAccessor"/> is <see langword="null"/>.</exception>
    internal FrameworkEcBattery(Func<IntPtr> handleAccessor)
    {
        ArgumentNullException.ThrowIfNull(handleAccessor);

        this.handleAccessor = handleAccessor;
    }

    /// <inheritdoc/>
    public FrameworkSmartBatterySnapshot GetSmartBatterySnapshot(uint? unsealKey = null)
    {
        byte useUnsealKey = unsealKey.HasValue ? (byte)1 : (byte)0;

        unsafe
        {
            return Native.NativeMethods.framework_ec_get_smart_battery_data(HandlePointer, useUnsealKey, unsealKey ?? 0u).GetValueOrThrow();
        }
    }

    /// <inheritdoc/>
    public FrameworkBatteryCutoffState GetCutoffState()
    {
        unsafe
        {
            return (FrameworkBatteryCutoffState)(int)Native.NativeMethods.framework_ec_get_battery_cutoff_status(HandlePointer).GetValueOrThrow();
        }
    }

    /// <inheritdoc/>
    public FrameworkChargingStateSnapshot GetChargingState()
    {
        unsafe
        {
            return Native.NativeMethods.framework_ec_is_charging(HandlePointer).GetValueOrThrow();
        }
    }

    /// <inheritdoc/>
    public bool Authenticate(byte[] authenticationKey)
    {
        ArgumentNullException.ThrowIfNull(authenticationKey);

        if (authenticationKey.Length != AuthenticationKeyLength)
        {
            throw new ArgumentException($"The authentication key must be exactly {AuthenticationKeyLength} bytes long.", nameof(authenticationKey));
        }

        unsafe
        {
            fixed (byte* authenticationKeyPointer = authenticationKey)
            {
                return Native.NativeMethods.framework_ec_authenticate_battery(HandlePointer, authenticationKeyPointer).GetValueOrThrow();
            }
        }
    }

    /// <inheritdoc/>
    public void SetChargeRateLimit(ElectricCurrent rateLimit, Ratio? batterySoc = null)
    {
        double amperes = rateLimit.Amperes;
        ArgumentOutOfRangeException.ThrowIfNegative(amperes, nameof(rateLimit));

        if (batterySoc.HasValue)
        {
            double percent = batterySoc.Value.Percent;
            ArgumentOutOfRangeException.ThrowIfNegative(percent, nameof(batterySoc));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(percent, 100.0, nameof(batterySoc));
        }

        float batterySocPercent = batterySoc.HasValue ? (float)batterySoc.Value.Percent : UnconditionalBatterySocPercent;

        unsafe
        {
            Native.NativeMethods.framework_ec_set_charge_rate_limit(HandlePointer, (float)amperes, batterySocPercent).ThrowIfFailure();
        }
    }

    private unsafe Native.FrameworkEcHandle* HandlePointer => (Native.FrameworkEcHandle*)handleAccessor();

    /// <summary>
    /// The exact length, in bytes, the native authentication entry point reads from the supplied key pointer.
    /// </summary>
    private const int AuthenticationKeyLength = 16;

    /// <summary>
    /// The sentinel the native layer interprets as "apply the charge rate limit unconditionally".
    /// </summary>
    private const float UnconditionalBatterySocPercent = -1.0f;

    private readonly Func<IntPtr> handleAccessor;
}
