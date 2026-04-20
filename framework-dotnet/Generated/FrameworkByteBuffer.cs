using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkByteBuffer : IDisposable
{
    public readonly bool HasValue => ptr != null && length > 0;

    public readonly Span<byte> AsSpan()
    {
        if (ptr == null || length <= 0)
        {
            return [];
        }

        return new Span<byte>(ptr, length);
    }

    public readonly Span<T> AsSpan<T>() where T : unmanaged
    {
        if (ptr == null || length <= 0)
        {
            return [];
        }

        var elementSize = Unsafe.SizeOf<T>();
        if (length % elementSize != 0)
        {
            throw new InvalidOperationException($"Buffer length {length} is not aligned for {typeof(T).Name}.");
        }

        return MemoryMarshal.CreateSpan(ref Unsafe.AsRef<T>(ptr), length / elementSize);
    }

    public readonly byte[] ToArray()
    {
        return AsSpan().ToArray();
    }

    public readonly T[] ToArray<T>() where T : unmanaged
    {
        return AsSpan<T>().ToArray();
    }

    public readonly string ToUtf8String()
    {
        return Encoding.UTF8.GetString(AsSpan());
    }

    public void Free()
    {
        if (ptr == null)
        {
            length = 0;
            capacity = 0;
            return;
        }

        NativeMethods.framework_byte_buffer_free(this);
        ptr = null;
        length = 0;
        capacity = 0;
    }

    public void Dispose()
    {
        Free();
    }
}
