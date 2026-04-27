using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Snapshots;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkFanReading
{
    internal FrameworkFanReading GetValueOrThrow()
    {
        if (state == FrameworkFanState.Ok)
            return this;
        throw FrameworkFanStateException.GetCorrectException(state);
    }

    internal FrameworkFanSnapshot ToManagedSnapshot()
    {
        return new FrameworkFanSnapshot((FrameworkDotnet.Enums.FrameworkFanState)(int)state, rpm);
    }
}
