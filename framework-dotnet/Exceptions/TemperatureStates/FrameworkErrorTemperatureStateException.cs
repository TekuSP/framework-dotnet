using Framework.System.Interop;

using FrameworkDotnet.Enums;

namespace FrameworkDotnet.Exceptions.TemperatureStates;

/// <summary>
/// Represents a <see cref="FrameworkTemperatureState.Error"/> temperature state.
/// </summary>
internal class FrameworkErrorTemperatureStateException : FrameworkTemperatureStateException
{
    internal FrameworkErrorTemperatureStateException() : base(FrameworkTemperatureState.Error)
    {
    }
}
