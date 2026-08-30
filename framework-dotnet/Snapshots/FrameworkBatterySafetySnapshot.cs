using System.Globalization;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents the Smart Battery operation, safety and permanent-failure status words.
/// </summary>
/// <remarks>
/// These registers live behind manufacturer access and are only readable once the pack has been unsealed, so this snapshot is only produced when <see cref="FrameworkSmartBatterySnapshot.IsUnsealed"/> is <see langword="true"/>. The words are reported verbatim; their bit layout is defined by the pack's gas-gauge firmware and is deliberately not interpreted here.
/// </remarks>
public sealed record FrameworkBatterySafetySnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkBatterySafetySnapshot"/> class.
    /// </summary>
    /// <param name="operationStatus">The raw gas-gauge operation status word.</param>
    /// <param name="safetyAlert">The raw safety alert word.</param>
    /// <param name="safetyStatus">The raw safety status word.</param>
    /// <param name="permanentFailureAlert">The raw permanent-failure alert word.</param>
    /// <param name="permanentFailureStatus">The raw permanent-failure status word.</param>
    public FrameworkBatterySafetySnapshot(
        uint operationStatus,
        uint safetyAlert,
        uint safetyStatus,
        uint permanentFailureAlert,
        uint permanentFailureStatus)
    {
        OperationStatus = operationStatus;
        SafetyAlert = safetyAlert;
        SafetyStatus = safetyStatus;
        PermanentFailureAlert = permanentFailureAlert;
        PermanentFailureStatus = permanentFailureStatus;
    }

    /// <summary>
    /// Gets the raw gas-gauge operation status word.
    /// </summary>
    public uint OperationStatus { get; init; }

    /// <summary>
    /// Gets the raw safety alert word, which reports conditions the pack is currently warning about.
    /// </summary>
    public uint SafetyAlert { get; init; }

    /// <summary>
    /// Gets the raw safety status word, which reports conditions the pack has latched.
    /// </summary>
    public uint SafetyStatus { get; init; }

    /// <summary>
    /// Gets the raw permanent-failure alert word.
    /// </summary>
    public uint PermanentFailureAlert { get; init; }

    /// <summary>
    /// Gets the raw permanent-failure status word. A non-zero value indicates the pack has permanently disabled itself.
    /// </summary>
    public uint PermanentFailureStatus { get; init; }

    public override string ToString()
    {
        return $"Battery Safety: Operation Status: 0x{OperationStatus.ToString("X8", CultureInfo.InvariantCulture)}, Safety Alert: 0x{SafetyAlert.ToString("X8", CultureInfo.InvariantCulture)}, Safety Status: 0x{SafetyStatus.ToString("X8", CultureInfo.InvariantCulture)}, PF Alert: 0x{PermanentFailureAlert.ToString("X8", CultureInfo.InvariantCulture)}, PF Status: 0x{PermanentFailureStatus.ToString("X8", CultureInfo.InvariantCulture)}";
    }
}
