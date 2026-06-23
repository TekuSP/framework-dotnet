using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Snapshots;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkChassisIntrusionResult
{
    internal readonly FrameworkChassisIntrusionSnapshot GetValueOrThrow()
    {
        if (status.IsFailure)
        {
            throw FrameworkStatusException.GetCorrectException(status);
        }

        return new FrameworkChassisIntrusionSnapshot(
            currently_open != 0,
            coin_cell_ever_removed != 0,
            ever_opened != 0,
            total_opened,
            vtr_open_count);
    }
}
