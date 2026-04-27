using FrameworkDotnet.Exceptions.TemperatureStates;

using ManagedPowerSnapshot = FrameworkDotnet.Snapshots.FrameworkPowerSnapshot;
namespace Framework.System.Interop;

internal unsafe partial struct FrameworkPowerSnapshot
{
    internal FrameworkPowerSnapshot GetValueOrThrow()
    {
        if (power_source_state == FrameworkPowerSourceState.None)
            throw new FrameworkNotPoweredTemperatureStateException();
        return this;
    }
    internal ManagedPowerSnapshot ToManagedSnapshot()
    {
        return new ManagedPowerSnapshot((FrameworkDotnet.Enums.FrameworkPowerSourceState)(int)power_source_state, battery_count, battery_0.GetValueOrThrow().ToManagedSnapshot());
    }
}
