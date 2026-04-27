using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.FanFeaturesStates;

/// <summary>
/// Represents a <see cref="FrameworkFanFeaturesState.ThermalReporting"/> fan features state.
/// </summary>
internal class FrameworkThermalReportingFanFeaturesStateException : FrameworkFanFeaturesStateException
{
    internal FrameworkThermalReportingFanFeaturesStateException()
        : base(FrameworkFanFeaturesState.ThermalReporting)
    {
    }
}
