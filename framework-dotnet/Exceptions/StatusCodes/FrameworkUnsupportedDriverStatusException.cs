using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.StatusCodes;

/// <summary>
/// Represents a native <see cref="FrameworkStatusCode.UnsupportedDriver"/> failure.
/// </summary>
internal class FrameworkUnsupportedDriverStatusException : FrameworkStatusCodeException
{
    internal FrameworkUnsupportedDriverStatusException() : base(FrameworkStatusCode.UnsupportedDriver)
    {
    }
}
