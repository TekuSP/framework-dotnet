using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.TemperatureStates;

/// <summary>
/// Represents a <see cref="FrameworkTemperatureState.NotPresent"/> temperature state.
/// </summary>
public class FrameworkNotPresentTemperatureStateException : FrameworkTemperatureStateException
{
    internal FrameworkNotPresentTemperatureStateException() : base(FrameworkTemperatureState.NotPresent)
    {
    }
}
