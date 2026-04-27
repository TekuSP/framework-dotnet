using FrameworkDotnet.Enums;
using FrameworkDotnet.Exceptions;
using System;

namespace FrameworkDotnet.Interfaces;

/// <summary>
/// Defines Framework system discovery and embedded controller access operations.
/// </summary>
public interface IFrameworkSystem
{
    /// <summary>
    /// Gets a cached value indicating whether the current device appears to be a Framework device.
    /// </summary>
    /// <remarks>The value is discovered on first access and cached for later calls.</remarks>
    bool? IsFrameworkDevice
    {
        get;
    }

    /// <summary>
    /// Checks if library is on drive next to the application, does not attempt to load the library or check for driver support. Useful for quick checks or to conditionally enable features that require the library without throwing exceptions on unsupported systems.
    /// </summary>
    /// <exception cref="PlatformNotSupportedException">Thrown when the current operating system architecture is not supported.</exception>
    bool IsLibraryAvailable
    {
        get;
    }

    /// <summary>
    /// Gets the detected Framework platform.
    /// </summary>
    /// <returns>The detected platform.</returns>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library returns an error status.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkPlatform GetPlatform();

    /// <summary>
    /// Gets the detected Framework platform family.
    /// </summary>
    /// <returns>The detected platform family.</returns>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library returns an error status.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkPlatformFamily GetPlatformFamily();

    /// <summary>
    /// Gets the product name reported by the native library.
    /// </summary>
    /// <returns>The product name.</returns>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library returns an error status.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    string GetProductName();

    /// <summary>
    /// Determines whether the specified EC driver is supported on the current system.
    /// </summary>
    /// <param name="driver">The driver to evaluate.</param>
    /// <returns><see langword="true"/> when the driver is supported; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    bool IsDriverSupported(FrameworkEcDriver driver);

    /// <summary>
    /// Opens the default embedded controller connection.
    /// </summary>
    /// <returns>An open EC connection.</returns>
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the native library reports success but returns a null handle.</exception>
    IFrameworkEcConnection OpenDefaultEc();

    /// <summary>
    /// Opens an embedded controller connection using the specified driver.
    /// </summary>
    /// <param name="driver">The driver to use.</param>
    /// <returns>An open EC connection.</returns>
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the native library reports success but returns a null handle.</exception>
    IFrameworkEcConnection OpenEcWithDriver(FrameworkEcDriver driver);
}