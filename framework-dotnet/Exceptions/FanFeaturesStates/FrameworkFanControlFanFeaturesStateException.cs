using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.FanFeaturesStates;

/// <summary>
/// Represents a <see cref="FrameworkFanFeaturesState.FanControl"/> fan features state.
/// </summary>
internal class FrameworkFanControlFanFeaturesStateException : FrameworkFanFeaturesStateException
{
    internal FrameworkFanControlFanFeaturesStateException()
        : base(FrameworkFanFeaturesState.FanControl)
    {
    }
}
