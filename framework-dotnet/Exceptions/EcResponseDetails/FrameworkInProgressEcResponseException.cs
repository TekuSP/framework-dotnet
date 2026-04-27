using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.EcResponseDetails;

/// <summary>
/// Represents a native <see cref="FrameworkEcResponseDetail.InProgress"/> EC response failure.
/// </summary>
internal class FrameworkInProgressEcResponseException : FrameworkEcResponseException
{
    internal FrameworkInProgressEcResponseException() : base(FrameworkEcResponseDetail.InProgress)
    {
    }
}
