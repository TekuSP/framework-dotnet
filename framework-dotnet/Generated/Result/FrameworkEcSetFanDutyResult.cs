using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Exceptions.InvalidArgument;
using FrameworkDotnet.Responses;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcSetFanDutyResult
{
    internal readonly FrameworkEcSetFanDutyResult GetValueOrThrow()
    {
        if (status.IsFailure)
        {
            throw FrameworkInvalidFanIndexException.GetCorrectException(status.code);
        }
        return this;
    }
    internal readonly FrameworkSetFanDutyResponse ToManagedResponse()
    {
        return new FrameworkSetFanDutyResponse(fan_index, percent);
    }
}
