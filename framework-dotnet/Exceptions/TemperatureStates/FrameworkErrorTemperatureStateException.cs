using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.TemperatureStates;

/// <summary>
/// Represents a <see cref="FrameworkTemperatureState.Error"/> temperature state.
/// </summary>
public class FrameworkErrorTemperatureStateException : FrameworkTemperatureStateException
{
    internal FrameworkErrorTemperatureStateException() : base(FrameworkTemperatureState.Error)
    {
    }
}
