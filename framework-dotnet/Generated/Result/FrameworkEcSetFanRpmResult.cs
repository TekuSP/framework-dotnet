using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Exceptions.InvalidArgument;
using FrameworkDotnet.Responses;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcSetFanRpmResult
{
    internal readonly FrameworkEcSetFanRpmResult GetValueOrThrow()
    {
        if (status.IsFailure)
        {
            throw FrameworkInvalidFanIndexException.GetCorrectException(status);
        }
        return this;
    }

    internal readonly FrameworkSetFanRpmResponse ToManagedResponse()
    {
        return new FrameworkSetFanRpmResponse(fan_index, rpm);
    }
}
