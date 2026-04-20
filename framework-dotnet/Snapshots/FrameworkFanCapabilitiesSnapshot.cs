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
    /// <param name="supportsFanControl">A value indicating whether manual fan control is supported.</param>
    /// <param name="supportsThermalReporting">A value indicating whether thermal reporting is supported.</param>
    public FrameworkFanCapabilitiesSnapshot(byte fanCount, bool supportsFanControl, bool supportsThermalReporting)
    {
        FanCount = fanCount;
        SupportsFanControl = supportsFanControl;
        SupportsThermalReporting = supportsThermalReporting;
    }

    /// <summary>
    /// Gets the number of fans reported by the device.
    /// </summary>
    public byte FanCount { get; init; }

    /// <summary>
    /// Gets a value indicating whether manual fan control is supported.
    /// </summary>
    public bool SupportsFanControl { get; init; }

    /// <summary>
    /// Gets a value indicating whether thermal reporting is supported.
    /// </summary>
    public bool SupportsThermalReporting { get; init; }
}
