using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkStatus
{
    internal readonly bool IsSuccess => code == FrameworkStatusCode.Success;

    internal readonly bool IsFailure => !IsSuccess;
}
