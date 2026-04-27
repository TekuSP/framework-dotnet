using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.EcResponseDetails;

/// <summary>
/// Represents a native <see cref="FrameworkEcResponseDetail.InvalidParameter"/> EC response failure.
/// </summary>
internal class FrameworkInvalidParameterEcResponseException : FrameworkEcResponseException
{
    internal FrameworkInvalidParameterEcResponseException() : base(FrameworkEcResponseDetail.InvalidParameter)
    {
    }
}
