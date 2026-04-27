namespace Framework.System.Interop;

internal unsafe partial struct FrameworkStatusUnknownEcResponseCodeRecord
{
    internal readonly int ResponseCode => response_code;

    internal readonly string Description => $"Unknown response code: {ResponseCode}";
}
