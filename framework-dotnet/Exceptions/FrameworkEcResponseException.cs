using System;

using Framework.System.Interop;

using FrameworkDotnet.Exceptions.EcResponseDetails;
using FrameworkDotnet.Exceptions.StatusCodes;

namespace FrameworkDotnet.Exceptions;

/// <summary>
/// Represents a native EC response failure.
/// </summary>
public class FrameworkEcResponseException : FrameworkStatusCodeException
{
    internal FrameworkEcResponseException(FrameworkEcResponseDetail detail) : base(FrameworkStatusCode.EcResponse)
    {
        Detail = detail;
    }

    internal FrameworkEcResponseDetail Detail
    {
        get;
    }

    internal static new FrameworkStatusException GetCorrectException(FrameworkStatusCode statusCode)
    {
        return GetCorrectException((FrameworkEcResponseDetail)(int)statusCode);
    }

    internal static FrameworkStatusException GetCorrectException(FrameworkEcResponseDetail statusCode)
    {
        switch (statusCode)
        {
            case FrameworkEcResponseDetail.Unknown:
                return new FrameworkUnknownEcResponseException();
            case FrameworkEcResponseDetail.Success:
                throw new ArgumentException("Status code indicates success, no exception should be thrown.", nameof(statusCode));
            case FrameworkEcResponseDetail.InvalidCommand:
                return new FrameworkInvalidCommandEcResponseException();
            case FrameworkEcResponseDetail.Error:
                return new FrameworkErrorEcResponseException();
            case FrameworkEcResponseDetail.InvalidParameter:
                return new FrameworkInvalidParameterEcResponseException();
            case FrameworkEcResponseDetail.AccessDenied:
                return new FrameworkAccessDeniedEcResponseException();
            case FrameworkEcResponseDetail.InvalidResponse:
                return new FrameworkInvalidResponseEcResponseException();
            case FrameworkEcResponseDetail.InvalidVersion:
                return new FrameworkInvalidVersionEcResponseException();
            case FrameworkEcResponseDetail.InvalidChecksum:
                return new FrameworkInvalidChecksumEcResponseException();
            case FrameworkEcResponseDetail.InProgress:
                return new FrameworkInProgressEcResponseException();
            case FrameworkEcResponseDetail.Unavailable:
                return new FrameworkUnavailableEcResponseException();
            case FrameworkEcResponseDetail.Timeout:
                return new FrameworkTimeoutEcResponseException();
            case FrameworkEcResponseDetail.Overflow:
                return new FrameworkOverflowEcResponseException();
            case FrameworkEcResponseDetail.InvalidHeader:
                return new FrameworkInvalidHeaderEcResponseException();
            case FrameworkEcResponseDetail.RequestTruncated:
                return new FrameworkRequestTruncatedEcResponseException();
            case FrameworkEcResponseDetail.ResponseTooBig:
                return new FrameworkResponseTooBigEcResponseException();
            case FrameworkEcResponseDetail.BusError:
                return new FrameworkBusErrorEcResponseException();
            case FrameworkEcResponseDetail.Busy:
                return new FrameworkBusyEcResponseException();
            default:
                throw new ArgumentOutOfRangeException(nameof(statusCode), statusCode, "Unhandled status code.");
        }
    }
}
