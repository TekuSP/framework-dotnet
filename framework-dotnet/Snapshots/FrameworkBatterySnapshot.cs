using System.Collections.Generic;

using FrameworkDotnet.Enums;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents a battery snapshot returned by the EC.
/// </summary>
public sealed record FrameworkBatterySnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkBatterySnapshot"/> class.
    /// </summary>
    /// <param name="manufacturer">The battery manufacturer.</param>
    /// <param name="model_number">The battery model number.</param>
    /// <param name="serial_number">The battery serial number.</param>
    /// <param name="battery_type">The battery chemistry or type.</param>
    /// <param name="batteryState">The battery state.</param>
    public FrameworkBatterySnapshot(string manufacturer, string model_number, string serial_number, string battery_type, FrameworkBatteryState batteryState)
    {
        Manufacturer = manufacturer;
        Model_Number = model_number;
        Serial_Number = serial_number;
        Battery_Type = battery_type;
        BatteryState = batteryState;
    }

    /// <summary>
    /// Gets the battery manufacturer.
    /// </summary>
    public string Manufacturer
    {
        get;
    }

    /// <summary>
    /// Gets the battery model number.
    /// </summary>
    public string Model_Number
    {
        get;
    }

    /// <summary>
    /// Gets the battery serial number.
    /// </summary>
    public string Serial_Number
    {
        get;
    }

    /// <summary>
    /// Gets the battery chemistry or type.
    /// </summary>
    public string Battery_Type
    {
        get;
    }

    /// <summary>
    /// Gets the battery state.
    /// </summary>
    public FrameworkBatteryState BatteryState
    {
        get;
    }
}
