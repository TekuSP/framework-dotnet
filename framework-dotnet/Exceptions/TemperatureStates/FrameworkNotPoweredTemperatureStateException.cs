using Framework.System.Interop;

using FrameworkDotnet.Enums;

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
