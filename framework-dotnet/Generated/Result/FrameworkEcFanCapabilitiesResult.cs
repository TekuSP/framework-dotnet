using FrameworkDotnet.Exceptions;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcFanCapabilitiesResult
{
    internal readonly FrameworkFanCapabilities GetValueOrThrow()
    {
        if (status.IsFailure)
        {
            throw FrameworkEcResponseException.GetCorrectException(status);
        }
        return capabilities;
    }
}
