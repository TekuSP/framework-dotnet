using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.TemperatureStates;

/// <summary>
/// Represents a <see cref="FrameworkTemperatureState.NotPowered"/> temperature state.
/// </summary>
public class FrameworkNotPoweredTemperatureStateException : FrameworkTemperatureStateException
{
    internal FrameworkNotPoweredTemperatureStateException() : base(FrameworkTemperatureState.NotPowered)
    {
    }
}
