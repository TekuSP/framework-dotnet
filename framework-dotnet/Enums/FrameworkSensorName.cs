namespace FrameworkDotnet.Enums;

/// <summary>
/// Represents the platform-specific role name for a temperature sensor slot, resolved from the detected
/// platform (mirrors the per-platform sensor labels in the native <c>power::print_thermal</c>). Slots past the
/// labelled set resolve to <see cref="Generic"/>; an undetermined platform yields <see cref="Unknown"/>.
/// </summary>
public enum FrameworkSensorName : ushort
{
    /// <summary>Platform could not be determined; the sensor role is indeterminate.</summary>
    Unknown = 0,

    /// <summary>Platform is known but no specific name is assigned to this slot.</summary>
    Generic = 1,

    /// <summary>F75303 local (board) sensor.</summary>
    F75303Local = 2,

    /// <summary>F75303 CPU-side sensor.</summary>
    F75303Cpu = 3,

    /// <summary>F75303 DDR / memory sensor.</summary>
    F75303Ddr = 4,

    /// <summary>Battery sensor.</summary>
    Battery = 5,

    /// <summary>CPU PECI sensor.</summary>
    Peci = 6,

    /// <summary>F57397 VCCGT (graphics rail) sensor.</summary>
    F57397VccGt = 7,

    /// <summary>F75303 skin (chassis surface) sensor.</summary>
    F75303Skin = 8,

    /// <summary>Charger IC sensor.</summary>
    ChargerIc = 9,

    /// <summary>APU (SoC) sensor.</summary>
    Apu = 10,

    /// <summary>Discrete GPU voltage-regulator sensor (Framework 16).</summary>
    DgpuVr = 11,

    /// <summary>Discrete GPU VRAM sensor (Framework 16).</summary>
    DgpuVram = 12,

    /// <summary>Discrete GPU ambient sensor (Framework 16).</summary>
    DgpuAmb = 13,

    /// <summary>Discrete GPU die temperature sensor (Framework 16).</summary>
    DgpuTemp = 14,

    /// <summary>F75303 APU sensor (Framework Desktop).</summary>
    F75303Apu = 15,

    /// <summary>F75303 ambient sensor (Framework Desktop).</summary>
    F75303Amb = 16,

    /// <summary>Virtual / aggregate sensor (Framework Desktop).</summary>
    Virtual = 17,
}
