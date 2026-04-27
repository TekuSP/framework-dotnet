using UnitsNet;

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
    /// <param name="appliedDutyCycle">The applied duty cycle.</param>
    public FrameworkSetFanDutyResponse(int fanIndex, Ratio appliedDutyCycle)
    {
        FanIndex = fanIndex;
        AppliedDutyCycle = appliedDutyCycle;
    }

    /// <summary>
    /// Gets the zero-based fan index.
    /// </summary>
    public int FanIndex { get; init; }

    /// <summary>
    /// Gets the applied duty cycle.
    /// </summary>
    public Ratio AppliedDutyCycle { get; init; }
}
