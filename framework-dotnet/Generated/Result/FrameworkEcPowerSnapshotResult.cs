using FrameworkDotnet.Exceptions;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcPowerSnapshotResult
{
    internal readonly FrameworkPowerSnapshot GetValueOrThrow()
    {
        if (status.IsFailure)
        {
            throw FrameworkEcResponseException.GetCorrectException(status);
        }
        return snapshot;
    }
}
