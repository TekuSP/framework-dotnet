using System;

using FrameworkDotnet.Attributes;
using FrameworkDotnet.Enums;
using FrameworkDotnet.Interfaces;
using FrameworkDotnet.Snapshots;

using UnitsNet;

using Native = Framework.System.Interop;

namespace FrameworkDotnet.Ec;

/// <summary>
/// Provides the embedded controller power-management operations and the read-only expansion-bay GPU
/// identity for an owning <c>FrameworkEcConnection</c>.
/// </summary>
internal sealed class FrameworkEcPowerManagement : IFrameworkEcPowerManagement
{
    private readonly Func<IntPtr> handleAccessor;

    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkEcPowerManagement"/> class.
    /// </summary>
    /// <param name="handleAccessor">
    /// A callback that returns the native embedded controller handle of the owning connection. The
    /// owning connection is responsible for validating its own lifetime inside the callback and for
    /// throwing <see cref="ObjectDisposedException"/> once it has been disposed, so that every member
    /// of this facet observes the same disposal semantics as the connection itself.
    /// </param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="handleAccessor"/> is <see langword="null"/>.</exception>
    internal FrameworkEcPowerManagement(Func<IntPtr> handleAccessor)
    {
        ArgumentNullException.ThrowIfNull(handleAccessor);

        this.handleAccessor = handleAccessor;
    }

    /// <inheritdoc/>
    public Duration GetHibernateDelay()
    {
        unsafe
        {
            return Native.NativeMethods.framework_ec_get_hibernate_delay(HandlePointer).GetValueOrThrow();
        }
    }

    /// <inheritdoc/>
    public void SetHibernateDelay(Duration delay)
    {
        uint seconds = ToWholeSeconds(delay);

        unsafe
        {
            Native.NativeMethods.framework_ec_set_hibernate_delay(HandlePointer, seconds).ThrowIfFailure();
        }
    }

    /// <inheritdoc/>
    public FrameworkStandaloneModeSnapshot GetStandaloneMode()
    {
        unsafe
        {
            return Native.NativeMethods.framework_ec_get_standalone_mode(HandlePointer).GetValueOrThrow();
        }
    }

    /// <inheritdoc/>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "Upstream framework-system currently documents the expansion-bay GPU surface on Framework Laptop 16 only.")]
    public string GetGpuSerial()
    {
        unsafe
        {
            return Native.NativeMethods.framework_ec_get_gpu_serial(HandlePointer).GetValueOrThrow();
        }
    }

    private static uint ToWholeSeconds(Duration delay)
    {
        double seconds = delay.Seconds;

        if (double.IsNaN(seconds) || double.IsInfinity(seconds))
        {
            throw new ArgumentOutOfRangeException(nameof(delay), seconds, "The hibernate delay must be a finite duration.");
        }

        ArgumentOutOfRangeException.ThrowIfNegative(seconds, nameof(delay));

        double wholeSeconds = Math.Round(seconds, MidpointRounding.AwayFromZero);

        if (wholeSeconds > uint.MaxValue)
        {
            throw new ArgumentOutOfRangeException(nameof(delay), seconds, "The hibernate delay must not exceed 4294967295 seconds.");
        }

        return (uint)wholeSeconds;
    }

    private unsafe Native.FrameworkEcHandle* HandlePointer => (Native.FrameworkEcHandle*)handleAccessor();
}
