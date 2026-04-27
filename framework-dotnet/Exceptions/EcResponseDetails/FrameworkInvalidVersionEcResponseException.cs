using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.EcResponseDetails;

/// <summary>
/// Represents a native <see cref="FrameworkEcResponseDetail.InvalidVersion"/> EC response failure.
/// </summary>
internal class FrameworkInvalidVersionEcResponseException : FrameworkEcResponseException
{
    internal FrameworkInvalidVersionEcResponseException() : base(FrameworkEcResponseDetail.InvalidVersion)
    {
    }
}
