using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.TemperatureStates;

/// <summary>
/// Represents a <see cref="FrameworkTemperatureState.NotPowered"/> temperature state.
/// </summary>
internal class FrameworkNotPoweredTemperatureStateException : FrameworkTemperatureStateException
{
    internal FrameworkNotPoweredTemperatureStateException() : base(FrameworkTemperatureState.NotPowered)
    {
    }
}
