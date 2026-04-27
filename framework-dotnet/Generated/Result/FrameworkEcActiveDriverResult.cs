using FrameworkDotnet.Exceptions;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcActiveDriverResult
{
    internal readonly FrameworkEcDriver GetValueOrThrow()
    {
        if (status.IsFailure)
        {
            throw FrameworkEcResponseException.GetCorrectException(status.code);
        }
        return driver;
    }
}
