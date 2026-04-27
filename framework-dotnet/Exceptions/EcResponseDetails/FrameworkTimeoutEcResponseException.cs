using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.EcResponseDetails;

/// <summary>
/// Represents a native <see cref="FrameworkEcResponseDetail.Timeout"/> EC response failure.
/// </summary>
internal class FrameworkTimeoutEcResponseException : FrameworkEcResponseException
{
    internal FrameworkTimeoutEcResponseException() : base(FrameworkEcResponseDetail.Timeout)
    {
    }
}
