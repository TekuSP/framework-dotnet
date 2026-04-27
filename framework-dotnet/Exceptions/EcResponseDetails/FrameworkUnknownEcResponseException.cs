using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.EcResponseDetails;

/// <summary>
/// Represents a native <see cref="FrameworkEcResponseDetail.Unknown"/> EC response failure.
/// </summary>
public class FrameworkUnknownEcResponseException : FrameworkEcResponseException
{
    internal FrameworkUnknownEcResponseException() : base(FrameworkEcResponseDetail.Unknown)
    {
    }
}
