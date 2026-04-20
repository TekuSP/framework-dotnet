using FrameworkDotnet.Generated;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkPowerSnapshot
{
    public readonly string GetManufacturer()
    {
        fixed (byte* value = manufacturer)
        {
            return InteropStringHelpers.ReadNullTerminatedUtf8(value, 8);
        }
    }

    public readonly string GetModelNumber()
    {
        fixed (byte* value = model_number)
        {
            return InteropStringHelpers.ReadNullTerminatedUtf8(value, 8);
        }
    }

    public readonly string GetSerialNumber()
    {
        fixed (byte* value = serial_number)
        {
            return InteropStringHelpers.ReadNullTerminatedUtf8(value, 8);
        }
    }

    public readonly string GetBatteryType()
    {
        fixed (byte* value = battery_type)
        {
            return InteropStringHelpers.ReadNullTerminatedUtf8(value, 8);
        }
    }
}
