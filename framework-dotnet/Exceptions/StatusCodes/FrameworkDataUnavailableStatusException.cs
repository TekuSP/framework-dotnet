using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.StatusCodes;

/// <summary>
/// Represents a native <see cref="FrameworkStatusCode.DataUnavailable"/> failure.
/// </summary>
public class FrameworkDataUnavailableStatusException : FrameworkStatusCodeException
{
    internal FrameworkDataUnavailableStatusException() : base(FrameworkStatusCode.DataUnavailable)
    {
    }
}
