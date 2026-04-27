using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.StatusCodes;

/// <summary>
/// Represents a native <see cref="FrameworkStatusCode.DeviceError"/> failure.
/// </summary>
internal class FrameworkDeviceErrorStatusException : FrameworkStatusCodeException
{
    internal FrameworkDeviceErrorStatusException() : base(FrameworkStatusCode.DeviceError)
    {
    }
}
