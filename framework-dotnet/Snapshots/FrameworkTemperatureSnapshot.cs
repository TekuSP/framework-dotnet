using FrameworkDotnet.Enums;

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
    public FrameworkTemperatureSnapshot(FrameworkTemperatureState state, Temperature temperature)
    {
        State = state;
        Temperature = temperature;
    }

    /// <summary>
    /// Gets the reading state.
    /// </summary>
    public FrameworkTemperatureState State { get; init; }

    /// <summary>
    /// Gets the temperature reading.
    /// </summary>
    public Temperature Temperature { get; init; }
}
