using System;

using FrameworkDotnet.Enums;
using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Responses;
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
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkEcDriver GetActiveDriver();

    /// <summary>
    /// Gets firmware build information.
    /// </summary>
    /// <returns>The firmware build information string.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the connection has been disposed.</exception>
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    string GetBuildInfo();

    /// <summary>
    /// Gets the current EC flash snapshot.
    /// </summary>
    /// <returns>The EC flash snapshot.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the connection has been disposed.</exception>
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkEcFlashSnapshot GetFlashSnapshot();

    /// <summary>
    /// Gets the current power snapshot.
    /// </summary>
    /// <returns>The current power snapshot.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the connection has been disposed.</exception>
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure.</exception>
    /// <exception cref="FrameworkBatteryStateException">Thrown when a battery reading within the reported battery count does not report a valid battery state.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkPowerSnapshot GetPowerSnapshot();

    /// <summary>
    /// Gets the current fan capabilities snapshot.
    /// </summary>
    /// <returns>The fan capabilities snapshot.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the connection has been disposed.</exception>
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the native layer reports a fan capabilities bit pattern that is not recognized by the managed API.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkFanCapabilitiesSnapshot GetFanCapabilitiesSnapshot();

    /// <summary>
    /// Gets the current thermal snapshot.
    /// </summary>
    /// <returns>The thermal snapshot.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the connection has been disposed.</exception>
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure.</exception>
    /// <exception cref="FrameworkTemperatureStateException">Thrown when a temperature reading within the reported sensor count does not report a successful state.</exception>
    /// <exception cref="FrameworkFanStateException">Thrown when a fan reading within the reported fan count does not report a successful state.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkThermalSnapshot GetThermalSnapshot();

    /// <summary>
    /// Sets the fan speed target in RPM.
    /// </summary>
    /// <param name="fanIndex">The zero-based fan index.</param>
    /// <param name="rpm">The target RPM.</param>
    /// <returns>The response returned for the fan speed change.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the connection has been disposed.</exception>
    /// <exception cref="FrameworkDotnet.Exceptions.InvalidArgument.FrameworkInvalidFanIndexException">Thrown when <paramref name="fanIndex"/> is not valid for the current device.</exception>
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkSetFanRpmResponse SetFanRpm(int fanIndex, uint rpm);

    /// <summary>
    /// Sets the fan duty cycle.
    /// </summary>
    /// <param name="fanIndex">The zero-based fan index.</param>
    /// <param name="percent">The duty cycle percentage.</param>
    /// <returns>The response returned for the fan duty change.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the connection has been disposed.</exception>
    /// <exception cref="FrameworkDotnet.Exceptions.InvalidArgument.FrameworkInvalidFanIndexException">Thrown when <paramref name="fanIndex"/> is not valid for the current device.</exception>
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkSetFanDutyResponse SetFanDuty(int fanIndex, uint percent);

    /// <summary>
    /// Restores automatic fan control for the specified fan.
    /// </summary>
    /// <param name="fanIndex">The zero-based fan index.</param>
    /// <returns>The response returned for the automatic fan control restore operation.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the connection has been disposed.</exception>
    /// <exception cref="FrameworkDotnet.Exceptions.InvalidArgument.FrameworkInvalidFanIndexException">Thrown when <paramref name="fanIndex"/> is not valid for the current device.</exception>
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkRestoreAutoFanControlResponse RestoreAutoFanControl(int fanIndex);
}
