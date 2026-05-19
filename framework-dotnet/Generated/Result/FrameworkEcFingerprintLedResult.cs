using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Snapshots;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcFingerprintLedResult
{
    internal readonly FrameworkFingerprintLedSnapshot GetValueOrThrow()
    {
        if (status.IsFailure)
        {
            throw FrameworkStatusException.GetCorrectException(status);
        }

        var value = state;
        return value.ToManagedSnapshot();
    }
}
