using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.StatusCodes;

/// <summary>
/// Represents a native <see cref="FrameworkStatusCode.UnknownResponseCode"/> failure.
/// </summary>
internal class FrameworkUnknownResponseCodeStatusException : FrameworkStatusCodeException
{
    internal FrameworkUnknownResponseCodeStatusException() : base(FrameworkStatusCode.UnknownResponseCode)
    {
    }
}
