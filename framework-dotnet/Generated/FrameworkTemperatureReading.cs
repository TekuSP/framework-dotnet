using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Snapshots;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkTemperatureReading
{
    internal FrameworkTemperatureReading GetValueOrThrow()
    {
        if (state == FrameworkTemperatureState.Ok)
            return this;
        throw FrameworkTemperatureStateException.GetCorrectException(state);
    }

    internal FrameworkTemperatureSnapshot ToManagedSnapshot()
    {
        return new FrameworkTemperatureSnapshot((global::FrameworkDotnet.Enums.FrameworkTemperatureState)(int)state, celsius);
    }
}
