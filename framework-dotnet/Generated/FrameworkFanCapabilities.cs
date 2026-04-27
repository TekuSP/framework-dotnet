using System.Runtime.Intrinsics.Arm;

using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Snapshots;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkFanCapabilities
{
    internal FrameworkFanCapabilities GetValueOrThrow()
    {
        if (features == FrameworkFanFeaturesState.All)
            return this;    
        throw FrameworkFanFeaturesStateException.GetCorrectException(features);
    }

    internal FrameworkFanCapabilitiesSnapshot ToManagedSnapshot()
    {
        return new FrameworkFanCapabilitiesSnapshot(fan_count, (FrameworkDotnet.Enums.FrameworkFanFeaturesState)(int)features);
    }
}
