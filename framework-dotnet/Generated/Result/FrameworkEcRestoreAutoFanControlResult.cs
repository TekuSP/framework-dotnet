using FrameworkDotnet.Exceptions.InvalidArgument;
using FrameworkDotnet.Responses;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcRestoreAutoFanControlResult
{
    internal readonly FrameworkEcRestoreAutoFanControlResult GetValueOrThrow()
    {
        if (status.IsFailure)
        {
            throw FrameworkInvalidFanIndexException.GetCorrectException(status.code);
        }
        return this;
    }
    internal readonly FrameworkRestoreAutoFanControlResponse ToManagedResponse()
    {
        return new FrameworkRestoreAutoFanControlResponse(fan_index);
    }
}
