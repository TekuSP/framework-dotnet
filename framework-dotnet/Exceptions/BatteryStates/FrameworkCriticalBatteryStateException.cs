using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.BatteryStates;

/// <summary>
/// Represents a <see cref="FrameworkBatteryState.Critical"/> battery state.
/// </summary>
internal class FrameworkCriticalBatteryStateException : FrameworkBatteryStateException
{
    internal FrameworkCriticalBatteryStateException()
        : base(FrameworkBatteryState.Critical)
    {
    }
}
