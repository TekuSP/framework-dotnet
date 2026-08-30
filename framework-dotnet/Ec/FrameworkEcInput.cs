using System;
using System.Collections.Generic;

using FrameworkDotnet.Attributes;
using FrameworkDotnet.Enums;
using FrameworkDotnet.Interfaces;
using FrameworkDotnet.Snapshots;

using UnitsNet;

using Native = Framework.System.Interop;

namespace FrameworkDotnet.Ec;

/// <summary>
/// Implements the embedded controller input-device controls on top of a borrowed embedded controller handle.
/// </summary>
/// <remarks>
/// The facet does not own the native handle. It reads it through the accessor supplied at construction time, so the
/// owning connection keeps sole responsibility for opening, disposal and disposed-state checks.
/// </remarks>
internal sealed class FrameworkEcInput : IFrameworkEcInput
{
    /// <summary>
    /// The maximum number of per-key colors the embedded controller accepts in a single set-color command.
    /// </summary>
    private const int MaxColorsPerCall = 64;

    /// <summary>
    /// The number of bytes the embedded controller expects per key color: red, green and blue.
    /// </summary>
    private const int BytesPerColor = 3;

    /// <summary>
    /// The largest value the native layer can accept for an argument that is marshalled as a single byte.
    /// </summary>
    private const int MaxByteValue = 255;

    private readonly Func<IntPtr> handleAccessor;

    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkEcInput"/> class.
    /// </summary>
    /// <param name="handleAccessor">
    /// A callback that returns the native embedded controller handle owned by the connection. The owning connection
    /// is expected to perform its own disposed-state validation inside the callback and to return
    /// <see cref="IntPtr.Zero"/> only when no usable handle exists.
    /// </param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="handleAccessor"/> is <see langword="null"/>.</exception>
    internal FrameworkEcInput(Func<IntPtr> handleAccessor)
    {
        ArgumentNullException.ThrowIfNull(handleAccessor);

        this.handleAccessor = handleAccessor;
    }

    /// <inheritdoc/>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.FrameworkDesktop, Message = "Upstream framework-system documents the RGB LED surface on Framework Desktop only. The Framework Laptop 16 keyboard is not driven by the embedded controller.")]
    public void SetRgbKeyboardColors(int startKey, IReadOnlyList<FrameworkKeyboardColor> colors)
    {
        ArgumentNullException.ThrowIfNull(colors);
        ArgumentOutOfRangeException.ThrowIfNegative(startKey);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(startKey, MaxByteValue);

        // Read Count exactly once. It is a caller-supplied interface, so a concurrent mutation
        // between the validation, the allocation and the length handed to native would let the
        // FFI read past the pinned buffer - native only rejects a negative count.
        int count = colors.Count;
        ArgumentOutOfRangeException.ThrowIfZero(count, nameof(colors));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(count, MaxColorsPerCall, nameof(colors));

        byte[] flattened = new byte[count * BytesPerColor];

        for (int index = 0; index < count; index++)
        {
            FrameworkKeyboardColor color = colors[index];
            int offset = index * BytesPerColor;

            flattened[offset] = color.Red;
            flattened[offset + 1] = color.Green;
            flattened[offset + 2] = color.Blue;
        }

        unsafe
        {
            fixed (byte* colorPointer = flattened)
            {
                Native.NativeMethods.framework_ec_set_rgb_keyboard_colors(HandlePointer, (byte)startKey, colorPointer, count).ThrowIfFailure();
            }
        }
    }

    /// <inheritdoc/>
    public void RemapKey(int row, int column, ushort scanCode)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(row);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(row, MaxByteValue);
        ArgumentOutOfRangeException.ThrowIfNegative(column);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(column, MaxByteValue);

        unsafe
        {
            Native.NativeMethods.framework_ec_remap_key(HandlePointer, (byte)row, (byte)column, scanCode).ThrowIfFailure();
        }
    }

    /// <inheritdoc/>
    public void RemapCapsLockToControl()
    {
        unsafe
        {
            Native.NativeMethods.framework_ec_remap_caps_to_ctrl(HandlePointer).ThrowIfFailure();
        }
    }

    /// <inheritdoc/>
    public void SetPs2EmulationEnabled(bool enabled)
    {
        unsafe
        {
            Native.NativeMethods.framework_ec_ps2_emulation_enable(HandlePointer, enabled).ThrowIfFailure();
        }
    }

    /// <inheritdoc/>
    public void SetFingerprintLedBrightness(Ratio brightness)
    {
        double percent = brightness.Percent;

        if (!double.IsFinite(percent))
        {
            throw new ArgumentOutOfRangeException(nameof(brightness), percent, "The fingerprint LED brightness must be a finite percentage between 0 and 100.");
        }

        ArgumentOutOfRangeException.ThrowIfNegative(percent, nameof(brightness));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(percent, 100.0, nameof(brightness));

        unsafe
        {
            Native.NativeMethods.framework_ec_set_fingerprint_led_percentage(HandlePointer, (byte)Math.Round(percent)).ThrowIfFailure();
        }
    }

    private unsafe Native.FrameworkEcHandle* HandlePointer
    {
        get
        {
            IntPtr handle = handleAccessor();

            if (handle == IntPtr.Zero)
            {
                throw new ObjectDisposedException(nameof(IFrameworkEcInput), "The embedded controller connection that owns this input facet has been disposed.");
            }

            return (Native.FrameworkEcHandle*)handle;
        }
    }
}
