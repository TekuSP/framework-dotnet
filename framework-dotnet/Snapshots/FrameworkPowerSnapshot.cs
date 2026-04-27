using System.Collections.Generic;

using FrameworkDotnet.Enums;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents a power snapshot returned by the EC.
/// </summary>
public sealed record FrameworkPowerSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkPowerSnapshot"/> class.
    /// </summary>
    /// <param name="power_source_state">The current power source state.</param>
    /// <param name="battery_count">The number of batteries reported by the device.</param>
    /// <param name="battery_0">The first battery snapshot.</param>
    public FrameworkPowerSnapshot(FrameworkPowerSourceState power_source_state, byte battery_count, FrameworkBatterySnapshot battery_0)
    {
        PowerSourceState = power_source_state;
        BatteryCount = battery_count;
        Battery_0 = battery_0;
    }

    /// <summary>
    /// Gets the current power source state.
    /// </summary>
    public FrameworkPowerSourceState PowerSourceState
    {
        get;
    }

    /// <summary>
    /// Gets the number of batteries reported by the device.
    /// </summary>
    public byte BatteryCount
    {
        get;
    }

    /// <summary>
    /// Gets the first battery snapshot.
    /// </summary>
    public FrameworkBatterySnapshot Battery_0
    {
        get;
    }

    /// <summary>
    /// Gets the battery snapshots in index order.
    /// </summary>
    public IReadOnlyList<FrameworkBatterySnapshot> Batteries => [Battery_0];
}
