namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents the identity strings an NVMe drive reports through its Identify Controller data.
/// </summary>
/// <remarks>
/// Both strings come straight from the drive's Identify Controller structure and are already trimmed of
/// the padding spaces the NVMe specification mandates. Either can be empty when the drive leaves the
/// corresponding field blank.
/// </remarks>
public sealed record FrameworkNvmeVersionSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkNvmeVersionSnapshot"/> class.
    /// </summary>
    /// <param name="modelNumber">The model number the drive reports.</param>
    /// <param name="firmwareVersion">The firmware revision the drive reports.</param>
    public FrameworkNvmeVersionSnapshot(string modelNumber, string firmwareVersion)
    {
        ModelNumber = modelNumber;
        FirmwareVersion = firmwareVersion;
    }

    /// <summary>
    /// Gets the model number the drive reports.
    /// </summary>
    public string ModelNumber { get; init; }

    /// <summary>
    /// Gets the firmware revision the drive reports.
    /// </summary>
    /// <remarks>
    /// This is a vendor-defined revision string, not a dotted version number, so it is surfaced verbatim
    /// rather than parsed. Compare it for equality; do not attempt an ordering comparison.
    /// </remarks>
    public string FirmwareVersion { get; init; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"NVMe Drive: Model: {ModelNumber}, Firmware: {FirmwareVersion}";
    }
}
