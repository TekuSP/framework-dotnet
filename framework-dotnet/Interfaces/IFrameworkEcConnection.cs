using System;

using FrameworkDotnet.Enums;
using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Snapshots;

namespace FrameworkDotnet.Interfaces;

/// <summary>
/// Defines a safe embedded controller connection.
/// </summary>
public interface IFrameworkEcConnection : IDisposable
{
    /// <summary>
    /// Gets the active driver for the current connection.
    /// </summary>
    /// <returns>The active EC driver.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the connection has been disposed.</exception>
    /// <exception cref="FrameworkInteropException">Thrown when the native Framework library returns an error status.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkEcDriver GetActiveDriver();

    /// <summary>
    /// Gets firmware build information.
    /// </summary>
    /// <returns>The firmware build information string.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the connection has been disposed.</exception>
    /// <exception cref="FrameworkInteropException">Thrown when the native Framework library returns an error status.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    string GetBuildInfo();

    /// <summary>
    /// Gets the current EC flash snapshot.
    /// </summary>
    /// <returns>The EC flash snapshot.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the connection has been disposed.</exception>
    /// <exception cref="FrameworkInteropException">Thrown when the native Framework library returns an error status.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkEcFlashSnapshot GetFlashSnapshot();

    /// <summary>
    /// Gets the current power snapshot.
    /// </summary>
    /// <returns>The current power snapshot.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the connection has been disposed.</exception>
    /// <exception cref="FrameworkInteropException">Thrown when the native Framework library returns an error status.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkPowerSnapshot GetPowerSnapshot();

    /// <summary>
    /// Gets the current fan capabilities snapshot.
    /// </summary>
    /// <returns>The fan capabilities snapshot.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the connection has been disposed.</exception>
    /// <exception cref="FrameworkInteropException">Thrown when the native Framework library returns an error status.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkFanCapabilitiesSnapshot GetFanCapabilitiesSnapshot();

    /// <summary>
    /// Gets the current thermal snapshot.
    /// </summary>
    /// <returns>The thermal snapshot.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the connection has been disposed.</exception>
    /// <exception cref="FrameworkInteropException">Thrown when the native Framework library returns an error status.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkThermalSnapshot GetThermalSnapshot();

    /// <summary>
    /// Sets the fan speed target in RPM.
    /// </summary>
    /// <param name="fanIndex">The zero-based fan index.</param>
    /// <param name="rpm">The target RPM.</param>
    /// <exception cref="ObjectDisposedException">Thrown when the connection has been disposed.</exception>
    /// <exception cref="FrameworkInteropException">Thrown when the native Framework library returns an error status.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    void SetFanRpm(int fanIndex, uint rpm);

    /// <summary>
    /// Sets the fan duty cycle.
    /// </summary>
    /// <param name="fanIndex">The zero-based fan index.</param>
    /// <param name="percent">The duty cycle percentage.</param>
    /// <exception cref="ObjectDisposedException">Thrown when the connection has been disposed.</exception>
    /// <exception cref="FrameworkInteropException">Thrown when the native Framework library returns an error status.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    void SetFanDuty(int fanIndex, uint percent);

    /// <summary>
    /// Restores automatic fan control for the specified fan.
    /// </summary>
    /// <param name="fanIndex">The zero-based fan index.</param>
    /// <exception cref="ObjectDisposedException">Thrown when the connection has been disposed.</exception>
    /// <exception cref="FrameworkInteropException">Thrown when the native Framework library returns an error status.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    void RestoreAutoFanControl(int fanIndex);
}
