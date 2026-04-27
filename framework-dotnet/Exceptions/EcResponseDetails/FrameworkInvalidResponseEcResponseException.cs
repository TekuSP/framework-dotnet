using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.EcResponseDetails;

/// <summary>
/// Represents a native <see cref="FrameworkEcResponseDetail.InvalidResponse"/> EC response failure.
/// </summary>
internal class FrameworkInvalidResponseEcResponseException : FrameworkEcResponseException
{
    internal FrameworkInvalidResponseEcResponseException() : base(FrameworkEcResponseDetail.InvalidResponse)
    {
    }
}
