using System;

using FrameworkDotnet.Attributes;
using FrameworkDotnet.Enums;
using FrameworkDotnet.Interfaces;
using FrameworkDotnet.Snapshots;

using Native = Framework.System.Interop;

namespace FrameworkDotnet.Ec;

/// <summary>
/// Implements the USB Power Delivery facet of an embedded controller connection.
/// </summary>
/// <remarks>
/// The facet does not own the native embedded controller handle. It borrows the handle through an accessor supplied by the owning connection, so the
/// connection remains the single owner and the single place where handle lifetime is enforced.
/// </remarks>
internal sealed class FrameworkEcPowerDelivery : IFrameworkEcPowerDelivery
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkEcPowerDelivery"/> class.
    /// </summary>
    /// <param name="handleAccessor">
    /// A delegate returning the native embedded controller handle owned by the connection. The delegate is invoked once per call and is expected to throw
    /// <see cref="ObjectDisposedException"/> when the owning connection has been closed or disposed.
    /// </param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="handleAccessor"/> is <see langword="null"/>.</exception>
    internal FrameworkEcPowerDelivery(Func<IntPtr> handleAccessor)
    {
        ArgumentNullException.ThrowIfNull(handleAccessor);

        this.handleAccessor = handleAccessor;
    }

    /// <inheritdoc/>
    public FrameworkPowerDeliveryControllerVersionsSnapshot GetControllerVersions()
    {
        unsafe
        {
            return Native.NativeMethods.framework_ec_get_pd_controller_versions(HandlePointer).GetValueOrThrow();
        }
    }

    /// <inheritdoc/>
    public FrameworkPowerDeliveryPowerInfoSnapshot GetPowerInfo(int port)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(port);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(port, byte.MaxValue);

        unsafe
        {
            return Native.NativeMethods.framework_ec_get_pd_power_info(HandlePointer, (byte)port).GetValueOrThrow();
        }
    }

    /// <inheritdoc/>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "The Parade retimer sits behind the Framework Laptop 16 expansion-bay discrete GPU; other platform families reject the underlying EC command.")]
    public FrameworkPowerDeliveryRetimerVersionSnapshot GetRetimerVersion()
    {
        unsafe
        {
            return Native.NativeMethods.framework_ec_get_retimer_version(HandlePointer).GetValueOrThrow();
        }
    }

    private readonly Func<IntPtr> handleAccessor;

    private unsafe Native.FrameworkEcHandle* HandlePointer => (Native.FrameworkEcHandle*)handleAccessor();
}
