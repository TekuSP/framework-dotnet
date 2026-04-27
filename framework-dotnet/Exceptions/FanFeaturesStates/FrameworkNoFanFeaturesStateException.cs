using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.FanFeaturesStates;

/// <summary>
/// Represents a <see cref="FrameworkFanFeaturesState.None"/> fan features state.
/// </summary>
internal class FrameworkNoFanFeaturesStateException : FrameworkFanFeaturesStateException
{
    internal FrameworkNoFanFeaturesStateException()
        : base(FrameworkFanFeaturesState.None)
    {
    }
}
