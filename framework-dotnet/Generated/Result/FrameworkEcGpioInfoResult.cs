using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Snapshots;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcGpioInfoResult
{
    internal readonly FrameworkEcGpioSnapshot GetValueOrThrow()
    {
        var nameBuffer = name;

        if (status.IsFailure)
        {
            try
            {
                throw FrameworkStatusException.GetCorrectException(status);
            }
            finally
            {
                nameBuffer.Free();
            }
        }

        return new FrameworkEcGpioSnapshot(
            index,
            nameBuffer.ToUtf8StringAndFree(),
            value != 0,
            flags);
    }
}
