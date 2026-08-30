using System;
using System.Collections.Generic;

using FrameworkDotnet.Attributes;
using FrameworkDotnet.Enums;
using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Snapshots;

using UnitsNet;

namespace FrameworkDotnet.Interfaces;

/// <summary>
/// Defines the embedded controller input-device controls: per-key RGB lighting, keyboard matrix
/// remapping, PS/2 emulation and fine-grained fingerprint LED brightness.
/// </summary>
/// <remarks>
/// Every member of this facet is a write path: each call issues an embedded controller host command that
/// changes hardware behaviour immediately. Upstream framework-system does not document whether any of these
/// settings survives an EC reset or a power cycle, so treat them as volatile embedded controller state and
/// re-apply them after resume rather than relying on them persisting. The facet borrows the embedded
/// controller handle owned by <see cref="IFrameworkEcConnection"/> and must not outlive it.
/// </remarks>
public interface IFrameworkEcInput
{
    /// <summary>
    /// Sets the per-key RGB keyboard colors for a contiguous run of keys beginning at the given key index.
    /// </summary>
    /// <param name="startKey">The zero-based embedded controller key index at which the run of colors begins. Valid values are 0 through 255.</param>
    /// <param name="colors">The colors to apply, one per key, in ascending key order. At most 64 colors may be sent in a single call.</param>
    /// <remarks>
    /// Each color is transmitted as three bytes in red, green, blue order, so the managed list length is the key
    /// count and the stride cannot be misstated. Upstream framework-system documents this surface for the Framework
    /// Desktop RGB LEDs; the Framework Laptop 16 keyboard is not driven by the embedded controller and is configured
    /// through the vendor's own keyboard tooling instead.
    /// <para>
    /// Upstream framework-system does not document whether the colors survive an EC reset or a power cycle, and the
    /// embedded controller may overwrite them whenever it resumes driving its own lighting behaviour. Treat them as
    /// volatile embedded controller state and re-apply them as needed. To color more than 64 keys, issue several
    /// calls with successive <paramref name="startKey"/> values.
    /// </para>
    /// </remarks>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="colors"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="startKey"/> is outside the 0–255 range, or when <paramref name="colors"/> is empty or contains more than 64 entries.</exception>
    /// <exception cref="ObjectDisposedException">Thrown when the owning connection has been disposed.</exception>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library returns an error status.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.FrameworkDesktop, Message = "Upstream framework-system documents the RGB LED surface on Framework Desktop only. The Framework Laptop 16 keyboard is not driven by the embedded controller.")]
    void SetRgbKeyboardColors(int startKey, IReadOnlyList<FrameworkKeyboardColor> colors);

    /// <summary>
    /// Remaps the key at the given keyboard matrix position to the given scan code.
    /// </summary>
    /// <param name="row">The zero-based keyboard matrix row. Valid values are 0 through 255.</param>
    /// <param name="column">The zero-based keyboard matrix column. Valid values are 0 through 255.</param>
    /// <param name="scanCode">The scan set 2 code the matrix position should report.</param>
    /// <remarks>
    /// This is an advanced API. The embedded controller rewrites its keyboard matrix map, so the change applies to
    /// the built-in keyboard before the operating system ever sees a key event, and it therefore cannot be undone
    /// from software that only remaps at the operating system layer. Writing a wrong row, column or scan code can
    /// make a key report the wrong character, report nothing at all, or shadow a modifier, which can leave the
    /// keyboard difficult to use. Know the matrix position for the target device before calling this, and keep an
    /// external keyboard available while experimenting.
    /// <para>
    /// <b>The matrix is model-specific.</b> Framework Laptop 12 and Framework Laptop 13 use different row and column
    /// assignments for the same physical keys, so a position taken from one model will address a different key on
    /// the other. The Framework Laptop 16 keyboard is not driven by the embedded controller, so this call does not
    /// change its behaviour. Scan codes are shared across models.
    /// </para>
    /// <para>
    /// Upstream framework-system does not document whether the mapping survives an EC reset or a power cycle. Treat
    /// it as volatile embedded controller state and re-apply it rather than relying on it persisting.
    /// </para>
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="row"/> or <paramref name="column"/> is outside the 0–255 range.</exception>
    /// <exception cref="ObjectDisposedException">Thrown when the owning connection has been disposed.</exception>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library returns an error status.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    /// <seealso cref="RemapCapsLockToControl"/>
    void RemapKey(int row, int column, ushort scanCode);

    /// <summary>
    /// Remaps the key at Framework Laptop 12 matrix position row 6, column 15 to Control.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This is the named shorthand the native layer provides for <see cref="RemapKey(int, int, ushort)"/> at matrix
    /// row 6, column 15, with scan code <c>0x0014</c>. That position is Caps Lock <b>on Framework Laptop 12 only</b>
    /// — the matrix differs per model, so this call does not remap Caps Lock on other families and will silently
    /// remap whatever key occupies that position instead:
    /// </para>
    /// <para>
    /// On Framework Laptop 13, call <see cref="RemapKey(int, int, ushort)"/> with row 4, column 4 and scan code
    /// <c>0x0014</c> to reach Caps Lock. The Framework Laptop 16 keyboard is not remappable through the embedded
    /// controller at all.
    /// </para>
    /// <para>
    /// Where the position is correct, the physical Caps Lock key reports Control afterwards and Caps Lock can no
    /// longer be toggled from that key, so the same advanced-API cautions apply. Upstream framework-system does not
    /// document whether the mapping survives an EC reset or a power cycle; treat it as volatile embedded controller
    /// state.
    /// </para>
    /// </remarks>
    /// <exception cref="ObjectDisposedException">Thrown when the owning connection has been disposed.</exception>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library returns an error status.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    /// <seealso cref="RemapKey(int, int, ushort)"/>
    void RemapCapsLockToControl();

    /// <summary>
    /// Enables or disables PS/2 emulation on the embedded controller.
    /// </summary>
    /// <param name="enabled"><see langword="true"/> to enable PS/2 emulation; <see langword="false"/> to disable it.</param>
    /// <remarks>
    /// <para>
    /// <b>This is a debug-only control.</b> Upstream framework-system hides the equivalent command from its own help
    /// output and describes it as affecting the <b>touchpad</b>, with the documented recovery being to <b>reboot the
    /// system</b> if the touchpad stops working. The native ABI comment describes it as keyboard emulation; where the
    /// two disagree, upstream framework-system is the behaviour that ships. Expect either pointing or key input to be
    /// affected, and do not assume toggling the flag back is enough to recover.
    /// </para>
    /// <para>
    /// Upstream does not document whether the setting survives an EC reset or a power cycle. Treat it as volatile
    /// embedded controller state.
    /// </para>
    /// </remarks>
    /// <exception cref="ObjectDisposedException">Thrown when the owning connection has been disposed.</exception>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library returns an error status.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    void SetPs2EmulationEnabled(bool enabled);

    /// <summary>
    /// Sets the fingerprint reader LED brightness as a percentage.
    /// </summary>
    /// <param name="brightness">The target brightness as a percentage-like ratio (0–100%). The value is rounded to the nearest whole percent before it is sent to the embedded controller.</param>
    /// <remarks>
    /// This is the fine-grained counterpart to <see cref="IFrameworkEcConnection.SetFingerprintLed(FrameworkFingerprintLedLevel)"/>,
    /// which selects one of the discrete firmware levels instead. The embedded controller changes the power-button
    /// fingerprint reader LED brightness immediately.
    /// <para>
    /// Upstream framework-system does not document whether the brightness survives an EC reset or a power cycle, and
    /// the embedded controller may override the value whenever it drives the LED for its own status indications.
    /// Treat it as volatile embedded controller state and re-apply it as needed.
    /// </para>
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="brightness"/> is not a finite value inside the 0–100 percent range.</exception>
    /// <exception cref="ObjectDisposedException">Thrown when the owning connection has been disposed.</exception>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library returns an error status.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    /// <seealso cref="IFrameworkEcConnection.SetFingerprintLed(FrameworkFingerprintLedLevel)"/>
    void SetFingerprintLedBrightness(Ratio brightness);
}
