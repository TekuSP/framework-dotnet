using System.Globalization;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents the live positions of the physical switches the embedded controller monitors.
/// </summary>
public sealed record FrameworkEcSwitchesSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkEcSwitchesSnapshot"/> class.
    /// </summary>
    /// <param name="rawSwitchByte">The raw switch byte as read from the memory-mapped region.</param>
    /// <param name="lidOpen">A value indicating whether the lid is open.</param>
    /// <param name="powerButtonPressed">A value indicating whether the power button is currently held down.</param>
    /// <param name="writeProtectDisabled">A value indicating whether firmware write protect is disabled.</param>
    /// <param name="dedicatedRecovery">A value indicating whether the dedicated recovery switch is asserted.</param>
    public FrameworkEcSwitchesSnapshot(byte rawSwitchByte, bool lidOpen, bool powerButtonPressed, bool writeProtectDisabled, bool dedicatedRecovery)
    {
        RawSwitchByte = rawSwitchByte;
        LidOpen = lidOpen;
        PowerButtonPressed = powerButtonPressed;
        WriteProtectDisabled = writeProtectDisabled;
        DedicatedRecovery = dedicatedRecovery;
    }

    /// <summary>
    /// Gets the raw switch byte as read from the memory-mapped region.
    /// </summary>
    /// <remarks>
    /// Exposed for callers that need bits the managed surface does not name. Prefer the named
    /// properties wherever they cover the bit of interest.
    /// </remarks>
    public byte RawSwitchByte { get; init; }

    /// <summary>
    /// Gets a value indicating whether the lid is open.
    /// </summary>
    public bool LidOpen { get; init; }

    /// <summary>
    /// Gets a value indicating whether the power button is currently held down.
    /// </summary>
    public bool PowerButtonPressed { get; init; }

    /// <summary>
    /// Gets a value indicating whether firmware write protect is disabled.
    /// </summary>
    /// <remarks>
    /// The underlying hardware bit has inverted sense: it is set when write protect is
    /// <em>disabled</em>. This property preserves that sense; use <see cref="WriteProtected"/>
    /// for the positive reading.
    /// </remarks>
    public bool WriteProtectDisabled { get; init; }

    /// <summary>
    /// Gets a value indicating whether the dedicated recovery switch is asserted.
    /// </summary>
    public bool DedicatedRecovery { get; init; }

    /// <summary>
    /// Gets a value indicating whether firmware write protect is currently in force.
    /// </summary>
    /// <remarks>
    /// This is the inverse of <see cref="WriteProtectDisabled"/>, provided so that callers do
    /// not have to reason about the inverted hardware sense.
    /// </remarks>
    public bool WriteProtected => !WriteProtectDisabled;

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"EC Switches: Lid Open: {LidOpen}, Power Button Pressed: {PowerButtonPressed}, Write Protected: {WriteProtected}, Dedicated Recovery: {DedicatedRecovery}, Raw: 0x{RawSwitchByte.ToString("X2", CultureInfo.InvariantCulture)}";
    }
}
