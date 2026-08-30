using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Snapshots;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcChargingStateResult
{
    internal readonly FrameworkChargingStateSnapshot GetValueOrThrow()
    {
        if (status.IsFailure)
        {
            throw FrameworkStatusException.GetCorrectException(status);
        }

        return new FrameworkChargingStateSnapshot(is_charging != 0, ac_present != 0);
    }
}
