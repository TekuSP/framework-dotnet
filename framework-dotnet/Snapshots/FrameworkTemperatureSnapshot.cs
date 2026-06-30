using FrameworkDotnet.Enums;

using System.Globalization;

using UnitsNet;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents a temperature reading from the EC.
/// </summary>
public sealed record FrameworkTemperatureSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkTemperatureSnapshot"/> class.
    /// </summary>
    /// <param name="state">The reading state.</param>
    /// <param name="temperature">The temperature reading.</param>
    /// <param name="name">The platform role name of the sensor.</param>
    public FrameworkTemperatureSnapshot(FrameworkTemperatureState state, Temperature temperature, FrameworkSensorName name)
    {
        State = state;
        Temperature = temperature;
        Name = name;
    }

    /// <summary>
    /// Gets the reading state.
    /// </summary>
    public FrameworkTemperatureState State { get; init; }

    /// <summary>
    /// Gets the temperature reading.
    /// </summary>
    public Temperature Temperature { get; init; }

    /// <summary>
    /// Gets the platform role name of the sensor (e.g. CPU, APU, DDR), or
    /// <see cref="FrameworkSensorName.Generic"/> / <see cref="FrameworkSensorName.Unknown"/> when not identified.
    /// </summary>
    public FrameworkSensorName Name { get; init; }

    public override string ToString()
    {
        return $"Temperature Snapshot: Name: {Name}, State: {State}, Temperature: {Temperature.ToString(CultureInfo.InvariantCulture)}";
    }
}
