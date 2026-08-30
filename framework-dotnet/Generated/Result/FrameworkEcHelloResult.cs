using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Snapshots;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcHelloResult
{
    internal readonly FrameworkEcHelloSnapshot GetValueOrThrow()
    {
        if (status.IsFailure)
        {
            throw FrameworkStatusException.GetCorrectException(status);
        }

        return new FrameworkEcHelloSnapshot(
            out_data,
            is_expected != 0);
    }
}
