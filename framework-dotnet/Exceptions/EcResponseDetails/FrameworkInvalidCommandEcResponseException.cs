using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.EcResponseDetails;

/// <summary>
/// Represents a native <see cref="FrameworkEcResponseDetail.InvalidCommand"/> EC response failure.
/// </summary>
public class FrameworkInvalidCommandEcResponseException : FrameworkEcResponseException
{
    internal FrameworkInvalidCommandEcResponseException() : base(FrameworkEcResponseDetail.InvalidCommand)
    {
    }
}
