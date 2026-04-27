using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.EcResponseDetails;

/// <summary>
/// Represents a native <see cref="FrameworkEcResponseDetail.RequestTruncated"/> EC response failure.
/// </summary>
internal class FrameworkRequestTruncatedEcResponseException : FrameworkEcResponseException
{
    internal FrameworkRequestTruncatedEcResponseException(): base(FrameworkEcResponseDetail.RequestTruncated)
    {
    }
}
