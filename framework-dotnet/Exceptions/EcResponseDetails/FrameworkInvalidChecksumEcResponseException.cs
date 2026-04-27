using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.EcResponseDetails;

/// <summary>
/// Represents a native <see cref="FrameworkEcResponseDetail.InvalidChecksum"/> EC response failure.
/// </summary>
public class FrameworkInvalidChecksumEcResponseException : FrameworkEcResponseException
{
    internal FrameworkInvalidChecksumEcResponseException() : base(FrameworkEcResponseDetail.InvalidChecksum)
    {
    }
}
