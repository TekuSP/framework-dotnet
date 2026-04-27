using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Exceptions.InvalidArgument;
using FrameworkDotnet.Responses;

using UnitsNet;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcSetFanDutyResult
{
    internal readonly FrameworkEcSetFanDutyResult GetValueOrThrow()
    {
        if (status.IsFailure)
        {
            throw FrameworkInvalidFanIndexException.GetCorrectException(status);
        }
        return this;
    }
    internal readonly FrameworkSetFanDutyResponse ToManagedResponse()
    {
        return new FrameworkSetFanDutyResponse(fan_index, Ratio.FromPercent(percent));
    }
}
