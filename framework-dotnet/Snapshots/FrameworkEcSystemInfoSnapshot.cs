using FrameworkDotnet.Enums;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents the embedded controller system information: which firmware image is running, why
/// the controller last reset, and its current lock and jump state.
/// </summary>
public sealed record FrameworkEcSystemInfoSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkEcSystemInfoSnapshot"/> class.
    /// </summary>
    /// <param name="currentImage">The firmware image the controller is currently executing.</param>
    /// <param name="resetFlags">The reasons recorded for the controller's most recent reset.</param>
    /// <param name="flags">The controller's current lock and jump state.</param>
    public FrameworkEcSystemInfoSnapshot(FrameworkEcCurrentImage currentImage, FrameworkEcResetFlag resetFlags, FrameworkEcSysinfoFlag flags)
    {
        CurrentImage = currentImage;
        ResetFlags = resetFlags;
        Flags = flags;
    }

    /// <summary>
    /// Gets the firmware image the controller is currently executing.
    /// </summary>
    public FrameworkEcCurrentImage CurrentImage { get; init; }

    /// <summary>
    /// Gets the reasons recorded for the controller's most recent reset.
    /// </summary>
    /// <remarks>
    /// More than one reason can be recorded for a single reset, so test individual flags rather
    /// than comparing the whole value.
    /// </remarks>
    public FrameworkEcResetFlag ResetFlags { get; init; }

    /// <summary>
    /// Gets the controller's current lock and jump state.
    /// </summary>
    public FrameworkEcSysinfoFlag Flags { get; init; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"EC System Info: Current Image: {CurrentImage}, Reset Flags: {ResetFlags}, Flags: {Flags}";
    }
}
