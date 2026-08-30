using System.Globalization;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents the standalone (batteryless) mode state reported by the EC.
/// </summary>
/// <remarks>
/// <para>
/// Standalone mode describes a system that runs without a battery pack installed, which is the
/// normal configuration for Framework Desktop.
/// </para>
/// <para>
/// <b><see langword="true"/> does not prove there is no battery.</b> Upstream derives both values
/// from a "no battery reported" check and falls back to <see langword="true"/> whenever the
/// embedded controller power-info read produces nothing, as a safe default. The native call
/// reports success in that case, so on a battery-equipped family <see langword="true"/> means
/// battery status could not be read rather than that no battery is fitted, and callers cannot
/// distinguish it from a genuine Desktop by the status code.
/// </para>
/// </remarks>
public sealed record FrameworkStandaloneModeSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkStandaloneModeSnapshot"/> class.
    /// </summary>
    /// <param name="isEcStandalone">The EC's own standalone reading.</param>
    /// <param name="isStandalone">The effective standalone state, including the platform default.</param>
    public FrameworkStandaloneModeSnapshot(bool isEcStandalone, bool isStandalone)
    {
        IsEcStandalone = isEcStandalone;
        IsStandalone = isStandalone;
    }

    /// <summary>
    /// Gets a value indicating whether the EC itself reports that the system runs without a battery.
    /// </summary>
    /// <remarks>
    /// Upstream currently computes this and <see cref="IsStandalone"/> from the same "no battery
    /// reported" check, so the two agree in practice. They are surfaced separately because the
    /// native ABI reports both, and upstream marks the second as unfinished work.
    /// </remarks>
    public bool IsEcStandalone { get; init; }

    /// <summary>
    /// Gets a value indicating whether the system is in standalone (batteryless) mode.
    /// </summary>
    /// <remarks>
    /// Upstream carries this as a placeholder that currently repeats the same computation as
    /// <see cref="IsEcStandalone"/>, pending a real platform-default path. Expect the two to agree
    /// until that lands; neither is more authoritative than the other today.
    /// </remarks>
    public bool IsStandalone { get; init; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"Standalone Mode: EC Reading: {IsEcStandalone.ToString(CultureInfo.InvariantCulture)}, Effective: {IsStandalone.ToString(CultureInfo.InvariantCulture)}";
    }
}
