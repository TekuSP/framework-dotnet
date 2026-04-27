using FrameworkDotnet.Exceptions;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcBuildInfoResult
{
    internal readonly string GetValueOrThrow()
    {
        if (status.IsFailure)
        {
            throw FrameworkEcResponseException.GetCorrectException(status);
        }
        return build_info.ToUtf8StringAndFree();
    }
}
