using FrameworkDotnet.Exceptions;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcGpuSerialResult
{
    internal readonly string GetValueOrThrow()
    {
        var value = serial;

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

        return value.ToUtf8StringAndFree();
    }
}
