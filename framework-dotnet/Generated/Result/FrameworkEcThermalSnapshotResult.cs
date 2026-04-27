using FrameworkDotnet.Exceptions;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcThermalSnapshotResult
{
    internal readonly FrameworkThermalSnapshot GetValueOrThrow()
    {
        if (status.IsFailure)
        {
            throw FrameworkEcResponseException.GetCorrectException(status);
        }
        return snapshot;
    }
}
