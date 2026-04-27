using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.EcResponseDetails;

/// <summary>
/// Represents a native <see cref="FrameworkEcResponseDetail.ResponseTooBig"/> EC response failure.
/// </summary>
internal class FrameworkResponseTooBigEcResponseException : FrameworkEcResponseException
{
    internal FrameworkResponseTooBigEcResponseException(): base(FrameworkEcResponseDetail.ResponseTooBig)
    {
    }
}
