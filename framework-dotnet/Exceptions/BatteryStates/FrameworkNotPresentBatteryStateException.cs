using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.BatteryStates;

/// <summary>
/// Represents a <see cref="FrameworkBatteryState.NotPresent"/> battery state.
/// </summary>
public class FrameworkNotPresentBatteryStateException : FrameworkBatteryStateException
{
    internal FrameworkNotPresentBatteryStateException()
        : base(FrameworkBatteryState.NotPresent)
    {
    }
}
