using FrameworkDotnet.Exceptions;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcAdcResult
{
    /// <summary>
    /// Returns the raw analog-to-digital converter count, or throws when the read failed.
    /// </summary>
    /// <remarks>
    /// The <c>channel</c> field is only an echo of the requested channel and carries no
    /// information the caller does not already have, so it is not surfaced.
    /// </remarks>
    internal readonly int GetValueOrThrow()
    {
        if (status.IsFailure)
        {
            throw FrameworkStatusException.GetCorrectException(status);
        }

        return value;
    }
}
