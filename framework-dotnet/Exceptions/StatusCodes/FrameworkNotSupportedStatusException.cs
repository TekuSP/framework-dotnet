using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.StatusCodes;

/// <summary>
/// Represents a native <see cref="FrameworkStatusCode.NotSupported"/> failure, raised when the
/// requested capability is not compiled in for this platform.
/// </summary>
/// <remarks>
/// <para>
/// The condition is permanent for this build and host operating system: the native library
/// contains no implementation of the requested capability, so retrying the call can never
/// succeed. This is the distinction from <see cref="FrameworkDataUnavailableStatusException"/>,
/// which reports a transient read failure where the capability does exist but the value could
/// not be obtained on this attempt and a later attempt may succeed.
/// </para>
/// <para>
/// At present only the NVMe drive version readback (<c>framework_get_nvme_version</c>) reports
/// this status, and only on non-Linux hosts, because the underlying NVMe admin passthrough
/// ioctl is gated to Linux upstream.
/// </para>
/// </remarks>
public class FrameworkNotSupportedStatusException : FrameworkStatusCodeException
{
    internal FrameworkNotSupportedStatusException() : base(FrameworkStatusCode.NotSupported)
    {
    }
}
