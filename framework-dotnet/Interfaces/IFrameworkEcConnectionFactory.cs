using FrameworkDotnet.Enums;

namespace FrameworkDotnet.Interfaces;

/// <summary>
/// Defines factory operations for opening Framework embedded controller connections.
/// </summary>
public interface IFrameworkEcConnectionFactory
{
    /// <summary>
    /// Opens the default embedded controller connection.
    /// </summary>
    /// <returns>An open EC connection.</returns>
    /// <exception cref="FrameworkDotnet.Exceptions.FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure.</exception>
    /// <exception cref="System.DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="System.BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="System.EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    /// <exception cref="System.InvalidOperationException">Thrown when the native library reports success but returns a null handle.</exception>
    IFrameworkEcConnection OpenDefaultEc();

    /// <summary>
    /// Opens an embedded controller connection using the specified driver.
    /// </summary>
    /// <param name="driver">The driver to use.</param>
    /// <returns>An open EC connection.</returns>
    /// <exception cref="FrameworkDotnet.Exceptions.FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure.</exception>
    /// <exception cref="System.DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="System.BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="System.EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    /// <exception cref="System.InvalidOperationException">Thrown when the native library reports success but returns a null handle.</exception>
    IFrameworkEcConnection OpenEcWithDriver(FrameworkEcDriver driver);
}
