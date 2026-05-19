using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Snapshots;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcExpansionBayStatusResult
{
    internal readonly FrameworkExpansionBaySnapshot GetValueOrThrow()
    {
        var value = bay;

        if (status.IsFailure)
        {
            try
            {
                throw FrameworkStatusException.GetCorrectException(status);
            }
            finally
            {
                value.FreeOwnedBuffers();
            }
        }

        return value.ToManagedSnapshot();
    }
}
