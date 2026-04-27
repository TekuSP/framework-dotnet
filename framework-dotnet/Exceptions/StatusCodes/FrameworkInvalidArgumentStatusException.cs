using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.StatusCodes;

/// <summary>
/// Represents a native <see cref="FrameworkStatusCode.InvalidArgument"/> failure.
/// </summary>
public class FrameworkInvalidArgumentStatusException : FrameworkStatusCodeException
{
    internal FrameworkInvalidArgumentStatusException() : base(FrameworkStatusCode.InvalidArgument)
    {
    }

    internal FrameworkInvalidArgumentStatusException(FrameworkStatus status)
        : base(status)
    {
        payloadDescription = status.payload.GetPayloadDescription(status.code);
    }

    /// <summary>
    /// Gets a human-readable description of the invalid argument failure, when available.
    /// </summary>
    public override string? Description => base.Description ?? payloadDescription;

    private readonly string? payloadDescription;
}
