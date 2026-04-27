using FrameworkDotnet.Enums;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents an embedded controller flash snapshot.
/// </summary>
public sealed record FrameworkEcFlashSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkEcFlashSnapshot"/> class.
    /// </summary>
    /// <param name="currentImage">The currently active firmware image.</param>
    /// <param name="roVersion">The read-only firmware version.</param>
    /// <param name="rwVersion">The read-write firmware version.</param>
    public FrameworkEcFlashSnapshot(FrameworkEcCurrentImage currentImage, string roVersion, string rwVersion)
    {
        CurrentImage = currentImage;
        RoVersion = roVersion;
        RwVersion = rwVersion;
    }

    /// <summary>
    /// Gets the currently active firmware image.
    /// </summary>
    public FrameworkEcCurrentImage CurrentImage { get; init; }

    /// <summary>
    /// Gets the read-only firmware version.
    /// </summary>
    public string RoVersion { get; init; }

    /// <summary>
    /// Gets the read-write firmware version.
    /// </summary>
    public string RwVersion { get; init; }

    public override string ToString()
    {
        return $"EC Flash Snapshot: Current Image: {CurrentImage}, RO Version: {RoVersion}, RW Version: {RwVersion}";
    }
}
