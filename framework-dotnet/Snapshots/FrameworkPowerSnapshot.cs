using FrameworkDotnet.Enums;

using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents a power snapshot returned by the EC.
/// </summary>
public sealed record FrameworkPowerSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkPowerSnapshot"/> class.
    /// </summary>
    /// <param name="powerSourceState">The current power source state.</param>
    /// <param name="batteryCount">The number of batteries reported by the device.</param>
    /// <param name="battery_0">The first battery snapshot.</param>
    public FrameworkPowerSnapshot(FrameworkPowerSourceState powerSourceState, byte batteryCount, FrameworkBatterySnapshot battery_0)
    {
        PowerSourceState = powerSourceState;
        BatteryCount = batteryCount;
        Battery_0 = battery_0;
    }

    /// <summary>
    /// Gets the current power source state.
    /// </summary>
    public FrameworkPowerSourceState PowerSourceState { get; init; }

    /// <summary>
    /// Gets the number of batteries reported by the device.
    /// </summary>
    public byte BatteryCount { get; init; }

    /// <summary>
    /// Gets the first battery snapshot.
    /// </summary>
    public FrameworkBatterySnapshot Battery_0 { get; init; }

    /// <summary>
    /// Gets the battery snapshots in index order.
    /// </summary>
    public IReadOnlyList<FrameworkBatterySnapshot> Batteries => [Battery_0];

    /// <summary>
    /// Gets the reported battery snapshots in index order.
    /// </summary>
    /// <seealso cref="BatteryCount"/>
    public IEnumerable<FrameworkBatterySnapshot> ReportedBatteries => Batteries.Take(BatteryCount);

    public override string ToString()
    {
        return $"Power Snapshot: Power Source State: {PowerSourceState}, Battery Count: {BatteryCount.ToString(CultureInfo.InvariantCulture)}, Batteries: {string.Join(", ", ReportedBatteries)}";
    }
}
