using System;

using FrameworkDotnet.Attributes;
using FrameworkDotnet.Enums;
using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Responses;
using FrameworkDotnet.Snapshots;

using UnitsNet;

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
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the native layer reports an EC current image value that is not recognized by the managed API.</exception>
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
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the native layer reports a power source or battery state value that is not recognized by the managed API.</exception>
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
    /// <remarks>Because the native layer does not currently report a sensor count, the managed API infers <see cref="FrameworkThermalSnapshot.SensorCount"/> from the first contiguous range of present temperature readings.</remarks>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkThermalSnapshot GetThermalSnapshot();

    /// <summary>
    /// Gets the optional EC feature flags reported by the native library.
    /// </summary>
    /// <returns>The reported EC feature flags.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the connection has been disposed.</exception>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library returns an error status, including data-unavailable conditions on unsupported platforms.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkEcFeatureFlags GetFeatureFlags();

    /// <summary>
    /// Gets the current keyboard backlight snapshot.
    /// </summary>
    /// <returns>The current keyboard backlight snapshot.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the connection has been disposed.</exception>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library returns an error status, including data-unavailable conditions on unsupported platforms.</exception>
    /// <remarks>Upstream <c>framework-system</c> currently documents keyboard-backlight support on Framework Laptop 13 only. Other Framework platform families may return data-unavailable statuses or firmware-specific values depending on native support.</remarks>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework13, Message = "Upstream framework-system currently documents keyboard-backlight support on Framework Laptop 13 only.")]
    FrameworkKeyboardBacklightSnapshot GetKeyboardBacklightSnapshot();

    /// <summary>
    /// Gets the current fingerprint LED snapshot.
    /// </summary>
    /// <returns>The current fingerprint LED snapshot.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the connection has been disposed.</exception>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library returns an error status, including data-unavailable conditions on unsupported platforms.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkFingerprintLedSnapshot GetFingerprintLedSnapshot();

    /// <summary>
    /// Gets the current expansion bay snapshot.
    /// </summary>
    /// <returns>The current expansion bay snapshot.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the connection has been disposed.</exception>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library returns an error status, including data-unavailable conditions on unsupported platforms.</exception>
    /// <remarks>Upstream <c>framework-system</c> currently documents expansion-bay status support on Framework Laptop 16 only. Other Framework platform families may return data-unavailable statuses or firmware-specific values depending on native support.</remarks>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "Upstream framework-system currently documents expansion-bay status support on Framework Laptop 16 only.")]
    FrameworkExpansionBaySnapshot GetExpansionBaySnapshot();

    /// <summary>
    /// Gets the parsed GPU descriptor header snapshot.
    /// </summary>
    /// <returns>The parsed GPU descriptor header snapshot.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the connection has been disposed.</exception>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library returns an error status, including data-unavailable conditions on unsupported platforms.</exception>
    /// <exception cref="FrameworkGpuDescriptorLengthException">Thrown when the reported GPU descriptor lengths do not match the bytes read from the EC.</exception>
    /// <exception cref="FrameworkGpuDescriptorChecksumException">Thrown when the GPU descriptor header or payload CRC32 does not match the bytes read from the EC.</exception>
    /// <remarks>Upstream <c>framework-system</c> currently documents the expansion-bay GPU descriptor surface on Framework Laptop 16 only. Other Framework platform families may return data-unavailable statuses or firmware-specific values depending on native support.</remarks>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "Upstream framework-system currently documents the expansion-bay GPU descriptor surface on Framework Laptop 16 only.")]
    FrameworkGpuDescriptorHeaderSnapshot GetGpuDescriptorHeaderSnapshot();

    /// <summary>
    /// Reads the raw GPU descriptor bytes.
    /// </summary>
    /// <returns>The full raw GPU descriptor blob.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the connection has been disposed.</exception>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library returns an error status, including data-unavailable conditions on unsupported platforms.</exception>
    /// <remarks>Upstream <c>framework-system</c> currently documents the expansion-bay GPU descriptor surface on Framework Laptop 16 only. Other Framework platform families may return data-unavailable statuses or firmware-specific values depending on native support.</remarks>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "Upstream framework-system currently documents the expansion-bay GPU descriptor surface on Framework Laptop 16 only.")]
    byte[] ReadGpuDescriptor();

    /// <summary>
    /// Validates the raw GPU descriptor bytes against the live descriptor exposed by the EC.
    /// </summary>
    /// <param name="expectedDescriptor">The descriptor bytes to validate.</param>
    /// <returns><see langword="true"/> when the descriptor bytes match; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="expectedDescriptor"/> is <see langword="null"/>.</exception>
    /// <exception cref="ObjectDisposedException">Thrown when the connection has been disposed.</exception>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library returns an error status, including data-unavailable conditions on unsupported platforms.</exception>
    /// <remarks>Upstream <c>framework-system</c> currently documents the expansion-bay GPU descriptor surface on Framework Laptop 16 only. Other Framework platform families may return data-unavailable statuses or firmware-specific values depending on native support.</remarks>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "Upstream framework-system currently documents the expansion-bay GPU descriptor surface on Framework Laptop 16 only.")]
    bool ValidateGpuDescriptor(byte[] expectedDescriptor);

    /// <summary>
    /// Gets the current module inventory snapshot.
    /// </summary>
    /// <returns>The current module inventory snapshot.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the connection has been disposed.</exception>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library returns an error status, including data-unavailable conditions on unsupported platforms.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkModuleInventorySnapshot GetModuleInventorySnapshot();

    /// <summary>
    /// Sets the fan speed target in RPM.
    /// </summary>
    /// <param name="fanIndex">The zero-based fan index.</param>
    /// <param name="targetSpeed">The target fan speed.</param>
    /// <returns>The response returned for the fan speed change.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the connection has been disposed.</exception>
    /// <exception cref="FrameworkDotnet.Exceptions.InvalidArgument.FrameworkInvalidFanIndexException">Thrown when <paramref name="fanIndex"/> is not valid for the current device.</exception>
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkSetFanRpmResponse SetFanRpm(int fanIndex, RotationalSpeed targetSpeed);

    /// <summary>
    /// Sets the fan duty cycle.
    /// </summary>
    /// <param name="fanIndex">The zero-based fan index.</param>
    /// <param name="dutyCycle">The duty cycle.</param>
    /// <returns>The response returned for the fan duty change.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the connection has been disposed.</exception>
    /// <exception cref="FrameworkDotnet.Exceptions.InvalidArgument.FrameworkInvalidFanIndexException">Thrown when <paramref name="fanIndex"/> is not valid for the current device.</exception>
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkSetFanDutyResponse SetFanDuty(int fanIndex, Ratio dutyCycle);

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
