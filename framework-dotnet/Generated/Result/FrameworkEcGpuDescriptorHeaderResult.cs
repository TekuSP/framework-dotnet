using FrameworkDotnet.Exceptions;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcGpuDescriptorHeaderResult
{
    internal readonly FrameworkGpuDescriptorHeader GetValueOrThrow()
    {
        if (status.IsFailure)
        {
            throw FrameworkStatusException.GetCorrectException(status);
        }

        return header;
    }
}
