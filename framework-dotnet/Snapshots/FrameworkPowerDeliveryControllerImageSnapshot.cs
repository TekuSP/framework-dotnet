namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents the version pair of a single firmware image stored on a USB Power Delivery controller.
/// </summary>
/// <remarks>Each image carries both a Cypress base version and a Cypress application version.</remarks>
public sealed record FrameworkPowerDeliveryControllerImageSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkPowerDeliveryControllerImageSnapshot"/> class.
    /// </summary>
    /// <param name="baseVersion">The Cypress base version of the image.</param>
    /// <param name="applicationVersion">The Cypress application version of the image.</param>
    public FrameworkPowerDeliveryControllerImageSnapshot(FrameworkPowerDeliveryBaseVersionSnapshot baseVersion, FrameworkPowerDeliveryApplicationVersionSnapshot applicationVersion)
    {
        BaseVersion = baseVersion;
        ApplicationVersion = applicationVersion;
    }

    /// <summary>
    /// Gets the Cypress base version of the image.
    /// </summary>
    public FrameworkPowerDeliveryBaseVersionSnapshot BaseVersion { get; init; }

    /// <summary>
    /// Gets the Cypress application version of the image.
    /// </summary>
    public FrameworkPowerDeliveryApplicationVersionSnapshot ApplicationVersion { get; init; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"Base: {BaseVersion}, App: {ApplicationVersion}";
    }
}
