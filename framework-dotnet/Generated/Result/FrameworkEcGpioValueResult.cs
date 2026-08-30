using FrameworkDotnet.Exceptions;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcGpioValueResult
{
    internal readonly bool GetValueOrThrow()
    {
        if (status.IsFailure)
        {
            throw FrameworkStatusException.GetCorrectException(status);
        }

        return value != 0;
    }
}
