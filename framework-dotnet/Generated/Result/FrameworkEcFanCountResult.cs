using FrameworkDotnet.Exceptions;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcFanCountResult
{
    internal readonly byte GetValueOrThrow()
    {
        if (status.IsFailure)
        {
            throw FrameworkStatusException.GetCorrectException(status);
        }

        return fan_count;
    }
}
