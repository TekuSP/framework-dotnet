using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.EcResponseDetails;

/// <summary>
/// Represents a native <see cref="FrameworkEcResponseDetail.Unavailable"/> EC response failure.
/// </summary>
public class FrameworkUnavailableEcResponseException : FrameworkEcResponseException
{
    internal FrameworkUnavailableEcResponseException(): base(FrameworkEcResponseDetail.Unavailable)
    {
    }
}
