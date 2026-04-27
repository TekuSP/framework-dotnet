using FrameworkDotnet.Exceptions;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcThermalSnapshotResult
{
    internal readonly FrameworkThermalSnapshot GetValueOrThrow()
    {
        if (status.IsFailure)
        {
            throw FrameworkStatusException.GetCorrectException(status.code);
        }
        return snapshot;
    }
}
