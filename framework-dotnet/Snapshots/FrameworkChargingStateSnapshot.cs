namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents the charging state reported by the embedded controller.
/// </summary>
public sealed record FrameworkChargingStateSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkChargingStateSnapshot"/> class.
    /// </summary>
    /// <param name="isCharging">A value indicating whether the battery is currently being charged.</param>
    /// <param name="isAcPresent">A value indicating whether an external power adapter is attached.</param>
    public FrameworkChargingStateSnapshot(bool isCharging, bool isAcPresent)
    {
        IsCharging = isCharging;
        IsAcPresent = isAcPresent;
    }

    /// <summary>
    /// Gets a value indicating whether the battery is currently being charged.
    /// </summary>
    public bool IsCharging { get; init; }

    /// <summary>
    /// Gets a value indicating whether an external power adapter is attached.
    /// </summary>
    /// <remarks>
    /// An adapter can be attached without the battery charging, for example when the pack is already full or a charge limit is active.
    /// </remarks>
    public bool IsAcPresent { get; init; }

    public override string ToString()
    {
        return $"Charging State: Charging: {IsCharging}, AC Present: {IsAcPresent}";
    }
}
