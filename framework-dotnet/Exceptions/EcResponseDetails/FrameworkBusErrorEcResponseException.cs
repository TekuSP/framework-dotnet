using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.EcResponseDetails;

/// <summary>
/// Represents a native <see cref="FrameworkEcResponseDetail.BusError"/> EC response failure.
/// </summary>
internal class FrameworkBusErrorEcResponseException : FrameworkEcResponseException
{
    internal FrameworkBusErrorEcResponseException() : base(FrameworkEcResponseDetail.BusError)
    {
    }
}
