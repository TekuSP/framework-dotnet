using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.StatusCodes;

/// <summary>
/// Represents a native <see cref="FrameworkStatusCode.UnknownResponseCode"/> failure.
/// </summary>
public class FrameworkUnknownResponseCodeStatusException : FrameworkStatusCodeException
{
    internal FrameworkUnknownResponseCodeStatusException() : base(FrameworkStatusCode.UnknownResponseCode)
    {
    }

    internal FrameworkUnknownResponseCodeStatusException(FrameworkStatusUnknownEcResponseCodeRecord unknownResponseCode)
        : base(FrameworkStatusCode.UnknownResponseCode)
    {
        ResponseCode = unknownResponseCode.ResponseCode;
        payloadDescription = unknownResponseCode.Description;
    }

    /// <summary>
    /// Gets the unknown native response code, when provided by the native layer.
    /// </summary>
    public int? ResponseCode { get; }

    private readonly string? payloadDescription;

    /// <summary>
    /// Gets a human-readable description of the unknown native response code, when available.
    /// </summary>
    public override string? Description => base.Description ?? payloadDescription ?? (ResponseCode.HasValue ? $"Unknown response code: {ResponseCode.Value}" : null);
}
