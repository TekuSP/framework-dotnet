using System;

using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Snapshots;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcUptimeResult
{
    internal readonly FrameworkEcUptimeSnapshot GetValueOrThrow()
    {
        if (status.IsFailure)
        {
            throw FrameworkStatusException.GetCorrectException(status);
        }

        return new FrameworkEcUptimeSnapshot(
            TimeSpan.FromMilliseconds(time_since_ec_boot_ms),
            ap_resets_since_ec_boot,
            ec_reset_flags);
    }
}
