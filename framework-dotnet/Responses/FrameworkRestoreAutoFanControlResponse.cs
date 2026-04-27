namespace FrameworkDotnet.Responses;

/// <summary>
/// Represents the result of restoring automatic fan control.
/// </summary>
public sealed record FrameworkRestoreAutoFanControlResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkRestoreAutoFanControlResponse"/> class.
    /// </summary>
    /// <param name="fanIndex">The zero-based fan index.</param>
    public FrameworkRestoreAutoFanControlResponse(int fanIndex)
    {
        FanIndex = fanIndex;
    }

    /// <summary>
    /// Gets the zero-based fan index.
    /// </summary>
    public int FanIndex { get; init; }
}
