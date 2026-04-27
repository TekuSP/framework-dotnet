namespace FrameworkDotnet.Responses;

/// <summary>
/// Represents the result of setting a fan RPM target.
/// </summary>
public sealed record FrameworkSetFanRpmResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkSetFanRpmResponse"/> class.
    /// </summary>
    /// <param name="fanIndex">The zero-based fan index.</param>
    /// <param name="rpm">The applied RPM target.</param>
    public FrameworkSetFanRpmResponse(int fanIndex, uint rpm)
    {
        FanIndex = fanIndex;
        Rpm = rpm;
    }

    /// <summary>
    /// Gets the zero-based fan index.
    /// </summary>
    public int FanIndex { get; init; }

    /// <summary>
    /// Gets the applied RPM target.
    /// </summary>
    public uint Rpm { get; init; }
}
