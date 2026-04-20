using FrameworkDotnet.Enums;
using FrameworkDotnet.Interfaces;

using Native = Framework.System.Interop;

namespace FrameworkDotnet;

/// <summary>
/// Provides a safe entry point for interacting with Framework system information and EC services.
/// </summary>
public static class FrameworkSystem
{
    /// <summary>
    /// Determines whether the specified EC driver is supported on the current system.
    /// </summary>
    /// <param name="driver">The driver to evaluate.</param>
    /// <returns><see langword="true"/> when the driver is supported; otherwise, <see langword="false"/>.</returns>
    public static bool IsDriverSupported(FrameworkEcDriver driver)
    {
        return Native.NativeMethods.framework_ec_driver_is_supported((Native.FrameworkEcDriver)(int)driver);
    }

    /// <summary>
    /// Gets the detected Framework platform.
    /// </summary>
    /// <returns>The detected platform.</returns>
    public static FrameworkPlatform GetPlatform()
    {
        return (FrameworkPlatform)(int)Native.NativeMethods.GetPlatformOrThrow();
    }

    /// <summary>
    /// Gets the detected Framework platform family.
    /// </summary>
    /// <returns>The detected platform family.</returns>
    public static FrameworkPlatformFamily GetPlatformFamily()
    {
        return (FrameworkPlatformFamily)(int)Native.NativeMethods.GetPlatformFamilyOrThrow();
    }

    /// <summary>
    /// Gets the product name reported by the native library.
    /// </summary>
    /// <returns>The product name.</returns>
    public static string GetProductName()
    {
        return Native.NativeMethods.GetProductNameOrThrow();
    }

    /// <summary>
    /// Opens the default embedded controller connection.
    /// </summary>
    /// <returns>An open EC connection.</returns>
    public static IFrameworkEcConnection OpenDefaultEc()
    {
        return FrameworkEcConnection.OpenDefault();
    }

    /// <summary>
    /// Opens an embedded controller connection using the specified driver.
    /// </summary>
    /// <param name="driver">The driver to use.</param>
    /// <returns>An open EC connection.</returns>
    public static IFrameworkEcConnection OpenEcWithDriver(FrameworkEcDriver driver)
    {
        return FrameworkEcConnection.OpenWithDriver(driver);
    }
}
