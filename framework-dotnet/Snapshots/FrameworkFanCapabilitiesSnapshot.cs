using FrameworkDotnet.Enums;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents a fan capabilities snapshot.
/// </summary>
public sealed record FrameworkFanCapabilitiesSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkFanCapabilitiesSnapshot"/> class.
    /// </summary>
    /// <param name="fanCount">The number of fans reported by the device.</param>
    /// <param name="features">The supported fan features.</param>
    public FrameworkFanCapabilitiesSnapshot(byte fanCount, FrameworkFanFeaturesState features)
    {
        FanCount = fanCount;
        Features = features;
    }

    /// <summary>
    /// Gets the number of fans reported by the device.
    /// </summary>
    public byte FanCount { get; init; }

    /// <summary>
    /// Gets the supported fan features.
    /// </summary>
    public FrameworkFanFeaturesState Features { get; init; }
}
