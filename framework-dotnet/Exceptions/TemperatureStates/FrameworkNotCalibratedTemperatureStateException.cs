using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions.TemperatureStates;

/// <summary>
/// Represents a <see cref="FrameworkTemperatureState.NotCalibrated"/> temperature state.
/// </summary>
public class FrameworkNotCalibratedTemperatureStateException : FrameworkTemperatureStateException
{
    internal FrameworkNotCalibratedTemperatureStateException()
        : base(FrameworkTemperatureState.NotCalibrated)
    {
    }
}
