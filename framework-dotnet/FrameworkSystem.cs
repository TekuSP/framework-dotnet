using FrameworkDotnet.Enums;
using FrameworkDotnet.Interfaces;

using Native = Framework.System.Interop;

namespace FrameworkDotnet;

/// <summary>
/// Provides a safe entry point for interacting with Framework system information and EC services.
/// </summary>
public class FrameworkSystem : IFrameworkSystem
{
    /// <inheritdoc/>
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

    /// <inheritdoc/>
    public bool IsLibraryAvailable => Native.NativeMethods.IsLibraryAvailable();

    /// <inheritdoc/>
    public bool IsDriverSupported(FrameworkEcDriver driver)
    {
        return Native.NativeMethods.framework_ec_driver_is_supported((Native.FrameworkEcDriver)(int)driver);
    }

    /// <inheritdoc/>
    public FrameworkPlatform GetPlatform()
    {
        return (FrameworkPlatform)(int)Native.NativeMethods.GetPlatformOrThrow();
    }

    /// <inheritdoc/>
    public FrameworkPlatformFamily GetPlatformFamily()
    {
        return (FrameworkPlatformFamily)(int)Native.NativeMethods.GetPlatformFamilyOrThrow();
    }

    /// <inheritdoc/>
    public string GetProductName()
    {
        return Native.NativeMethods.GetProductNameOrThrow();
    }

    /// <inheritdoc/>
    public IFrameworkEcConnection OpenDefaultEc()
    {
        return FrameworkEcConnection.OpenDefault();
    }

    /// <inheritdoc/>
    public IFrameworkEcConnection OpenEcWithDriver(FrameworkEcDriver driver)
    {
        return FrameworkEcConnection.OpenWithDriver(driver);
    }
}
