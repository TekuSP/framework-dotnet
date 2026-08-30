using FrameworkDotnet.Exceptions;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcBatteryAuthResult
{
    /// <summary>
    /// Returns whether the pack answered the challenge correctly, throwing only when the exchange itself failed.
    /// </summary>
    /// <remarks>
    /// A success status with <c>authenticated == 0</c> means the pack answered and failed the challenge. That is a
    /// legitimate negative answer, not an error, so it is returned as <see langword="false"/>.
    /// </remarks>
    internal readonly bool GetValueOrThrow()
    {
        if (status.IsFailure)
        {
            throw FrameworkStatusException.GetCorrectException(status);
        }

        return authenticated != 0;
    }
}
