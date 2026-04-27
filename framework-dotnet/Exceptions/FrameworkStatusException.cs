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
    internal FrameworkStatusException(Framework.System.Interop.FrameworkStatusCode statusCode) : base()
    {
        this.statusCode = statusCode;
    }

    /// <summary>
    /// Gets the native status code name associated with the exception.
    /// </summary>
    public string StatusCode => statusCode.ToString();

    /// <summary>
    /// Gets a human-readable description of the native failure, when available.
    /// </summary>
    public virtual string? Description => description;

    /// <inheritdoc/>
    public override string Message => Description is { Length: > 0 }
        ? $"{StatusCode}: {Description}"
        : StatusCode;

    private string? description;
    private readonly Framework.System.Interop.FrameworkStatusCode statusCode;

    internal static FrameworkStatusException GetCorrectException(Framework.System.Interop.FrameworkStatusCode statusCode)
    {
        switch (statusCode)
        {
            case Framework.System.Interop.FrameworkStatusCode.Success:
                throw new ArgumentException("Status code indicates success, no exception should be thrown.", nameof(statusCode));
            case Framework.System.Interop.FrameworkStatusCode.NullPointer:
                return new FrameworkNullPointerStatusException();
            case Framework.System.Interop.FrameworkStatusCode.InvalidArgument:
                return new FrameworkInvalidArgumentStatusException();
            case Framework.System.Interop.FrameworkStatusCode.NoDriverAvailable:
                return new FrameworkNoDriverAvailableStatusException();
            case Framework.System.Interop.FrameworkStatusCode.UnsupportedDriver:
                return new FrameworkUnsupportedDriverStatusException();
            case Framework.System.Interop.FrameworkStatusCode.DeviceError:
                return new FrameworkDeviceErrorStatusException();
            case Framework.System.Interop.FrameworkStatusCode.EcResponse:
                return FrameworkEcResponseException.GetCorrectException(statusCode);
            case Framework.System.Interop.FrameworkStatusCode.UnknownResponseCode:
                return new FrameworkUnknownResponseCodeStatusException();
            case Framework.System.Interop.FrameworkStatusCode.DataUnavailable:
                return new FrameworkDataUnavailableStatusException();
            default:
                throw new ArgumentOutOfRangeException(nameof(statusCode), statusCode, "Unhandled status code.");
        }
    }

    internal static FrameworkStatusException GetCorrectException(FrameworkStatus status)
    {
        FrameworkStatusException exception = status.code switch
        {
            Framework.System.Interop.FrameworkStatusCode.Success => throw new ArgumentException("Status code indicates success, no exception should be thrown.", nameof(status)),
            Framework.System.Interop.FrameworkStatusCode.InvalidArgument => new FrameworkInvalidArgumentStatusException(status),
            Framework.System.Interop.FrameworkStatusCode.DeviceError => new FrameworkDeviceErrorStatusException(status.payload.device_error),
            Framework.System.Interop.FrameworkStatusCode.EcResponse => FrameworkEcResponseException.GetCorrectException(status),
            Framework.System.Interop.FrameworkStatusCode.UnknownResponseCode => new FrameworkUnknownResponseCodeStatusException(status.payload.unknown_ec_response_code),
            _ => GetCorrectException(status.code)
        };

        TrySetNativeDescription(exception, status);

        if (exception is FrameworkDeviceErrorStatusException deviceErrorException)
        {
            TrySetDeviceErrorMessage(deviceErrorException, status);
        }

        return exception;
    }

    internal void SetNativeDescription(string? nativeDescription)
    {
        if (!string.IsNullOrWhiteSpace(nativeDescription))
        {
            description = nativeDescription;
        }
    }

    private static void TrySetNativeDescription(FrameworkStatusException exception, FrameworkStatus status)
    {
        try
        {
            exception.SetNativeDescription(NativeMethods.GetStatusDescriptionOrEmpty(status));
        }
        catch
        {
        }
    }

    private static void TrySetDeviceErrorMessage(FrameworkDeviceErrorStatusException exception, FrameworkStatus status)
    {
        try
        {
            exception.SetDeviceErrorMessage(NativeMethods.GetDeviceErrorMessageOrEmpty(status));
        }
        catch
        {
        }
    }
}
