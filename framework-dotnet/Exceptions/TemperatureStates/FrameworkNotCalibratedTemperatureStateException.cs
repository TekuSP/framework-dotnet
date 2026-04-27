using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.TemperatureStates;

/// <summary>
/// Represents a <see cref="FrameworkTemperatureState.NotCalibrated"/> temperature state.
/// </summary>
internal class FrameworkNotCalibratedTemperatureStateException : FrameworkTemperatureStateException
{
    internal FrameworkNotCalibratedTemperatureStateException()
        : base(FrameworkTemperatureState.NotCalibrated)
    {
    }
}
