using System;

using FrameworkDotnet.Enums;
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
    FrameworkEcDriver GetActiveDriver();

    /// <summary>
    /// Gets firmware build information.
    /// </summary>
    /// <returns>The firmware build information string.</returns>
    string GetBuildInfo();

    /// <summary>
    /// Gets the current EC flash snapshot.
    /// </summary>
    /// <returns>The EC flash snapshot.</returns>
    FrameworkEcFlashSnapshot GetFlashSnapshot();

    /// <summary>
    /// Gets the current power snapshot.
    /// </summary>
    /// <returns>The current power snapshot.</returns>
    FrameworkPowerSnapshot GetPowerSnapshot();

    /// <summary>
    /// Gets the current fan capabilities snapshot.
    /// </summary>
    /// <returns>The fan capabilities snapshot.</returns>
    FrameworkFanCapabilitiesSnapshot GetFanCapabilitiesSnapshot();

    /// <summary>
    /// Gets the current thermal snapshot.
    /// </summary>
    /// <returns>The thermal snapshot.</returns>
    FrameworkThermalSnapshot GetThermalSnapshot();

    /// <summary>
    /// Sets the fan speed target in RPM.
    /// </summary>
    /// <param name="fanIndex">The zero-based fan index.</param>
    /// <param name="rpm">The target RPM.</param>
    void SetFanRpm(int fanIndex, uint rpm);

    /// <summary>
    /// Sets the fan duty cycle.
    /// </summary>
    /// <param name="fanIndex">The zero-based fan index.</param>
    /// <param name="percent">The duty cycle percentage.</param>
    void SetFanDuty(int fanIndex, uint percent);

    /// <summary>
    /// Restores automatic fan control for the specified fan.
    /// </summary>
    /// <param name="fanIndex">The zero-based fan index.</param>
    void RestoreAutoFanControl(int fanIndex);
}
