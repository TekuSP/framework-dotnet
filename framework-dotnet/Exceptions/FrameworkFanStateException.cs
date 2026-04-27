using System;

using Framework.System.Interop;

using FrameworkDotnet.Exceptions.FanStates;

namespace FrameworkDotnet.Exceptions;

/// <summary>
/// Represents an exception mapped directly from a <see cref="FrameworkFanState"/> value.
/// </summary>
internal abstract class FrameworkFanStateException : FrameworkException
{
    internal FrameworkFanStateException(FrameworkFanState fanState)
        : base()
    {
        FanState = fanState;
    }

    internal FrameworkFanState FanState { get; }

    internal static FrameworkFanStateException GetCorrectException(FrameworkFanState statusCode)
    {
        switch (statusCode)
        {
            case FrameworkFanState.Ok:
                throw new ArgumentException("Status code indicates success, no exception should be thrown.", nameof(statusCode));
            case FrameworkFanState.NotPresent:
                return new FrameworkNotPresentFanStateException();
            case FrameworkFanState.Stalled:
                return new FrameworkStalledFanStateException();
            default:
                throw new ArgumentOutOfRangeException(nameof(statusCode), statusCode, "Unhandled status code.");
        }
    }
}
