using Framework.System.Interop;

using FrameworkDotnet.Exceptions.StatusCodes;

namespace FrameworkDotnet.Exceptions.InvalidArgument;

/// <summary>
/// Represents a native invalid fan index failure.
/// </summary>
public class FrameworkInvalidFanIndexException : FrameworkInvalidArgumentStatusException
{
    internal FrameworkInvalidFanIndexException(int fanIndex) : base()
    {
        FanIndex = fanIndex;
    }

    internal FrameworkInvalidFanIndexException(FrameworkStatusInvalidFanIndexRecord invalidFanIndex)
        : base()
    {
        FanIndex = invalidFanIndex.FanIndex;
    }

    /// <summary>
    /// Gets the invalid zero-based fan index reported by the native API.
    /// </summary>
    public int FanIndex { get; }

    /// <summary>
    /// Gets a human-readable description of the invalid fan index failure.
    /// </summary>
    public override string? Description => base.Description ?? $"Invalid fan index: {FanIndex}";

    internal new static FrameworkStatusException GetCorrectException(FrameworkStatus status)
    {
        if (status.code == FrameworkStatusCode.InvalidArgument)
        {
            var exception = new FrameworkInvalidFanIndexException(status.payload.invalid_fan_index);

            try
            {
                exception.SetNativeDescription(NativeMethods.GetStatusDescriptionOrEmpty(status));
            }
            catch
            {
            }

            return exception;
        }
        else
        {
            return FrameworkEcResponseException.GetCorrectException(status);
        }
    }
}
