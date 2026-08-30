using System.Globalization;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents the Cypress base firmware version of a USB Power Delivery controller image.
/// </summary>
/// <remarks>The native layer formats this version as <c>Major.Minor.Patch.BuildNumber</c>.</remarks>
public sealed record FrameworkPowerDeliveryBaseVersionSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkPowerDeliveryBaseVersionSnapshot"/> class.
    /// </summary>
    /// <param name="major">The major component of the base version.</param>
    /// <param name="minor">The minor component of the base version.</param>
    /// <param name="patch">The patch component of the base version.</param>
    /// <param name="buildNumber">The build number of the base version.</param>
    public FrameworkPowerDeliveryBaseVersionSnapshot(byte major, byte minor, byte patch, ushort buildNumber)
    {
        Major = major;
        Minor = minor;
        Patch = patch;
        BuildNumber = buildNumber;
    }

    /// <summary>
    /// Gets the major component of the base version.
    /// </summary>
    public byte Major { get; init; }

    /// <summary>
    /// Gets the minor component of the base version.
    /// </summary>
    public byte Minor { get; init; }

    /// <summary>
    /// Gets the patch component of the base version.
    /// </summary>
    public byte Patch { get; init; }

    /// <summary>
    /// Gets the build number of the base version.
    /// </summary>
    public ushort BuildNumber { get; init; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{Major.ToString(CultureInfo.InvariantCulture)}.{Minor.ToString(CultureInfo.InvariantCulture)}.{Patch.ToString(CultureInfo.InvariantCulture)}.{BuildNumber.ToString(CultureInfo.InvariantCulture)}";
    }
}
