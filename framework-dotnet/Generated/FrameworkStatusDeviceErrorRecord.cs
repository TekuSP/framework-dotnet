namespace Framework.System.Interop;

internal unsafe partial struct FrameworkStatusDeviceErrorRecord
{
    internal readonly int MessageToken => message_token;

    internal readonly string Description => $"Device error message token: {MessageToken}";
}
