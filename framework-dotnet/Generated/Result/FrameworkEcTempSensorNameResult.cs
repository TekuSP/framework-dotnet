using FrameworkDotnet.Enums;
using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Snapshots;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcTempSensorNameResult
{
    internal readonly FrameworkTemperatureSensorNameSnapshot GetValueOrThrow()
    {
        var value = name;

        if (status.IsFailure)
        {
            try
            {
                throw FrameworkStatusException.GetCorrectException(status);
            }
            finally
            {
                value.Free();
            }
        }

        return new FrameworkTemperatureSensorNameSnapshot(
            sensor_index,
            value.ToUtf8StringAndFree(),
            (FrameworkDotnet.Enums.FrameworkSensorName)(ushort)mapped_name,
            (FrameworkTemperatureSensorType)sensor_type);
    }
}
