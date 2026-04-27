using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.EcResponseDetails;

/// <summary>
/// Represents a native <see cref="FrameworkEcResponseDetail.Busy"/> EC response failure.
/// </summary>
public class FrameworkBusyEcResponseException : FrameworkEcResponseException
{
    internal FrameworkBusyEcResponseException() : base(FrameworkEcResponseDetail.Busy)
    {
    }
}
