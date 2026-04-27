namespace Framework.System.Interop;

internal unsafe partial struct FrameworkStatusInvalidFanIndexRecord
{
    internal readonly int FanIndex => fan_index;

    internal readonly string Description => $"Invalid fan index: {FanIndex}";
}
