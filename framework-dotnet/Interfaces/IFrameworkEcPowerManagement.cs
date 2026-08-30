using System;

using FrameworkDotnet.Attributes;
using FrameworkDotnet.Enums;
using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Snapshots;

using UnitsNet;

namespace FrameworkDotnet.Interfaces;

/// <summary>
/// Defines the embedded controller power-management surface, together with the read-only
/// expansion-bay GPU identity it exposes.
/// </summary>
/// <remarks>
/// Every member issues a host command against the embedded controller of the owning connection, so
/// the lifetime of an implementation is bound to that connection. Once the connection is disposed,
/// every member throws <see cref="ObjectDisposedException"/>.
/// </remarks>
public interface IFrameworkEcPowerManagement
{
    /// <summary>
    /// Gets the delay the EC waits with the system off before it hibernates.
    /// </summary>
    /// <returns>The configured hibernate delay.</returns>
    /// <remarks>The EC stores this delay as a whole number of seconds, so the returned duration is always an integral number of seconds.</remarks>
    /// <exception cref="ObjectDisposedException">Thrown when the connection has been disposed.</exception>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library returns an error status.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    Duration GetHibernateDelay();

    /// <summary>
    /// Sets the delay the EC waits with the system off before it hibernates.
    /// </summary>
    /// <param name="delay">The hibernate delay to program. Must be a finite, non-negative duration of at most 4294967295 seconds.</param>
    /// <remarks>The EC stores the delay as a whole number of seconds, so <paramref name="delay"/> is rounded to the nearest second before it is written.</remarks>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="delay"/> is negative, not finite, or rounds to more than 4294967295 seconds.</exception>
    /// <exception cref="ObjectDisposedException">Thrown when the connection has been disposed.</exception>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library returns an error status.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    void SetHibernateDelay(Duration delay);

    /// <summary>
    /// Gets the standalone (batteryless) mode state reported by the EC.
    /// </summary>
    /// <returns>The standalone mode snapshot.</returns>
    /// <remarks>
    /// Standalone mode describes a system that runs without a battery pack, the normal configuration
    /// for Framework Desktop. The call is valid on every platform family, but a <see langword="true"/>
    /// reading is not proof that no battery is fitted: upstream falls back to <see langword="true"/>
    /// as a safe default whenever the embedded controller power-info read produces nothing, and
    /// reports success while doing so. See <see cref="FrameworkStandaloneModeSnapshot"/>.
    /// </remarks>
    /// <exception cref="ObjectDisposedException">Thrown when the connection has been disposed.</exception>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library returns an error status.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkStandaloneModeSnapshot GetStandaloneMode();

    /// <summary>
    /// Reads the serial number of the expansion-bay GPU module.
    /// </summary>
    /// <returns>The expansion-bay GPU serial, or an empty string when the EC reports no serial.</returns>
    /// <remarks>
    /// <para>
    /// This surface is deliberately read-only. The matching write path is <b>not</b> exposed by this
    /// library and no setter will be added: programming a serial rewrites persistent expansion-bay
    /// identity, and the upstream <c>framework-system</c> implementation copies the supplied bytes
    /// into a fixed-size slice without a length check.
    /// </para>
    /// <para>
    /// Upstream <c>framework-system</c> currently documents the expansion-bay GPU surface on
    /// Framework Laptop 16 only. Other Framework platform families may return data-unavailable
    /// statuses or firmware-specific values depending on native support.
    /// </para>
    /// </remarks>
    /// <exception cref="ObjectDisposedException">Thrown when the connection has been disposed.</exception>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library returns an error status, including data-unavailable conditions on unsupported platforms.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "Upstream framework-system currently documents the expansion-bay GPU surface on Framework Laptop 16 only.")]
    string GetGpuSerial();
}
