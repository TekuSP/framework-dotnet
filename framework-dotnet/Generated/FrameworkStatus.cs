using System;

using FrameworkDotnet.Exceptions;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkStatus
{
    internal readonly bool IsSuccess => code == FrameworkStatusCode.Success;

    internal readonly bool IsFailure => !IsSuccess;

    internal readonly void ThrowIfError()
    {
        if (IsFailure)
        {
            throw new FrameworkInteropException(code, detail);
        }
    }
}
