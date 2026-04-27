using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.FanStates;

/// <summary>
/// Represents a <see cref="FrameworkFanState.NotPresent"/> fan state.
/// </summary>
public class FrameworkNotPresentFanStateException : FrameworkFanStateException
{
    internal FrameworkNotPresentFanStateException() : base(FrameworkFanState.NotPresent)
    {
    }
}
