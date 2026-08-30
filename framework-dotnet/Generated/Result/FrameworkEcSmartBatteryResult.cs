using System;
using System.Buffers.Binary;

using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Snapshots;

using UnitsNet;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcSmartBatteryResult
{
    /// <summary>
    /// Copies the whole Smart Battery record into managed memory and then releases the native record.
    /// </summary>
    /// <remarks>
    /// <see cref="FrameworkSmartBatteryData"/> owns ten byte buffers and must be released as a whole through
    /// <c>framework_smart_battery_data_free</c>. The individual buffers must never be freed with
    /// <c>framework_byte_buffer_free</c>. Everything is copied into managed memory before the aggregate is released,
    /// and the release happens in a <c>finally</c> so it also runs on the failure path.
    /// </remarks>
    internal readonly FrameworkSmartBatterySnapshot GetValueOrThrow()
    {
        FrameworkSmartBatteryData owned = data;

        try
        {
            if (status.IsFailure)
            {
                throw FrameworkStatusException.GetCorrectException(status);
            }

            return ToManagedSnapshot(in owned);
        }
        finally
        {
            NativeMethods.framework_smart_battery_data_free(&owned);
        }
    }

    private static FrameworkSmartBatterySnapshot ToManagedSnapshot(in FrameworkSmartBatteryData value)
    {
        bool isUnsealed = value.unsealed != 0;

        return new FrameworkSmartBatterySnapshot(
            value.mode,
            value.serial_number,
            value.manufacture_date_raw,
            CreateManufactureDate(in value),
            value.device_name.ToUtf8String(),
            value.manufacturer_name.ToUtf8String(),
            value.device_chemistry.ToUtf8String(),
            value.firmware_version.ToArray(),
            Temperature.FromKelvins(value.temperature_decikelvin / 10.0),
            ElectricPotential.FromMillivolts((int)value.voltage_mv),
            ElectricPotential.FromMillivolts((int)value.cell_voltage_1_mv),
            ElectricPotential.FromMillivolts((int)value.cell_voltage_2_mv),
            ElectricPotential.FromMillivolts((int)value.cell_voltage_3_mv),
            ElectricPotential.FromMillivolts((int)value.cell_voltage_4_mv),
            ElectricCurrent.FromMilliamperes((int)value.current_ma),
            ElectricCurrent.FromMilliamperes((int)value.avg_current_ma),
            value.cycle_count,
            Ratio.FromPercent((int)value.rel_state_of_charge),
            Ratio.FromPercent((int)value.abs_state_of_charge),
            value.remaining_capacity,
            value.full_charge_capacity,
            value.design_capacity,
            ElectricPotential.FromMillivolts((int)value.design_voltage_mv),
            ElectricCurrent.FromMilliamperes((int)value.charging_current_ma),
            ElectricPotential.FromMillivolts((int)value.charging_voltage_mv),
            value.battery_status,
            isUnsealed,
            isUnsealed ? CreateStateOfHealth(in value) : null,
            isUnsealed ? CreateSafety(in value) : null,
            isUnsealed ? CreateLifetimeData(in value) : null);
    }

    private static DateOnly? CreateManufactureDate(in FrameworkSmartBatteryData value)
    {
        int year = value.manufacture_year;
        int month = value.manufacture_month;
        int day = value.manufacture_day;

        if (year < 1 || year > 9999 || month < 1 || month > 12)
        {
            return null;
        }

        if (day < 1 || day > DateTime.DaysInMonth(year, month))
        {
            return null;
        }

        return new DateOnly(year, month, day);
    }

    private static FrameworkBatteryStateOfHealthSnapshot CreateStateOfHealth(in FrameworkSmartBatteryData value)
    {
        byte[] raw = value.state_of_health.ToArray();

        ElectricCharge? chargeCapacity = raw.Length >= 2
            ? ElectricCharge.FromMilliampereHours((int)BinaryPrimitives.ReadUInt16LittleEndian(raw.AsSpan(0, 2)))
            : null;

        Energy? energyCapacity = raw.Length >= 4
            ? Energy.FromWattHours(BinaryPrimitives.ReadUInt16LittleEndian(raw.AsSpan(2, 2)) / 100.0)
            : null;

        return new FrameworkBatteryStateOfHealthSnapshot(chargeCapacity, energyCapacity, raw);
    }

    private static FrameworkBatterySafetySnapshot CreateSafety(in FrameworkSmartBatteryData value)
    {
        return new FrameworkBatterySafetySnapshot(
            value.operation_status,
            value.safety_alert,
            value.safety_status,
            value.pf_alert,
            value.pf_status);
    }

    private static FrameworkBatteryLifetimeDataSnapshot CreateLifetimeData(in FrameworkSmartBatteryData value)
    {
        return new FrameworkBatteryLifetimeDataSnapshot(
            value.lifetime_1.ToArray(),
            value.lifetime_2.ToArray(),
            value.lifetime_3.ToArray(),
            value.lifetime_4.ToArray(),
            value.lifetime_5.ToArray());
    }
}
