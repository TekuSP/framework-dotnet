using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Snapshots;

using UnitsNet;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkFanReading
{
    internal readonly FrameworkFanReading GetValueOrThrow()
    {
        if (state == FrameworkFanState.Ok)
            return this;
        throw FrameworkFanStateException.GetCorrectException(state);
    }

    internal readonly FrameworkFanSnapshot ToManagedSnapshot()
    {
        return new FrameworkFanSnapshot((FrameworkDotnet.Enums.FrameworkFanState)(int)state, RotationalSpeed.FromRevolutionsPerMinute(rpm));
    }
}
