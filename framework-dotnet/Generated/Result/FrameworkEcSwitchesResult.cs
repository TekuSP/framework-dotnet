using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Snapshots;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcSwitchesResult
{
    internal readonly FrameworkEcSwitchesSnapshot GetValueOrThrow()
    {
        if (status.IsFailure)
        {
            throw FrameworkStatusException.GetCorrectException(status);
        }

        return new FrameworkEcSwitchesSnapshot(
            raw,
            lid_open != 0,
            power_button_pressed != 0,
            write_protect_disabled != 0,
            dedicated_recovery != 0);
    }
}
