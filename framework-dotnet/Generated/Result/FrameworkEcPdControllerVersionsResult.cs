using FrameworkDotnet.Exceptions;

using ManagedPowerDeliveryControllerSlot = FrameworkDotnet.Enums.FrameworkPowerDeliveryControllerSlot;
using ManagedPowerDeliveryControllerVersionsSnapshot = FrameworkDotnet.Snapshots.FrameworkPowerDeliveryControllerVersionsSnapshot;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcPdControllerVersionsResult
{
    internal readonly ManagedPowerDeliveryControllerVersionsSnapshot GetValueOrThrow()
    {
        if (status.IsFailure)
        {
            throw FrameworkStatusException.GetCorrectException(status);
        }

        return new ManagedPowerDeliveryControllerVersionsSnapshot(
            controller_count,
            controller_0.ToManagedSnapshot(ManagedPowerDeliveryControllerSlot.Right01),
            controller_1.ToManagedSnapshot(ManagedPowerDeliveryControllerSlot.Left23),
            controller_2.ToManagedSnapshot(ManagedPowerDeliveryControllerSlot.Back));
    }
}
