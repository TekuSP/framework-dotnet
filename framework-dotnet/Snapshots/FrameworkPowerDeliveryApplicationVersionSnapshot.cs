using System.Globalization;

using FrameworkDotnet.Enums;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents the Cypress application firmware version of a USB Power Delivery controller image.
/// </summary>
/// <remarks>The native layer formats this version as <c>Major.Minor.Circuit</c>.</remarks>
public sealed record FrameworkPowerDeliveryApplicationVersionSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkPowerDeliveryApplicationVersionSnapshot"/> class.
    /// </summary>
    /// <param name="application">The application the firmware image targets.</param>
    /// <param name="major">The major component of the application version.</param>
    /// <param name="minor">The minor component of the application version.</param>
    /// <param name="circuit">The circuit component of the application version.</param>
    public FrameworkPowerDeliveryApplicationVersionSnapshot(FrameworkPdApplication application, byte major, byte minor, byte circuit)
    {
        Application = application;
        Major = major;
        Minor = minor;
        Circuit = circuit;
    }

    /// <summary>
    /// Gets the application the firmware image targets.
    /// </summary>
    public FrameworkPdApplication Application { get; init; }

    /// <summary>
    /// Gets the major component of the application version.
    /// </summary>
    public byte Major { get; init; }

    /// <summary>
    /// Gets the minor component of the application version.
    /// </summary>
    public byte Minor { get; init; }

    /// <summary>
    /// Gets the circuit component of the application version.
    /// </summary>
    public byte Circuit { get; init; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{Major.ToString(CultureInfo.InvariantCulture)}.{Minor.ToString(CultureInfo.InvariantCulture)}.{Circuit.ToString(CultureInfo.InvariantCulture)} ({Application})";
    }
}
