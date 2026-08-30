using System;

using FrameworkDotnet.Interfaces;
using FrameworkDotnet.Snapshots;

using Native = Framework.System.Interop;

namespace FrameworkDotnet.Ec;

/// <summary>
/// Implements the embedded controller diagnostic facet on top of a borrowed native EC handle.
/// </summary>
/// <remarks>
/// <para>
/// The facet does not own the handle. It borrows it from the connection that created it through
/// a caller-supplied accessor, which is expected to perform the connection's own disposed check
/// and hand back the current native handle. That keeps the facet decoupled from the concrete
/// connection type while preserving the connection's lifetime guarantees: once the connection is
/// disposed every call through this facet throws
/// <see cref="ObjectDisposedException"/> from inside the accessor.
/// </para>
/// <para>
/// Because nothing here is owned, the facet is deliberately not disposable and is safe to hold
/// for the lifetime of the owning connection.
/// </para>
/// </remarks>
internal sealed class FrameworkEcDiagnostics : IFrameworkEcDiagnostics
{
    private readonly Func<IntPtr> handleAccessor;

    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkEcDiagnostics"/> class.
    /// </summary>
    /// <param name="handleAccessor">A callback returning the native EC handle of the owning connection. It is invoked on every call and is expected to throw when the owning connection has been disposed.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="handleAccessor"/> is <see langword="null"/>.</exception>
    internal FrameworkEcDiagnostics(Func<IntPtr> handleAccessor)
    {
        ArgumentNullException.ThrowIfNull(handleAccessor);

        this.handleAccessor = handleAccessor;
    }

    /// <inheritdoc/>
    public FrameworkEcHelloSnapshot SendHello(uint inData)
    {
        unsafe
        {
            return Native.NativeMethods.framework_ec_hello(HandlePointer, inData).GetValueOrThrow();
        }
    }

    /// <inheritdoc/>
    public FrameworkEcHelloSnapshot CheckHello()
    {
        unsafe
        {
            return Native.NativeMethods.framework_ec_check_hello(HandlePointer).GetValueOrThrow();
        }
    }

    /// <inheritdoc/>
    public FrameworkEcProtocolInfoSnapshot GetProtocolInfo()
    {
        unsafe
        {
            return Native.NativeMethods.framework_ec_get_protocol_info(HandlePointer).GetValueOrThrow();
        }
    }

    /// <inheritdoc/>
    public FrameworkEcSystemInfoSnapshot GetSystemInfo()
    {
        unsafe
        {
            return Native.NativeMethods.framework_ec_get_sysinfo(HandlePointer).GetValueOrThrow();
        }
    }

    /// <inheritdoc/>
    public FrameworkEcPanicInfoSnapshot GetPanicInfo()
    {
        unsafe
        {
            return Native.NativeMethods.framework_ec_get_panic_info(HandlePointer).GetValueOrThrow();
        }
    }

    /// <inheritdoc/>
    public FrameworkEcPort80HistorySnapshot GetPort80History()
    {
        unsafe
        {
            return Native.NativeMethods.framework_ec_get_port80_history(HandlePointer).GetValueOrThrow();
        }
    }

    /// <inheritdoc/>
    public FrameworkEcSwitchesSnapshot GetSwitches()
    {
        unsafe
        {
            return Native.NativeMethods.framework_ec_get_switches(HandlePointer).GetValueOrThrow();
        }
    }

    /// <inheritdoc/>
    public FrameworkEcApThrottleSnapshot GetApThrottleStatus()
    {
        unsafe
        {
            return Native.NativeMethods.framework_ec_get_ap_throttle_status(HandlePointer).GetValueOrThrow();
        }
    }

    /// <inheritdoc/>
    public int ReadAdcChannel(byte channel)
    {
        unsafe
        {
            return Native.NativeMethods.framework_ec_adc_read(HandlePointer, channel).GetValueOrThrow();
        }
    }

    /// <inheritdoc/>
    public bool IsCommandVersionSupported(uint command, byte version)
    {
        unsafe
        {
            return Native.NativeMethods.framework_ec_command_version_supported(HandlePointer, command, version).GetValueOrThrow();
        }
    }

    private unsafe Native.FrameworkEcHandle* HandlePointer => (Native.FrameworkEcHandle*)handleAccessor();
}
