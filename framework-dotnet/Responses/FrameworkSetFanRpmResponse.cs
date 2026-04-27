using UnitsNet;

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
    /// <param name="appliedSpeed">The applied fan speed target.</param>
    public FrameworkSetFanRpmResponse(int fanIndex, RotationalSpeed appliedSpeed)
    {
        FanIndex = fanIndex;
        AppliedSpeed = appliedSpeed;
    }

    /// <summary>
    /// Gets the zero-based fan index.
    /// </summary>
    public int FanIndex { get; init; }

    /// <summary>
    /// Gets the applied fan speed target.
    /// </summary>
    public RotationalSpeed AppliedSpeed { get; init; }
}
