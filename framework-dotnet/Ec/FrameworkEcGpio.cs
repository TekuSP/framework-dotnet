using System;
using System.Collections.Generic;
using System.Text;

using FrameworkDotnet.Interfaces;
using FrameworkDotnet.Snapshots;

using Native = Framework.System.Interop;

namespace FrameworkDotnet.Ec;

/// <summary>
/// Implements the embedded controller general-purpose input/output facet over a live native EC handle.
/// </summary>
/// <remarks>
/// The facet does not own the native handle. It borrows it through the accessor supplied by the owning
/// <see cref="FrameworkEcConnection"/>, so the connection stays the single point of lifetime control and
/// keeps its disposal check on every call.
/// </remarks>
internal sealed class FrameworkEcGpio : IFrameworkEcGpio
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkEcGpio"/> class.
    /// </summary>
    /// <param name="handleAccessor">
    /// A delegate returning the live native embedded controller handle as an <see cref="IntPtr"/>. It is
    /// invoked immediately before every native call, so the owning connection can validate its own state:
    /// the delegate is expected to throw <see cref="ObjectDisposedException"/> once the connection has been
    /// closed, and must never hand back a stale or closed handle.
    /// </param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="handleAccessor"/> is <see langword="null"/>.</exception>
    internal FrameworkEcGpio(Func<IntPtr> handleAccessor)
    {
        ArgumentNullException.ThrowIfNull(handleAccessor);

        this.handleAccessor = handleAccessor;
    }

    /// <inheritdoc/>
    public int GetCount()
    {
        unsafe
        {
            return Native.NativeMethods.framework_ec_get_gpio_count(HandlePointer).GetValueOrThrow();
        }
    }

    /// <inheritdoc/>
    public bool GetValue(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);

        unsafe
        {
            byte* nameBytes = stackalloc byte[MaximumNameLengthInBytes];
            int nameLength = EncodeName(name, new Span<byte>(nameBytes, MaximumNameLengthInBytes));

            return Native.NativeMethods.framework_ec_get_gpio(HandlePointer, nameBytes, nameLength).GetValueOrThrow();
        }
    }

    /// <inheritdoc/>
    public void SetValue(string name, bool value)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);

        unsafe
        {
            byte* nameBytes = stackalloc byte[MaximumNameLengthInBytes];
            int nameLength = EncodeName(name, new Span<byte>(nameBytes, MaximumNameLengthInBytes));

            Native.NativeMethods.framework_ec_set_gpio(HandlePointer, nameBytes, nameLength, value).ThrowIfFailure();
        }
    }

    /// <inheritdoc/>
    public FrameworkEcGpioSnapshot GetInfo(int index)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(index);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(index, byte.MaxValue);

        unsafe
        {
            return Native.NativeMethods.framework_ec_get_gpio_info(HandlePointer, (byte)index).GetValueOrThrow();
        }
    }

    /// <inheritdoc/>
    public IReadOnlyList<FrameworkEcGpioSnapshot> GetAll()
    {
        int count = GetCount();
        List<FrameworkEcGpioSnapshot> snapshots = new(count);

        for (int index = 0; index < count; index++)
        {
            snapshots.Add(GetInfo(index));
        }

        return snapshots;
    }

    /// <summary>
    /// Encodes a GPIO line name into the caller-supplied buffer as UTF-8 without a terminating NUL.
    /// </summary>
    /// <param name="name">The line name to encode.</param>
    /// <param name="destination">The buffer receiving the encoded bytes.</param>
    /// <returns>The number of bytes written, which is the length the native layer expects.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> encodes to more than <see cref="MaximumNameLengthInBytes"/> UTF-8 bytes.</exception>
    private static int EncodeName(string name, Span<byte> destination)
    {
        int byteCount = Encoding.UTF8.GetByteCount(name);

        if (byteCount > MaximumNameLengthInBytes)
        {
            throw new ArgumentException($"The GPIO name must encode to at most {MaximumNameLengthInBytes} UTF-8 bytes, but '{name}' requires {byteCount}. Longer names are truncated by the embedded controller and would address the wrong line.", nameof(name));
        }

        return Encoding.UTF8.GetBytes(name, destination);
    }

    private unsafe Native.FrameworkEcHandle* HandlePointer => (Native.FrameworkEcHandle*)handleAccessor();

    /// <summary>
    /// The largest GPIO line name the embedded controller host command can carry, in UTF-8 bytes.
    /// </summary>
    private const int MaximumNameLengthInBytes = 32;

    private readonly Func<IntPtr> handleAccessor;
}
