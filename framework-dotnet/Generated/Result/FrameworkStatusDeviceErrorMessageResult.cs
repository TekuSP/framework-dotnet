namespace Framework.System.Interop;

internal unsafe partial struct FrameworkStatusDeviceErrorMessageResult
{
    internal readonly string GetValueOrDefault()
    {
        if (status.IsFailure)
        {
            return string.Empty;
        }

        return message.ToUtf8StringAndFree();
    }
}
