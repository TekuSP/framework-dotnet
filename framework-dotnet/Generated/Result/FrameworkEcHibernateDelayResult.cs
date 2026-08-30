using FrameworkDotnet.Exceptions;

using UnitsNet;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcHibernateDelayResult
{
    internal readonly Duration GetValueOrThrow()
    {
        if (status.IsFailure)
        {
            throw FrameworkStatusException.GetCorrectException(status);
        }

        return Duration.FromSeconds((double)seconds);
    }
}
