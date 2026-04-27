using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.StatusCodes;

/// <summary>
/// Represents a native <see cref="FrameworkStatusCode.DataUnavailable"/> failure.
/// </summary>
internal class FrameworkDataUnavailableStatusException : FrameworkStatusCodeException
{
    internal FrameworkDataUnavailableStatusException() : base(FrameworkStatusCode.DataUnavailable)
    {
    }
}
