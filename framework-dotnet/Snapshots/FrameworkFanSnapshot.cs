using FrameworkDotnet.Enums;

using System.Globalization;

using UnitsNet;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents a fan reading from the EC.
/// </summary>
public sealed class FrameworkFanSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkFanSnapshot"/> class.
    /// </summary>
    /// <param name="fanState">The fan reading state.</param>
    /// <param name="speed">The fan speed.</param>
    public FrameworkFanSnapshot(FrameworkFanState fanState, RotationalSpeed speed)
    {
        FanState = fanState;
        Speed = speed;
    }

    /// <summary>
    /// Gets the fan reading state.
    /// </summary>
    public FrameworkFanState FanState { get; init; }

    /// <summary>
    /// Gets the fan speed.
    /// </summary>
    public RotationalSpeed Speed { get; init; }

    public override string ToString()
    {
        return $"Fan Snapshot: Fan State: {FanState}, Speed: {Speed.ToString(CultureInfo.InvariantCulture)}";
    }
}
