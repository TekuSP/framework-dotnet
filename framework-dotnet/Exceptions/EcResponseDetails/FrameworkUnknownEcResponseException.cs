using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.EcResponseDetails;

/// <summary>
/// Represents a native <see cref="FrameworkEcResponseDetail.Unknown"/> EC response failure.
/// </summary>
internal class FrameworkUnknownEcResponseException : FrameworkEcResponseException
{
    internal FrameworkUnknownEcResponseException() : base(FrameworkEcResponseDetail.Unknown)
    {
    }
}
