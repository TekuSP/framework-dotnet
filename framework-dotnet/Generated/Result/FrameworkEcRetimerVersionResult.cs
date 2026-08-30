using FrameworkDotnet.Exceptions;

using ManagedPowerDeliveryRetimerVersionSnapshot = FrameworkDotnet.Snapshots.FrameworkPowerDeliveryRetimerVersionSnapshot;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcRetimerVersionResult
{
    internal readonly ManagedPowerDeliveryRetimerVersionSnapshot GetValueOrThrow()
    {
        var buffer = version;

        if (status.IsFailure)
        {
            try
            {
                throw FrameworkStatusException.GetCorrectException(status);
            }
            finally
            {
                buffer.Free();
            }
        }

        return new ManagedPowerDeliveryRetimerVersionSnapshot(
            present != 0,
            buffer.ToArrayAndFree());
    }
}
