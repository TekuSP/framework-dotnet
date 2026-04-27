using System.Collections.Generic;

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
    /// <param name="temperature0">The first temperature reading.</param>
    /// <param name="temperature1">The second temperature reading.</param>
    /// <param name="temperature2">The third temperature reading.</param>
    /// <param name="temperature3">The fourth temperature reading.</param>
    /// <param name="temperature4">The fifth temperature reading.</param>
    /// <param name="temperature5">The sixth temperature reading.</param>
    /// <param name="temperature6">The seventh temperature reading.</param>
    /// <param name="temperature7">The eighth temperature reading.</param>
    /// <param name="fan0">The first fan snapshot.</param>
    /// <param name="fan1">The second fan snapshot.</param>
    /// <param name="fan2">The third fan snapshot.</param>
    /// <param name="fan3">The fourth fan snapshot.</param>
    public FrameworkThermalSnapshot(byte fanCount, byte sensorCount, FrameworkTemperatureSnapshot temperature0, FrameworkTemperatureSnapshot temperature1, FrameworkTemperatureSnapshot temperature2, FrameworkTemperatureSnapshot temperature3, FrameworkTemperatureSnapshot temperature4, FrameworkTemperatureSnapshot temperature5, FrameworkTemperatureSnapshot temperature6, FrameworkTemperatureSnapshot temperature7, FrameworkFanSnapshot fan0, FrameworkFanSnapshot fan1, FrameworkFanSnapshot fan2, FrameworkFanSnapshot fan3)
    {
        FanCount = fanCount;
        SensorCount = sensorCount;
        Temperature0 = temperature0;
        Temperature1 = temperature1;
        Temperature2 = temperature2;
        Temperature3 = temperature3;
        Temperature4 = temperature4;
        Temperature5 = temperature5;
        Temperature6 = temperature6;
        Temperature7 = temperature7;
        Fan0 = fan0;
        Fan1 = fan1;
        Fan2 = fan2;
        Fan3 = fan3;
    }

    /// <summary>
    /// Gets the number of sensors reported in the snapshot.
    /// </summary>
    public byte SensorCount
    {
        get; init;
    }

    /// <summary>
    /// Gets the number of fans reported in the snapshot.
    /// </summary>
    public byte FanCount { get; init; }

    /// <summary>
    /// Gets the first temperature reading.
    /// </summary>
    public FrameworkTemperatureSnapshot Temperature0 { get; init; }

    /// <summary>
    /// Gets the second temperature reading.
    /// </summary>
    public FrameworkTemperatureSnapshot Temperature1 { get; init; }

    /// <summary>
    /// Gets the third temperature reading.
    /// </summary>
    public FrameworkTemperatureSnapshot Temperature2 { get; init; }

    /// <summary>
    /// Gets the fourth temperature reading.
    /// </summary>
    public FrameworkTemperatureSnapshot Temperature3 { get; init; }

    /// <summary>
    /// Gets the fifth temperature reading.
    /// </summary>
    public FrameworkTemperatureSnapshot Temperature4 { get; init; }

    /// <summary>
    /// Gets the sixth temperature reading.
    /// </summary>
    public FrameworkTemperatureSnapshot Temperature5 { get; init; }

    /// <summary>
    /// Gets the seventh temperature reading.
    /// </summary>
    public FrameworkTemperatureSnapshot Temperature6 { get; init; }

    /// <summary>
    /// Gets the eighth temperature reading.
    /// </summary>
    public FrameworkTemperatureSnapshot Temperature7 { get; init; }

    /// <summary>
    /// Gets the first fan snapshot.
    /// </summary>
    public FrameworkFanSnapshot Fan0
    {
        get;
    }

    /// <summary>
    /// Gets the second fan snapshot.
    /// </summary>
    public FrameworkFanSnapshot Fan1
    {
        get;
    }

    /// <summary>
    /// Gets the third fan snapshot.
    /// </summary>
    public FrameworkFanSnapshot Fan2
    {
        get;
    }

    /// <summary>
    /// Gets the fourth fan snapshot.
    /// </summary>
    public FrameworkFanSnapshot Fan3
    {
        get;
    }

    /// <summary>
    /// Gets all temperature readings in index order.
    /// </summary>
    public IReadOnlyList<FrameworkTemperatureSnapshot> Temperatures => [Temperature0, Temperature1, Temperature2, Temperature3, Temperature4, Temperature5, Temperature6, Temperature7];

    /// <summary>
    /// Gets all fan snapshots in index order.
    /// </summary>
    public IReadOnlyList<FrameworkFanSnapshot> Fans => [Fan0, Fan1, Fan2, Fan3];
}
