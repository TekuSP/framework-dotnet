namespace Framework.System.Interop;

internal unsafe partial struct FrameworkStatusEcResponseRecord
{
    internal readonly FrameworkEcResponseDetail Response => response;

    internal readonly string Description => $"EC response: {Response}";
}
