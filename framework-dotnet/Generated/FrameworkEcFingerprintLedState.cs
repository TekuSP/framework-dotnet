using System;

using ManagedFingerprintLedSnapshot = FrameworkDotnet.Snapshots.FrameworkFingerprintLedSnapshot;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcFingerprintLedState
{
    internal readonly ManagedFingerprintLedSnapshot ToManagedSnapshot()
    {
        return new ManagedFingerprintLedSnapshot(raw_level, ToManagedLevel());
    }

    private readonly FrameworkDotnet.Enums.FrameworkFingerprintLedLevel ToManagedLevel()
    {
        return level switch
        {
            FrameworkFingerprintLedLevel.Unknown => FrameworkDotnet.Enums.FrameworkFingerprintLedLevel.Unknown,
            FrameworkFingerprintLedLevel.High => FrameworkDotnet.Enums.FrameworkFingerprintLedLevel.High,
            FrameworkFingerprintLedLevel.Medium => FrameworkDotnet.Enums.FrameworkFingerprintLedLevel.Medium,
            FrameworkFingerprintLedLevel.Low => FrameworkDotnet.Enums.FrameworkFingerprintLedLevel.Low,
            FrameworkFingerprintLedLevel.UltraLow => FrameworkDotnet.Enums.FrameworkFingerprintLedLevel.UltraLow,
            FrameworkFingerprintLedLevel.Custom => FrameworkDotnet.Enums.FrameworkFingerprintLedLevel.Custom,
            FrameworkFingerprintLedLevel.Auto => FrameworkDotnet.Enums.FrameworkFingerprintLedLevel.Auto,
            _ => throw new ArgumentOutOfRangeException(nameof(level), level, "Unhandled fingerprint LED level.")
        };
    }
}
