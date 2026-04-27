namespace Framework.System.Interop;

internal unsafe partial struct FrameworkBatterySnapshot
{
    internal readonly string GetManufacturer()
    {
        var value = manufacturer;
        return value.ToUtf8StringAndFree();
    }

    internal readonly string GetModelNumber()
    {
        var value = model_number;
        return value.ToUtf8StringAndFree();
    }

    internal readonly string GetSerialNumber()
    {
        var value = serial_number;
        return value.ToUtf8StringAndFree();
    }

    internal readonly string GetBatteryType()
    {
        var value = battery_type;
        return value.ToUtf8StringAndFree();
    }
}
