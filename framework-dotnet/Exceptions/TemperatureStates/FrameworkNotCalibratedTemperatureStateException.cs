using Framework.System.Interop;

using FrameworkDotnet.Enums;

namespace FrameworkDotnet.Exceptions.TemperatureStates;

/// <summary>
/// Represents a <see cref="FrameworkTemperatureState.NotCalibrated"/> temperature state.
/// </summary>
internal class FrameworkNotCalibratedTemperatureStateException : FrameworkTemperatureStateException
{
    internal FrameworkNotCalibratedTemperatureStateException(): base(FrameworkTemperatureState.NotCalibrated)
    {
    }
}
