using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Snapshots;

using UnitsNet;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcKeyboardBacklightResult
{
    internal readonly FrameworkKeyboardBacklightSnapshot GetValueOrThrow()
    {
        if (status.IsFailure)
        {
            throw FrameworkStatusException.GetCorrectException(status);
        }

        return new FrameworkKeyboardBacklightSnapshot(Ratio.FromPercent(brightness_percent));
    }
}
