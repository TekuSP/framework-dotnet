using FrameworkDotnet.Enums;
using FrameworkDotnet.Exceptions;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcFeatureFlagsResult
{
    internal readonly FrameworkEcFeatureFlags GetValueOrThrow()
    {
        if (status.IsFailure)
        {
            throw FrameworkStatusException.GetCorrectException(status);
        }

        return (FrameworkEcFeatureFlags)flags;
    }
}
