using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Snapshots;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcModuleInventoryResult
{
    internal readonly FrameworkModuleInventorySnapshot GetValueOrThrow()
    {
        if (status.IsFailure)
        {
            throw FrameworkStatusException.GetCorrectException(status);
        }

        var value = inventory;
        return value.ToManagedSnapshot();
    }
}
