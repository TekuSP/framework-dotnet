using System;

using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Snapshots;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkFanCapabilities
{
    internal readonly FrameworkFanCapabilities GetValueOrThrow()
    {
        switch (features)
        {
            case FrameworkFanFeaturesState.None:
            case FrameworkFanFeaturesState.FanControl:
            case FrameworkFanFeaturesState.ThermalReporting:
            case FrameworkFanFeaturesState.All:
                return this;
            default:
                throw new ArgumentOutOfRangeException(nameof(features), features, "Unhandled fan features state.");
        }
    }

    internal readonly FrameworkFanCapabilitiesSnapshot ToManagedSnapshot()
    {
        return new FrameworkFanCapabilitiesSnapshot(fan_count, (FrameworkDotnet.Enums.FrameworkFanFeaturesState)(int)features);
    }
}
