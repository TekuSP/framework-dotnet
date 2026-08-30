using FrameworkDotnet.Exceptions;

using UnitsNet;

using ManagedStylusBatterySnapshot = FrameworkDotnet.Snapshots.FrameworkStylusBatterySnapshot;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkStylusBatteryResult
{
    internal readonly ManagedStylusBatterySnapshot GetValueOrThrow()
    {
        if (status.IsFailure)
        {
            throw FrameworkStatusException.GetCorrectException(status);
        }

        return new ManagedStylusBatterySnapshot(
            present != 0,
            Ratio.FromPercent(level_percent));
    }
}
