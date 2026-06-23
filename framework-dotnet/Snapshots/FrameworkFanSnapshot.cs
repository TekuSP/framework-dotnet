using System.Globalization;

using FrameworkDotnet.Enums;

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
    /// <param name="name">The platform-specific role name for this fan slot.</param>
    public FrameworkFanSnapshot(FrameworkFanState fanState, RotationalSpeed speed, FrameworkFanName name)
    {
        FanState = fanState;
        Speed = speed;
        Name = name;
    }

    /// <summary>
    /// Gets the fan reading state.
    /// </summary>
    public FrameworkFanState FanState { get; init; }

    /// <summary>
    /// Gets the fan speed.
    /// </summary>
    public RotationalSpeed Speed { get; init; }

    /// <summary>
    /// Gets the platform-specific role name for this fan slot.
    /// </summary>
    public FrameworkFanName Name { get; init; }

    public override string ToString()
    {
        return $"Fan Snapshot: Fan State: {FanState}, Speed: {Speed.ToString(CultureInfo.InvariantCulture)}, Name: {Name}";
    }
}
