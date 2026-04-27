using FrameworkDotnet.Exceptions;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkStatusDescriptionResult
{
    internal readonly string GetValueOrDefault()
    {
        if (status.IsFailure)
        {
            return string.Empty;
        }

        return description.ToUtf8StringAndFree();
    }
}
