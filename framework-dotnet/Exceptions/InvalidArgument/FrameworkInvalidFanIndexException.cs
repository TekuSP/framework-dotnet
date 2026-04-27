using System.Net.NetworkInformation;

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

    /// <summary>
    /// Gets the invalid zero-based fan index reported by the native API.
    /// </summary>
    public int FanIndex { get; }

    internal static FrameworkStatusException GetCorrectException(FrameworkStatus status)
    {
        if (status.code == FrameworkStatusCode.InvalidArgument)
        {
            return new FrameworkInvalidFanIndexException(status.payload.invalid_fan_index.fan_index);
        }
        else
        {
            return FrameworkEcResponseException.GetCorrectException(status.code);
        }
    }
}
