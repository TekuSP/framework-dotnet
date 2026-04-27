using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Snapshots;

using UnitsNet;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkTemperatureReading
{
    internal readonly FrameworkTemperatureReading GetValueOrThrow()
    {
        if (state == FrameworkTemperatureState.Ok)
            return this;
        throw FrameworkTemperatureStateException.GetCorrectException(state);
    }

    internal readonly FrameworkTemperatureSnapshot ToManagedSnapshot()
    {
        return new FrameworkTemperatureSnapshot((FrameworkDotnet.Enums.FrameworkTemperatureState)(int)state, Temperature.FromDegreesCelsius(celsius));
    }
}
