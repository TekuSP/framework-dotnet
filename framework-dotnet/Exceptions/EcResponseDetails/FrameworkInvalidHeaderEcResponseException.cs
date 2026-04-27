using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.EcResponseDetails;

/// <summary>
/// Represents a native <see cref="FrameworkEcResponseDetail.InvalidHeader"/> EC response failure.
/// </summary>
public class FrameworkInvalidHeaderEcResponseException : FrameworkEcResponseException
{
    internal FrameworkInvalidHeaderEcResponseException() : base(FrameworkEcResponseDetail.InvalidHeader)
    {
    }
}
