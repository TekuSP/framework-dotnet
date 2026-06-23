using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Snapshots;

using UnitsNet;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkChargeLimitsResult
{
    internal readonly FrameworkChargeLimitsSnapshot GetValueOrThrow()
    {
        if (status.IsFailure)
        {
            throw FrameworkStatusException.GetCorrectException(status);
        }

        return new FrameworkChargeLimitsSnapshot(
            Ratio.FromPercent(min_percent),
            Ratio.FromPercent(max_percent));
    }
}
