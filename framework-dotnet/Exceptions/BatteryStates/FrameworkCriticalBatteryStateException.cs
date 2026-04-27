using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.BatteryStates;

/// <summary>
/// Represents a <see cref="FrameworkBatteryState.Critical"/> battery state.
/// </summary>
public class FrameworkCriticalBatteryStateException : FrameworkBatteryStateException
{
    internal FrameworkCriticalBatteryStateException()
        : base(FrameworkBatteryState.Critical)
    {
    }
}
