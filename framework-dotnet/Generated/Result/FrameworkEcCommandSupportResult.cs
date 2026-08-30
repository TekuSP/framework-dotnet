using FrameworkDotnet.Exceptions;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcCommandSupportResult
{
    /// <summary>
    /// Returns whether the embedded controller implements the probed command at the probed
    /// version, or throws when the probe itself failed.
    /// </summary>
    /// <remarks>
    /// The <c>command</c> and <c>version</c> fields are only echoes of the probe arguments, so
    /// they are not surfaced.
    /// </remarks>
    internal readonly bool GetValueOrThrow()
    {
        if (status.IsFailure)
        {
            throw FrameworkStatusException.GetCorrectException(status);
        }

        return supported != 0;
    }
}
