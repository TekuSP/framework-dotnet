using System.Collections.Generic;
using System.Linq;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents a thermal snapshot returned by the EC.
/// </summary>
public sealed record FrameworkThermalSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkThermalSnapshot"/> class.
    /// </summary>
    /// <param name="fanCount">The number of fans reported in the snapshot.</param>
    /// <param name="sensorCount">The number of sensors reported in the snapshot.</param>
    /// <param name="temperature_0">The first temperature reading.</param>
    /// <param name="temperature_1">The second temperature reading.</param>
    /// <param name="temperature_2">The third temperature reading.</param>
    /// <param name="temperature_3">The fourth temperature reading.</param>
    /// <param name="temperature_4">The fifth temperature reading.</param>
    /// <param name="temperature_5">The sixth temperature reading.</param>
    /// <param name="temperature_6">The seventh temperature reading.</param>
    /// <param name="temperature_7">The eighth temperature reading.</param>
    /// <param name="fan_0">The first fan snapshot.</param>
    /// <param name="fan_1">The second fan snapshot.</param>
    /// <param name="fan_2">The third fan snapshot.</param>
    /// <param name="fan_3">The fourth fan snapshot.</param>
    public FrameworkThermalSnapshot(byte fanCount, byte sensorCount, FrameworkTemperatureSnapshot temperature_0, FrameworkTemperatureSnapshot temperature_1, FrameworkTemperatureSnapshot temperature_2, FrameworkTemperatureSnapshot temperature_3, FrameworkTemperatureSnapshot temperature_4, FrameworkTemperatureSnapshot temperature_5, FrameworkTemperatureSnapshot temperature_6, FrameworkTemperatureSnapshot temperature_7, FrameworkFanSnapshot fan_0, FrameworkFanSnapshot fan_1, FrameworkFanSnapshot fan_2, FrameworkFanSnapshot fan_3)
    {
        FanCount = fanCount;
        SensorCount = sensorCount;
        Temperature_0 = temperature_0;
        Temperature_1 = temperature_1;
        Temperature_2 = temperature_2;
        Temperature_3 = temperature_3;
        Temperature_4 = temperature_4;
        Temperature_5 = temperature_5;
        Temperature_6 = temperature_6;
        Temperature_7 = temperature_7;
        Fan_0 = fan_0;
        Fan_1 = fan_1;
        Fan_2 = fan_2;
        Fan_3 = fan_3;
    }

    /// <summary>
    /// Gets the number of sensors reported in the snapshot.
    /// </summary>
    public byte SensorCount { get; init; }

    /// <summary>
    /// Gets the number of fans reported in the snapshot.
    /// </summary>
    public byte FanCount { get; init; }

    /// <summary>
    /// Gets the first temperature reading.
    /// </summary>
    public FrameworkTemperatureSnapshot Temperature_0 { get; init; }

    /// <summary>
    /// Gets the second temperature reading.
    /// </summary>
    public FrameworkTemperatureSnapshot Temperature_1 { get; init; }

    /// <summary>
    /// Gets the third temperature reading.
    /// </summary>
    public FrameworkTemperatureSnapshot Temperature_2 { get; init; }

    /// <summary>
    /// Gets the fourth temperature reading.
    /// </summary>
    public FrameworkTemperatureSnapshot Temperature_3 { get; init; }

    /// <summary>
    /// Gets the fifth temperature reading.
    /// </summary>
    public FrameworkTemperatureSnapshot Temperature_4 { get; init; }

    /// <summary>
    /// Gets the sixth temperature reading.
    /// </summary>
    public FrameworkTemperatureSnapshot Temperature_5 { get; init; }

    /// <summary>
    /// Gets the seventh temperature reading.
    /// </summary>
    public FrameworkTemperatureSnapshot Temperature_6 { get; init; }

    /// <summary>
    /// Gets the eighth temperature reading.
    /// </summary>
    public FrameworkTemperatureSnapshot Temperature_7 { get; init; }

    /// <summary>
    /// Gets the first fan snapshot.
    /// </summary>
    public FrameworkFanSnapshot Fan_0 { get; init; }

    /// <summary>
    /// Gets the second fan snapshot.
    /// </summary>
    public FrameworkFanSnapshot Fan_1 { get; init; }

    /// <summary>
    /// Gets the third fan snapshot.
    /// </summary>
    public FrameworkFanSnapshot Fan_2 { get; init; }

    /// <summary>
    /// Gets the fourth fan snapshot.
    /// </summary>
    public FrameworkFanSnapshot Fan_3 { get; init; }

    /// <summary>
    /// Gets all temperature readings in index order.
    /// </summary>
    public IReadOnlyList<FrameworkTemperatureSnapshot> Temperatures => [Temperature_0, Temperature_1, Temperature_2, Temperature_3, Temperature_4, Temperature_5, Temperature_6, Temperature_7];

    /// <summary>
    /// Gets the reported temperature readings in index order.
    /// </summary>
    /// <seealso cref="SensorCount"/>
    public IEnumerable<FrameworkTemperatureSnapshot> ReportedTemperatures => Temperatures.Take(SensorCount);

    /// <summary>
    /// Gets all fan snapshots in index order.
    /// </summary>
    public IReadOnlyList<FrameworkFanSnapshot> Fans => [Fan_0, Fan_1, Fan_2, Fan_3];

    /// <summary>
    /// Gets the reported fan snapshots in index order.
    /// </summary>
    /// <seealso cref="FanCount"/>
    public IEnumerable<FrameworkFanSnapshot> ReportedFans => Fans.Take(FanCount);
}
