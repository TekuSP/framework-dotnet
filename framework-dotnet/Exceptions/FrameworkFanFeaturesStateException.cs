using System;

using Framework.System.Interop;

using FrameworkDotnet.Exceptions.FanFeaturesStates;

namespace FrameworkDotnet.Exceptions;

/// <summary>
/// Represents an exception mapped directly from a <see cref="FrameworkFanFeaturesState"/> value.
/// </summary>
internal abstract class FrameworkFanFeaturesStateException : FrameworkException
{
    internal FrameworkFanFeaturesStateException(FrameworkFanFeaturesState fanFeaturesState)
        : base()
    {
        FanFeaturesState = fanFeaturesState;
    }

    internal FrameworkFanFeaturesState FanFeaturesState { get; }

    internal static FrameworkFanFeaturesStateException GetCorrectException(FrameworkFanFeaturesState statusCode)
    {
        switch (statusCode)
        {
            case FrameworkFanFeaturesState.None:
                return new FrameworkNoFanFeaturesStateException();
            case FrameworkFanFeaturesState.FanControl:
                return new FrameworkFanControlFanFeaturesStateException();
            case FrameworkFanFeaturesState.ThermalReporting:
                return new FrameworkThermalReportingFanFeaturesStateException();
            case FrameworkFanFeaturesState.All:
                throw new ArgumentException("Status code indicates success, no exception should be thrown.", nameof(statusCode));
            default:
                throw new ArgumentOutOfRangeException(nameof(statusCode), statusCode, "Unhandled status code.");
        }
    }
}
