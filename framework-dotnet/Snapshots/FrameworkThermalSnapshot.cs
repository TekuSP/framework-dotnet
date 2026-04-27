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
    /// <param name="fanRpm0">The first fan RPM value.</param>
    /// <param name="fanRpm1">The second fan RPM value.</param>
    /// <param name="fanRpm2">The third fan RPM value.</param>
    /// <param name="fanRpm3">The fourth fan RPM value.</param>
    /// <param name="fanPresent0">A value indicating whether the first fan is present.</param>
    /// <param name="fanPresent1">A value indicating whether the second fan is present.</param>
    /// <param name="fanPresent2">A value indicating whether the third fan is present.</param>
    /// <param name="fanPresent3">A value indicating whether the fourth fan is present.</param>
    /// <param name="fanStalled0">A value indicating whether the first fan is stalled.</param>
    /// <param name="fanStalled1">A value indicating whether the second fan is stalled.</param>
    /// <param name="fanStalled2">A value indicating whether the third fan is stalled.</param>
    /// <param name="fanStalled3">A value indicating whether the fourth fan is stalled.</param>
    public FrameworkThermalSnapshot(byte fanCount, FrameworkTemperatureSnapshot temperature0, FrameworkTemperatureSnapshot temperature1, FrameworkTemperatureSnapshot temperature2, FrameworkTemperatureSnapshot temperature3, FrameworkTemperatureSnapshot temperature4, FrameworkTemperatureSnapshot temperature5, FrameworkTemperatureSnapshot temperature6, FrameworkTemperatureSnapshot temperature7, ushort fanRpm0, ushort fanRpm1, ushort fanRpm2, ushort fanRpm3, bool fanPresent0, bool fanPresent1, bool fanPresent2, bool fanPresent3, bool fanStalled0, bool fanStalled1, bool fanStalled2, bool fanStalled3)
    {
        FanCount = fanCount;
        Temperature0 = temperature0;
        Temperature1 = temperature1;
        Temperature2 = temperature2;
        Temperature3 = temperature3;
        Temperature4 = temperature4;
        Temperature5 = temperature5;
        Temperature6 = temperature6;
        Temperature7 = temperature7;
        FanRpm0 = fanRpm0;
        FanRpm1 = fanRpm1;
        FanRpm2 = fanRpm2;
        FanRpm3 = fanRpm3;
        FanPresent0 = fanPresent0;
        FanPresent1 = fanPresent1;
        FanPresent2 = fanPresent2;
        FanPresent3 = fanPresent3;
        FanStalled0 = fanStalled0;
        FanStalled1 = fanStalled1;
        FanStalled2 = fanStalled2;
        FanStalled3 = fanStalled3;
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
    /// Gets the first fan RPM value.
    /// </summary>
    public ushort FanRpm0 { get; init; }

    /// <summary>
    /// Gets the second fan RPM value.
    /// </summary>
    public ushort FanRpm1 { get; init; }

    /// <summary>
    /// Gets the third fan RPM value.
    /// </summary>
    public ushort FanRpm2 { get; init; }

    /// <summary>
    /// Gets the fourth fan RPM value.
    /// </summary>
    public ushort FanRpm3 { get; init; }

    /// <summary>
    /// Gets a value indicating whether the first fan is present.
    /// </summary>
    public bool FanPresent0 { get; init; }

    /// <summary>
    /// Gets a value indicating whether the second fan is present.
    /// </summary>
    public bool FanPresent1 { get; init; }

    /// <summary>
    /// Gets a value indicating whether the third fan is present.
    /// </summary>
    public bool FanPresent2 { get; init; }

    /// <summary>
    /// Gets a value indicating whether the fourth fan is present.
    /// </summary>
    public bool FanPresent3 { get; init; }

    /// <summary>
    /// Gets a value indicating whether the first fan is stalled.
    /// </summary>
    public bool FanStalled0 { get; init; }

    /// <summary>
    /// Gets a value indicating whether the second fan is stalled.
    /// </summary>
    public bool FanStalled1 { get; init; }

    /// <summary>
    /// Gets a value indicating whether the third fan is stalled.
    /// </summary>
    public bool FanStalled2 { get; init; }

    /// <summary>
    /// Gets a value indicating whether the fourth fan is stalled.
    /// </summary>
    public bool FanStalled3 { get; init; }

    /// <summary>
    /// Gets all temperature readings in index order.
    /// </summary>
    public IReadOnlyList<FrameworkTemperatureSnapshot> Temperatures => [Temperature0, Temperature1, Temperature2, Temperature3, Temperature4, Temperature5, Temperature6, Temperature7];

    /// <summary>
    /// Gets all fan RPM values in index order.
    /// </summary>
    public IReadOnlyList<ushort> FanRpm => [FanRpm0, FanRpm1, FanRpm2, FanRpm3];

    /// <summary>
    /// Gets all fan-present flags in index order.
    /// </summary>
    public IReadOnlyList<bool> FanPresent => [FanPresent0, FanPresent1, FanPresent2, FanPresent3];

    /// <summary>
    /// Gets all fan-stalled flags in index order.
    /// </summary>
    public IReadOnlyList<bool> FanStalled => [FanStalled0, FanStalled1, FanStalled2, FanStalled3];
}
