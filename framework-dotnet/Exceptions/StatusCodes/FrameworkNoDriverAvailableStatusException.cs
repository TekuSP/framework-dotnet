using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.StatusCodes;

/// <summary>
/// Represents a native <see cref="FrameworkStatusCode.NoDriverAvailable"/> failure.
/// </summary>
internal class FrameworkNoDriverAvailableStatusException : FrameworkStatusCodeException
{
    internal FrameworkNoDriverAvailableStatusException() : base(FrameworkStatusCode.NoDriverAvailable)
    {
    }
}
