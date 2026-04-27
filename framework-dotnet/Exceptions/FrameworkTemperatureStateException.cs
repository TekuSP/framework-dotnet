using System;

using Framework.System.Interop;

using FrameworkDotnet.Exceptions.TemperatureStates;

namespace FrameworkDotnet.Exceptions;

/// <summary>
/// Represents an exception mapped directly from a <see cref="FrameworkTemperatureState"/> value.
/// </summary>
internal abstract class FrameworkTemperatureStateException : FrameworkException
{
    internal FrameworkTemperatureStateException(FrameworkTemperatureState temperatureState) : base()
    {
        TemperatureState = temperatureState;
    }

    internal FrameworkTemperatureState TemperatureState { get; }

    internal static FrameworkTemperatureStateException GetCorrectException(FrameworkTemperatureState statusCode)
    {
        switch (statusCode)
        {
            case FrameworkTemperatureState.Ok:
                throw new ArgumentException("Status code indicates success, no exception should be thrown.", nameof(statusCode));
            case FrameworkTemperatureState.NotPresent:
                return new FrameworkNotPresentTemperatureStateException();
            case FrameworkTemperatureState.Error:
                return new FrameworkErrorTemperatureStateException();
            case FrameworkTemperatureState.NotPowered:
                return new FrameworkNotPoweredTemperatureStateException();
            case FrameworkTemperatureState.NotCalibrated:
                return new FrameworkNotCalibratedTemperatureStateException();
            default:
                throw new ArgumentOutOfRangeException(nameof(statusCode), statusCode, "Unhandled status code.");
        }
    }
}
