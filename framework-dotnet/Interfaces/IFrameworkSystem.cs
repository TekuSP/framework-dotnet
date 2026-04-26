using FrameworkDotnet.Enums;

namespace FrameworkDotnet.Interfaces;

public interface IFrameworkSystem
{
    /// <summary>
    /// Are we on FrameWork device?
    /// </summary>
    /// <remarks>Caches values automatically, never should be null</remarks>
    bool? IsFrameworkDevice
    {
        get;
    }

    /// <summary>
    /// Checks if library is on drive next to the application, does not attempt to load the library or check for driver support. Useful for quick checks or to conditionally enable features that require the library without throwing exceptions on unsupported systems.
    /// </summary>
    bool IsLibraryAvailable
    {
        get;
    }

    /// <summary>
    /// Determines whether the specified EC driver is supported on the current system.
    /// </summary>
    /// <param name="driver">The driver to evaluate.</param>
    /// <returns><see langword="true"/> when the driver is supported; otherwise, <see langword="false"/>.</returns>
    FrameworkPlatform GetPlatform();

    /// <summary>
    /// Gets the detected Framework platform.
    /// </summary>
    /// <returns>The detected platform.</returns>
    FrameworkPlatformFamily GetPlatformFamily();

    /// <summary>
    /// Gets the detected Framework platform family.
    /// </summary>
    /// <returns>The detected platform family.</returns>
    string GetProductName();

    /// <summary>
    /// Gets the product name reported by the native library.
    /// </summary>
    /// <returns>The product name.</returns>
    bool IsDriverSupported(FrameworkEcDriver driver);

    /// <summary>
    /// Opens the default embedded controller connection.
    /// </summary>
    /// <returns>An open EC connection.</returns>
    IFrameworkEcConnection OpenDefaultEc();

    /// <summary>
    /// Opens an embedded controller connection using the specified driver.
    /// </summary>
    /// <param name="driver">The driver to use.</param>
    /// <returns>An open EC connection.</returns>
    IFrameworkEcConnection OpenEcWithDriver(FrameworkEcDriver driver);
}