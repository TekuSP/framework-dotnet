using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.StatusCodes;

/// <summary>
/// Represents an exception mapped directly from a <see cref="FrameworkStatusCode"/> value.
/// </summary>
public abstract class FrameworkStatusCodeException : FrameworkStatusException
{
    internal FrameworkStatusCodeException(FrameworkStatusCode statusCode) : base(statusCode)
    {
    }

    internal FrameworkStatusCodeException(FrameworkStatus status)
        : base(status.code)
    {
    }
}
