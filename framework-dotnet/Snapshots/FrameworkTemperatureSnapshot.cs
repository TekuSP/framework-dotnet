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
    /// <param name="celsius">The temperature in degrees Celsius.</param>
    public FrameworkTemperatureSnapshot(short celsius)
    {
        Celsius = celsius;
    }

    /// <summary>
    /// Gets the temperature in degrees Celsius.
    /// </summary>
    public short Celsius { get; init; }
}
