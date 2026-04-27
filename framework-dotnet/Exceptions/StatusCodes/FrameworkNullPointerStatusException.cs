using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.StatusCodes;

/// <summary>
/// Represents a native <see cref="FrameworkStatusCode.NullPointer"/> failure.
/// </summary>
public class FrameworkNullPointerStatusException : FrameworkStatusCodeException
{
    internal FrameworkNullPointerStatusException() : base(FrameworkStatusCode.NullPointer)
    {
    }
}
