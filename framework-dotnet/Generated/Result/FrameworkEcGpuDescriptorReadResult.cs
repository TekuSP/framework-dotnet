using FrameworkDotnet.Exceptions;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcGpuDescriptorReadResult
{
    internal readonly byte[] GetValueOrThrow()
    {
        var value = descriptor;

        if (status.IsFailure)
        {
            try
            {
                throw FrameworkStatusException.GetCorrectException(status);
            }
            finally
            {
                value.Free();
            }
        }

        return value.ToArrayAndFree();
    }
}
