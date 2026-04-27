using ManagedThermalSnapshot = FrameworkDotnet.Snapshots.FrameworkThermalSnapshot;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkThermalSnapshot
{
    private readonly FrameworkDotnet.Snapshots.FrameworkTemperatureSnapshot ToManagedTemperatureSnapshot(byte sensorCount, byte sensorIndex, FrameworkTemperatureReading reading)
    {
        return sensorIndex >= sensorCount
            ? reading.ToManagedSnapshot()
            : reading.GetValueOrThrow().ToManagedSnapshot();
    }

    private readonly FrameworkDotnet.Snapshots.FrameworkFanSnapshot ToManagedFanSnapshot(byte fanCount, byte fanIndex, FrameworkFanReading reading)
    {
        return fanIndex >= fanCount
            ? reading.ToManagedSnapshot()
            : reading.GetValueOrThrow().ToManagedSnapshot();
    }

    internal readonly byte CalculateSensorCount()
    {
        byte sensor_count = 0;

        if (temperature_0.state != FrameworkTemperatureState.Ok) return sensor_count;
        sensor_count++;
        if (temperature_1.state != FrameworkTemperatureState.Ok) return sensor_count;
        sensor_count++;
        if (temperature_2.state != FrameworkTemperatureState.Ok) return sensor_count;
        sensor_count++;
        if (temperature_3.state != FrameworkTemperatureState.Ok) return sensor_count;
        sensor_count++;
        if (temperature_4.state != FrameworkTemperatureState.Ok) return sensor_count;
        sensor_count++;
        if (temperature_5.state != FrameworkTemperatureState.Ok) return sensor_count;
        sensor_count++;
        if (temperature_6.state != FrameworkTemperatureState.Ok) return sensor_count;
        sensor_count++;
        if (temperature_7.state != FrameworkTemperatureState.Ok) return sensor_count;
        sensor_count++;

        return sensor_count;
    }
    internal readonly ManagedThermalSnapshot ToManagedSnapshot()
    {
        var sensor_count = CalculateSensorCount();
        return new ManagedThermalSnapshot(
            fan_count,
            sensor_count,
            ToManagedTemperatureSnapshot(sensor_count, 0, temperature_0),
            ToManagedTemperatureSnapshot(sensor_count, 1, temperature_1),
            ToManagedTemperatureSnapshot(sensor_count, 2, temperature_2),
            ToManagedTemperatureSnapshot(sensor_count, 3, temperature_3),
            ToManagedTemperatureSnapshot(sensor_count, 4, temperature_4),
            ToManagedTemperatureSnapshot(sensor_count, 5, temperature_5),
            ToManagedTemperatureSnapshot(sensor_count, 6, temperature_6),
            ToManagedTemperatureSnapshot(sensor_count, 7, temperature_7),
            ToManagedFanSnapshot(fan_count, 0, fan_0),
            ToManagedFanSnapshot(fan_count, 1, fan_1),
            ToManagedFanSnapshot(fan_count, 2, fan_2),
            ToManagedFanSnapshot(fan_count, 3, fan_3));
    }
}
