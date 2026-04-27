using FrameworkDotnet.Exceptions;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkProductNameResult
{
    internal readonly string GetValueOrThrow()
    {
        if (status.IsFailure)
        {
            throw FrameworkStatusException.GetCorrectException(status.code);
        }
        return product_name.ToUtf8StringAndFree();
    }
}
