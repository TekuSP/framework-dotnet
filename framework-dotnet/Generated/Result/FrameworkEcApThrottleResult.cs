using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Snapshots;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcApThrottleResult
{
    internal readonly FrameworkEcApThrottleSnapshot GetValueOrThrow()
    {
        if (status.IsFailure)
        {
            throw FrameworkStatusException.GetCorrectException(status);
        }

        return new FrameworkEcApThrottleSnapshot(
            soft_throttled != 0,
            hard_throttled != 0);
    }
}
