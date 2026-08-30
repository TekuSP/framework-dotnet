using System.Globalization;

using FrameworkDotnet.Enums;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents the identity of one embedded controller temperature sensor slot.
/// </summary>
/// <remarks>
/// Reading a sensor name costs one host command, so names are read once per session and cached.
/// Live temperature values come from the thermal snapshot instead, which is the surface intended
/// for polling.
/// </remarks>
public sealed record FrameworkTemperatureSensorNameSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkTemperatureSensorNameSnapshot"/> class.
    /// </summary>
    /// <param name="sensorIndex">The temperature sensor slot the name belongs to.</param>
    /// <param name="firmwareName">The raw sensor name reported by embedded controller firmware.</param>
    /// <param name="mappedName">The firmware name reconciled onto the stable managed sensor role names.</param>
    /// <param name="sensorType">The embedded controller's classification tag for the slot.</param>
    public FrameworkTemperatureSensorNameSnapshot(uint sensorIndex, string firmwareName, FrameworkSensorName mappedName, FrameworkTemperatureSensorType sensorType)
    {
        SensorIndex = sensorIndex;
        FirmwareName = firmwareName;
        MappedName = mappedName;
        SensorType = sensorType;
    }

    /// <summary>
    /// Gets the temperature sensor slot this name belongs to. The index matches the temperature slot
    /// order of the thermal snapshot.
    /// </summary>
    public uint SensorIndex { get; init; }

    /// <summary>
    /// Gets the raw sensor name exactly as embedded controller firmware reports it. Firmware wording
    /// varies between platforms and firmware revisions, so treat it as display text rather than as a
    /// stable identifier.
    /// </summary>
    public string FirmwareName { get; init; }

    /// <summary>
    /// Gets the firmware name reconciled onto the stable managed sensor role names, or
    /// <see cref="FrameworkSensorName.Generic"/> when firmware uses a name this version does not
    /// recognize. Use this rather than <see cref="FirmwareName"/> when branching in code.
    /// </summary>
    public FrameworkSensorName MappedName { get; init; }

    /// <summary>
    /// Gets the embedded controller's classification tag for the slot, which says what the sensor
    /// physically measures.
    /// </summary>
    public FrameworkTemperatureSensorType SensorType { get; init; }

    /// <summary>
    /// Returns a readable description of the sensor identity.
    /// </summary>
    /// <returns>A readable description of the sensor identity.</returns>
    public override string ToString()
    {
        return $"Temperature Sensor Name Snapshot: Sensor Index: {SensorIndex.ToString(CultureInfo.InvariantCulture)}, Firmware Name: {FirmwareName}, Mapped Name: {MappedName}, Sensor Type: {SensorType}";
    }
}
