using System;

using Framework.System.Interop;
using FrameworkDotnet.Exceptions.StatusCodes;

namespace FrameworkDotnet.Exceptions;

/// <summary>
/// Represents a failure returned by the native Framework interop layer.
/// </summary>
public class FrameworkStatusException : FrameworkException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkStatusException"/> class.
    /// </summary>
    /// <param name="statusCode">The native status code name.</param>
    internal FrameworkStatusException(FrameworkStatusCode statusCode) : base()
    {
        StatusCode = statusCode;
    }

    internal FrameworkStatusCode StatusCode { get; }

    internal static FrameworkStatusException GetCorrectException(FrameworkStatusCode statusCode)
    {
        switch (statusCode)
        {
            case FrameworkStatusCode.Success:
                throw new ArgumentException("Status code indicates success, no exception should be thrown.", nameof(statusCode));
            case FrameworkStatusCode.NullPointer:
                return new FrameworkNullPointerStatusException();
            case FrameworkStatusCode.InvalidArgument:
                return new FrameworkInvalidArgumentStatusException();
            case FrameworkStatusCode.NoDriverAvailable:
                return new FrameworkNoDriverAvailableStatusException();
            case FrameworkStatusCode.UnsupportedDriver:
                return new FrameworkUnsupportedDriverStatusException();
            case FrameworkStatusCode.DeviceError:
                return new FrameworkDeviceErrorStatusException();
            case FrameworkStatusCode.EcResponse:
                return FrameworkEcResponseException.GetCorrectException(statusCode);
            case FrameworkStatusCode.UnknownResponseCode:
                return new FrameworkUnknownResponseCodeStatusException();
            case FrameworkStatusCode.DataUnavailable:
                return new FrameworkDataUnavailableStatusException();
            default:
                throw new ArgumentOutOfRangeException(nameof(statusCode), statusCode, "Unhandled status code.");
        }
    }
}
