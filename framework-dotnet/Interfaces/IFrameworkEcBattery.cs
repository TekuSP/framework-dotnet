using System;

using FrameworkDotnet.Enums;
using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Snapshots;

using UnitsNet;

namespace FrameworkDotnet.Interfaces;

/// <summary>
/// Defines the battery surface of an embedded controller connection.
/// </summary>
/// <remarks>
/// Instances are obtained from an <see cref="IFrameworkEcConnection"/> and remain bound to the lifetime of that connection. Every member throws <see cref="ObjectDisposedException"/> once the owning connection has been disposed.
/// </remarks>
public interface IFrameworkEcBattery
{
    /// <summary>
    /// Reads the full Smart Battery data set from the pack.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This call performs many I2C passthrough round trips and is far slower than <see cref="IFrameworkEcConnection.GetPowerSnapshot"/>. Treat it strictly as an on-demand read: never place it in a polling loop, and keep it off the UI thread.
    /// </para>
    /// <para>
    /// Supplying <paramref name="unsealKey"/> unlocks the manufacturer-access register group. When the pack accepts the key, <see cref="FrameworkSmartBatterySnapshot.IsUnsealed"/> is <see langword="true"/> and the state-of-health, safety and lifetime groups are populated; otherwise those groups are <see langword="null"/>.
    /// </para>
    /// </remarks>
    /// <param name="unsealKey">The manufacturer-access unseal key, or <see langword="null"/> to read only the sealed-mode subset.</param>
    /// <returns>The Smart Battery snapshot, already copied into managed memory.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the owning connection has been disposed.</exception>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library returns an error status.</exception>
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkSmartBatterySnapshot GetSmartBatterySnapshot(uint? unsealKey = null);

    /// <summary>
    /// Gets the battery cutoff (ship mode) state.
    /// </summary>
    /// <returns>The reported cutoff state, or <see cref="FrameworkBatteryCutoffState.Unknown"/> when the embedded controller did not answer the query.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the owning connection has been disposed.</exception>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library returns an error status.</exception>
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the native layer reports a cutoff state value that is not recognized by the managed API.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkBatteryCutoffState GetCutoffState();

    /// <summary>
    /// Gets the current charging state together with the external power adapter state.
    /// </summary>
    /// <returns>The charging state snapshot.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the owning connection has been disposed.</exception>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library returns an error status.</exception>
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkChargingStateSnapshot GetChargingState();

    /// <summary>
    /// Runs the Smart Battery SHA-1 HMAC challenge and reports whether the pack answered it correctly.
    /// </summary>
    /// <remarks>
    /// A pack that answers the challenge but fails it is a legitimate negative answer, not a failure: the method returns <see langword="false"/> in that case rather than throwing. Exceptions are reserved for packs that do not answer at all and for transport failures.
    /// </remarks>
    /// <param name="authenticationKey">The authentication key. It must be exactly 16 bytes long.</param>
    /// <returns><see langword="true"/> when the pack answered the challenge correctly; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="authenticationKey"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="authenticationKey"/> is not exactly 16 bytes long.</exception>
    /// <exception cref="ObjectDisposedException">Thrown when the owning connection has been disposed.</exception>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library returns an error status, which includes a pack that did not answer the challenge at all.</exception>
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    bool Authenticate(byte[] authenticationKey);

    /// <summary>
    /// Sets the battery charge rate limit, optionally conditioned on a battery state-of-charge threshold.
    /// </summary>
    /// <remarks>
    /// The nullable threshold mirrors <see cref="IFrameworkEcConnection.SetChargeCurrentLimit(uint, int?)"/>: passing <see langword="null"/> applies the limit unconditionally, which the native layer expresses as a negative state-of-charge value.
    /// </remarks>
    /// <param name="rateLimit">The maximum charge rate. The native layer takes amperes as a single-precision value, so the quantity is narrowed to <see cref="float"/> at the boundary.</param>
    /// <param name="batterySoc">The battery state-of-charge threshold (0-100%) below which the limit is applied, or <see langword="null"/> to apply it unconditionally.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="rateLimit"/> is negative, or when <paramref name="batterySoc"/> is outside the 0-100 percent range.</exception>
    /// <exception cref="ObjectDisposedException">Thrown when the owning connection has been disposed.</exception>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library returns an error status.</exception>
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    void SetChargeRateLimit(ElectricCurrent rateLimit, Ratio? batterySoc = null);
}
