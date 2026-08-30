using System;

using FrameworkDotnet.Exceptions;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcBatteryCutoffResult
{
    internal readonly FrameworkBatteryCutoffState GetValueOrThrow()
    {
        if (status.IsFailure)
        {
            throw FrameworkStatusException.GetCorrectException(status);
        }

        switch (state)
        {
            case FrameworkBatteryCutoffState.Unknown:
            case FrameworkBatteryCutoffState.NotCutOff:
            case FrameworkBatteryCutoffState.CutOff:
                return state;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, "Unhandled battery cutoff state.");
        }
    }
}
