using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.EcResponseDetails;

/// <summary>
/// Represents a native <see cref="FrameworkEcResponseDetail.AccessDenied"/> EC response failure.
/// </summary>
public class FrameworkAccessDeniedEcResponseException : FrameworkEcResponseException
{
    internal FrameworkAccessDeniedEcResponseException() : base(FrameworkEcResponseDetail.AccessDenied)
    {
    }
}
