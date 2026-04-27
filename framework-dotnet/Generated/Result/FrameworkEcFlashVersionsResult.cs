using FrameworkDotnet.Exceptions;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcFlashVersionsResult
{
    internal readonly FrameworkEcFlashVersions GetValueOrThrow()
    {
        if (status.IsFailure)
        {
            throw FrameworkEcResponseException.GetCorrectException(status);
        }
        return versions;
    }
}
