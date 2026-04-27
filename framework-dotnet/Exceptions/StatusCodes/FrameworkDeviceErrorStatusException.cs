using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.StatusCodes;

/// <summary>
/// Represents a native <see cref="FrameworkStatusCode.DeviceError"/> failure.
/// </summary>
public class FrameworkDeviceErrorStatusException : FrameworkStatusCodeException
{
    internal FrameworkDeviceErrorStatusException() : base(FrameworkStatusCode.DeviceError)
    {
    }

    internal FrameworkDeviceErrorStatusException(FrameworkStatusDeviceErrorRecord deviceError)
        : base(FrameworkStatusCode.DeviceError)
    {
        MessageToken = deviceError.MessageToken;
        payloadDescription = deviceError.Description;
    }

    /// <summary>
    /// Gets the native device error message token, when provided by the native layer.
    /// </summary>
    public int? MessageToken { get; }

    private readonly string? payloadDescription;

    /// <summary>
    /// Gets a human-readable description of the native device error, when available.
    /// </summary>
    public override string? Description => base.Description ?? DeviceErrorMessage ?? payloadDescription;

    /// <summary>
    /// Gets the resolved native device error message, when the native layer provides one.
    /// </summary>
    public string? DeviceErrorMessage { get; private set; }

    public override string Message => DeviceErrorMessage is { Length: > 0 }
        ? $"{base.Message}: {DeviceErrorMessage}"
        : base.Message;

    internal void SetDeviceErrorMessage(string? deviceErrorMessage)
    {
        if (!string.IsNullOrWhiteSpace(deviceErrorMessage))
        {
            DeviceErrorMessage = deviceErrorMessage;
        }
    }
}
