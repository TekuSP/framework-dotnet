using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.StatusCodes;

/// <summary>
/// Represents a native <see cref="FrameworkStatusCode.InvalidArgument"/> failure.
/// </summary>
public class FrameworkInvalidArgumentStatusException : FrameworkStatusCodeException
{
    internal FrameworkInvalidArgumentStatusException() : base(FrameworkStatusCode.InvalidArgument)
    {
    }
}
