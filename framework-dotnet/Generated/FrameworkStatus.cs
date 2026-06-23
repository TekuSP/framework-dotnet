using FrameworkDotnet.Exceptions;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkStatus
{
    internal readonly bool IsSuccess => code == FrameworkStatusCode.Success;

    internal readonly bool IsFailure => !IsSuccess;

    internal readonly void ThrowIfFailure()
    {
        if (IsFailure)
        {
            throw FrameworkStatusException.GetCorrectException(this);
        }
    }
}
