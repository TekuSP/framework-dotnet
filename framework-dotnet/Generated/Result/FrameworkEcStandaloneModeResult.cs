using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Snapshots;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcStandaloneModeResult
{
    internal readonly FrameworkStandaloneModeSnapshot GetValueOrThrow()
    {
        if (status.IsFailure)
        {
            throw FrameworkStatusException.GetCorrectException(status);
        }

        return new FrameworkStandaloneModeSnapshot(
            is_standalone != 0,
            standalone_mode != 0);
    }
}
