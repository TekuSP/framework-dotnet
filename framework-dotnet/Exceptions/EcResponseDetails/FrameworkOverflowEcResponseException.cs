using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.EcResponseDetails;

/// <summary>
/// Represents a native <see cref="FrameworkEcResponseDetail.Overflow"/> EC response failure.
/// </summary>
public class FrameworkOverflowEcResponseException : FrameworkEcResponseException
{
    internal FrameworkOverflowEcResponseException(): base(FrameworkEcResponseDetail.Overflow)
    {
    }
}
