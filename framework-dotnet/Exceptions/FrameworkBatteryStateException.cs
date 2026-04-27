using System;

using Framework.System.Interop;

using FrameworkDotnet.Exceptions.BatteryStates;

namespace FrameworkDotnet.Exceptions;

/// <summary>
/// Represents an exception mapped directly from a <see cref="FrameworkBatteryState"/> value.
/// </summary>
internal abstract class FrameworkBatteryStateException : FrameworkException
{
    internal FrameworkBatteryStateException(FrameworkBatteryState batteryState)
        : base()
    {
        BatteryState = batteryState;
    }

    internal FrameworkBatteryState BatteryState { get; }

    internal static FrameworkBatteryStateException GetCorrectException(FrameworkBatteryState statusCode)
    {
        switch (statusCode)
        {
            case FrameworkBatteryState.NotPresent:
                return new FrameworkNotPresentBatteryStateException();
            case FrameworkBatteryState.Critical:
                return new FrameworkCriticalBatteryStateException();

            case FrameworkBatteryState.Idle:
            case FrameworkBatteryState.Charging:
            case FrameworkBatteryState.Discharging:
            case FrameworkBatteryState.ChargingAndDischarging:
                throw new ArgumentException("Status code indicates success, no exception should be thrown.", nameof(statusCode));

            default:
                throw new ArgumentOutOfRangeException(nameof(statusCode), statusCode, "Unhandled status code.");
        }
    }
}
