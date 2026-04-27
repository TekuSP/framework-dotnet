using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.FanStates;

/// <summary>
/// Represents a <see cref="FrameworkFanState.Stalled"/> fan state.
/// </summary>
public class FrameworkStalledFanStateException : FrameworkFanStateException
{
    internal FrameworkStalledFanStateException() : base(FrameworkFanState.Stalled)
    {
    }
}
