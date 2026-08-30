using System.Globalization;

using UnitsNet;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents the charge level a paired stylus reports over the touchscreen HID interface.
/// </summary>
/// <remarks>
/// <para>
/// The reading comes from the touchscreen controller rather than the embedded controller, so it is
/// available without an embedded controller connection.
/// </para>
/// <para>
/// A successful read with <see cref="IsPresent"/> set to <see langword="false"/> is a normal outcome and
/// not an error: it means no stylus is paired, or the touchscreen does not report a stylus battery at
/// all. In that case <see cref="ChargeLevel"/> is zero and carries no meaning.
/// </para>
/// </remarks>
public sealed record FrameworkStylusBatterySnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkStylusBatterySnapshot"/> class.
    /// </summary>
    /// <param name="isPresent">A value indicating whether a stylus answered the query.</param>
    /// <param name="chargeLevel">The reported stylus charge level, valid only when <paramref name="isPresent"/> is <see langword="true"/>.</param>
    public FrameworkStylusBatterySnapshot(bool isPresent, Ratio chargeLevel)
    {
        IsPresent = isPresent;
        ChargeLevel = chargeLevel;
    }

    /// <summary>
    /// Gets a value indicating whether a stylus answered the query.
    /// </summary>
    /// <remarks>This flag is authoritative. Read <see cref="ChargeLevel"/> only when it is <see langword="true"/>.</remarks>
    public bool IsPresent { get; init; }

    /// <summary>
    /// Gets the reported stylus charge level.
    /// </summary>
    /// <remarks>
    /// The touchscreen reports a whole percentage between 0 and 100. The value is meaningful only when
    /// <see cref="IsPresent"/> is <see langword="true"/>; otherwise it is zero.
    /// </remarks>
    public Ratio ChargeLevel { get; init; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return IsPresent
            ? $"Stylus Battery: {ChargeLevel.Percent.ToString(CultureInfo.InvariantCulture)}%"
            : "Stylus Battery: Not Present";
    }
}
