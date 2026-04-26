using FrameworkDotnet.Enums;
using FrameworkDotnet.Interfaces;

using Native = Framework.System.Interop;

namespace FrameworkDotnet;

/// <summary>
/// Provides a safe entry point for interacting with Framework system information and EC services.
/// </summary>
public class FrameworkSystem
{
    /// <summary>
    /// Are we on FrameWork device?
    /// </summary>
    /// <remarks>Caches values automatically</remarks>
    public bool? IsFrameworkDevice
    {
        get
        {
            if (field.HasValue)
            {
                return field.Value;
            }

            //Costly operation
            try
            {
                var name = GetProductName(); //Attempt to get product name, if it fails we're likely not on a Framework device
                field = !string.IsNullOrEmpty(name);
            }
            catch
            {
                field = false;
            }

            return field;
        }
    }

    /// <summary>
    /// Checks if library is on drive next to the application, does not attempt to load the library or check for driver support. Useful for quick checks or to conditionally enable features that require the library without throwing exceptions on unsupported systems.
    /// </summary>
    public bool IsLibraryAvailable => Native.NativeMethods.IsLibraryAvailable();

    /// <summary>
    /// Determines whether the specified EC driver is supported on the current system.
    /// </summary>
    /// <param name="driver">The driver to evaluate.</param>
    /// <returns><see langword="true"/> when the driver is supported; otherwise, <see langword="false"/>.</returns>
    public bool IsDriverSupported(FrameworkEcDriver driver)
    {
        return Native.NativeMethods.framework_ec_driver_is_supported((Native.FrameworkEcDriver)(int)driver);
    }

    /// <summary>
    /// Gets the detected Framework platform.
    /// </summary>
    /// <returns>The detected platform.</returns>
    public FrameworkPlatform GetPlatform()
    {
        return (FrameworkPlatform)(int)Native.NativeMethods.GetPlatformOrThrow();
    }

    /// <summary>
    /// Gets the detected Framework platform family.
    /// </summary>
    /// <returns>The detected platform family.</returns>
    public FrameworkPlatformFamily GetPlatformFamily()
    {
        return (FrameworkPlatformFamily)(int)Native.NativeMethods.GetPlatformFamilyOrThrow();
    }

    /// <summary>
    /// Gets the product name reported by the native library.
    /// </summary>
    /// <returns>The product name.</returns>
    public string GetProductName()
    {
        return Native.NativeMethods.GetProductNameOrThrow();
    }

    /// <summary>
    /// Opens the default embedded controller connection.
    /// </summary>
    /// <returns>An open EC connection.</returns>
    public IFrameworkEcConnection OpenDefaultEc()
    {
        return FrameworkEcConnection.OpenDefault();
    }

    /// <summary>
    /// Opens an embedded controller connection using the specified driver.
    /// </summary>
    /// <param name="driver">The driver to use.</param>
    /// <returns>An open EC connection.</returns>
    public IFrameworkEcConnection OpenEcWithDriver(FrameworkEcDriver driver)
    {
        return FrameworkEcConnection.OpenWithDriver(driver);
    }
}
