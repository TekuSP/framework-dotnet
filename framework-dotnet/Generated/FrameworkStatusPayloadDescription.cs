namespace Framework.System.Interop;

internal unsafe partial struct FrameworkStatusPayload
{
    internal readonly string? GetPayloadDescription(FrameworkStatusCode code)
    {
        return code switch
        {
            FrameworkStatusCode.InvalidArgument => invalid_fan_index.Description,
            FrameworkStatusCode.EcResponse => ec_response.Description,
            FrameworkStatusCode.UnknownResponseCode => unknown_ec_response_code.Description,
            FrameworkStatusCode.DeviceError => device_error.Description,
            _ => none.Description,
        };
    }
}
