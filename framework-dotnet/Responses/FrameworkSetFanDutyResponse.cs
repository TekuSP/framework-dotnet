namespace FrameworkDotnet.Responses;

/// <summary>
/// Represents the result of setting a fan duty cycle.
/// </summary>
public sealed record FrameworkSetFanDutyResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkSetFanDutyResponse"/> class.
    /// </summary>
    /// <param name="fanIndex">The zero-based fan index.</param>
    /// <param name="percent">The applied duty cycle percentage.</param>
    public FrameworkSetFanDutyResponse(int fanIndex, uint percent)
    {
        FanIndex = fanIndex;
        Percent = percent;
    }

    /// <summary>
    /// Gets the zero-based fan index.
    /// </summary>
    public int FanIndex { get; init; }

    /// <summary>
    /// Gets the applied duty cycle percentage.
    /// </summary>
    public uint Percent { get; init; }
}
