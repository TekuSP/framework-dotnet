using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Snapshots;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkPrivacySwitchesResult
{
    internal readonly FrameworkPrivacySwitchesSnapshot GetValueOrThrow()
    {
        if (status.IsFailure)
        {
            throw FrameworkStatusException.GetCorrectException(status);
        }

        return new FrameworkPrivacySwitchesSnapshot(
            microphone_enabled != 0,
            camera_enabled != 0);
    }
}
