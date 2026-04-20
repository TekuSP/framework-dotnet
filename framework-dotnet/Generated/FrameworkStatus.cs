using System;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkStatus
{
    internal readonly bool IsSuccess => code == FrameworkStatusCode.Success;

    internal readonly bool IsFailure => !IsSuccess;

    internal readonly void ThrowIfError()
    {
        if (IsFailure)
        {
            throw new InvalidOperationException($"Framework interop call failed with {code} (detail: {detail}).");
        }
    }
}
