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
    internal FrameworkEcResponseException(Framework.System.Interop.FrameworkEcResponseDetail detail) : base(Framework.System.Interop.FrameworkStatusCode.EcResponse)
    {
        this.detail = detail;
    }

    internal FrameworkEcResponseException(FrameworkStatusEcResponseRecord ecResponse)
        : base(Framework.System.Interop.FrameworkStatusCode.EcResponse)
    {
        detail = ecResponse.Response;
        payloadDescription = ecResponse.Description;
    }

    /// <summary>
    /// Gets the native EC response detail name associated with the exception.
    /// </summary>
    public string Detail => detail.ToString();

    private readonly Framework.System.Interop.FrameworkEcResponseDetail detail;
    private readonly string? payloadDescription;

    /// <inheritdoc/>
    public override string? Description => base.Description ?? payloadDescription ?? Detail;

    internal static new FrameworkStatusException GetCorrectException(Framework.System.Interop.FrameworkStatusCode statusCode)
    {
        return GetCorrectException((Framework.System.Interop.FrameworkEcResponseDetail)(int)statusCode);
    }

    internal static FrameworkStatusException GetCorrectException(FrameworkStatusEcResponseRecord ecResponse)
    {
        return GetCorrectException(ecResponse.Response, ecResponse);
    }

    internal new static FrameworkStatusException GetCorrectException(FrameworkStatus status)
    {
        var exception = GetCorrectException(status.payload.ec_response);

        try
        {
            exception.SetNativeDescription(NativeMethods.GetStatusDescriptionOrEmpty(status));
        }
        catch
        {
        }

        return exception;
    }

    internal static FrameworkStatusException GetCorrectException(Framework.System.Interop.FrameworkEcResponseDetail statusCode)
    {
        return GetCorrectException(statusCode, null);
    }

    private static FrameworkStatusException GetCorrectException(Framework.System.Interop.FrameworkEcResponseDetail statusCode, FrameworkStatusEcResponseRecord? ecResponse)
    {
        FrameworkStatusException exception = statusCode switch
        {
            Framework.System.Interop.FrameworkEcResponseDetail.Unknown => new FrameworkUnknownEcResponseException(),
            Framework.System.Interop.FrameworkEcResponseDetail.Success => throw new ArgumentException("Status code indicates success, no exception should be thrown.", nameof(statusCode)),
            Framework.System.Interop.FrameworkEcResponseDetail.InvalidCommand => new FrameworkInvalidCommandEcResponseException(),
            Framework.System.Interop.FrameworkEcResponseDetail.Error => new FrameworkErrorEcResponseException(),
            Framework.System.Interop.FrameworkEcResponseDetail.InvalidParameter => new FrameworkInvalidParameterEcResponseException(),
            Framework.System.Interop.FrameworkEcResponseDetail.AccessDenied => new FrameworkAccessDeniedEcResponseException(),
            Framework.System.Interop.FrameworkEcResponseDetail.InvalidResponse => new FrameworkInvalidResponseEcResponseException(),
            Framework.System.Interop.FrameworkEcResponseDetail.InvalidVersion => new FrameworkInvalidVersionEcResponseException(),
            Framework.System.Interop.FrameworkEcResponseDetail.InvalidChecksum => new FrameworkInvalidChecksumEcResponseException(),
            Framework.System.Interop.FrameworkEcResponseDetail.InProgress => new FrameworkInProgressEcResponseException(),
            Framework.System.Interop.FrameworkEcResponseDetail.Unavailable => new FrameworkUnavailableEcResponseException(),
            Framework.System.Interop.FrameworkEcResponseDetail.Timeout => new FrameworkTimeoutEcResponseException(),
            Framework.System.Interop.FrameworkEcResponseDetail.Overflow => new FrameworkOverflowEcResponseException(),
            Framework.System.Interop.FrameworkEcResponseDetail.InvalidHeader => new FrameworkInvalidHeaderEcResponseException(),
            Framework.System.Interop.FrameworkEcResponseDetail.RequestTruncated => new FrameworkRequestTruncatedEcResponseException(),
            Framework.System.Interop.FrameworkEcResponseDetail.ResponseTooBig => new FrameworkResponseTooBigEcResponseException(),
            Framework.System.Interop.FrameworkEcResponseDetail.BusError => new FrameworkBusErrorEcResponseException(),
            Framework.System.Interop.FrameworkEcResponseDetail.Busy => new FrameworkBusyEcResponseException(),
            _ => throw new ArgumentOutOfRangeException(nameof(statusCode), statusCode, "Unhandled status code.")
        };

        if (ecResponse.HasValue)
        {
            exception.SetNativeDescription(ecResponse.Value.Description);
        }

        return exception;
    }
}
