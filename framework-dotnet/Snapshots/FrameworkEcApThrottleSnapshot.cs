namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents whether the embedded controller is throttling the application processor for
/// thermal reasons.
/// </summary>
public sealed record FrameworkEcApThrottleSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkEcApThrottleSnapshot"/> class.
    /// </summary>
    /// <param name="softThrottled">A value indicating whether the application processor is soft throttled.</param>
    /// <param name="hardThrottled">A value indicating whether the application processor is hard throttled.</param>
    public FrameworkEcApThrottleSnapshot(bool softThrottled, bool hardThrottled)
    {
        SoftThrottled = softThrottled;
        HardThrottled = hardThrottled;
    }

    /// <summary>
    /// Gets a value indicating whether the application processor is soft throttled.
    /// </summary>
    /// <remarks>
    /// Soft throttling asks the operating system to reduce demand and is the gentler of the two
    /// responses.
    /// </remarks>
    public bool SoftThrottled { get; init; }

    /// <summary>
    /// Gets a value indicating whether the application processor is hard throttled.
    /// </summary>
    /// <remarks>
    /// Hard throttling is applied by the controller itself, without the operating system's
    /// cooperation, and indicates a more severe thermal condition than <see cref="SoftThrottled"/>.
    /// </remarks>
    public bool HardThrottled { get; init; }

    /// <summary>
    /// Gets a value indicating whether the application processor is throttled in either way.
    /// </summary>
    public bool Throttled => SoftThrottled || HardThrottled;

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"EC AP Throttle: Soft Throttled: {SoftThrottled}, Hard Throttled: {HardThrottled}";
    }
}
