using FrameworkDotnet.Exceptions;

using ManagedTemperatureReading = FrameworkDotnet.Snapshots.FrameworkTemperatureSnapshot;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkTemperatureReading
{
    internal FrameworkTemperatureReading GetValueOrThrow()
    {
        if (state == FrameworkTemperatureState.Ok)
            return this;
        throw FrameworkTemperatureStateException.GetCorrectException(state);
    }

    internal ManagedTemperatureReading ToManagedSnapshot()
    {
        return new ManagedTemperatureReading(celsius);
    }
}
