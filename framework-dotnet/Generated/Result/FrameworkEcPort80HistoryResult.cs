using System.Buffers.Binary;

using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Snapshots;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcPort80HistoryResult
{
    private const int EntrySizeInBytes = sizeof(ushort);

    internal readonly FrameworkEcPort80HistorySnapshot GetValueOrThrow()
    {
        var buffer = codes;

        if (status.IsFailure)
        {
            try
            {
                throw FrameworkStatusException.GetCorrectException(status);
            }
            finally
            {
                buffer.Free();
            }
        }

        try
        {
            // The native buffer holds history_size little-endian u16 entries in buffer order.
            // Decode explicitly rather than reinterpreting the bytes so the layout stays correct
            // regardless of host endianness.
            var rawBytes = buffer.AsSpan();
            var entryCount = rawBytes.Length / EntrySizeInBytes;
            var entries = new ushort[entryCount];

            for (var index = 0; index < entryCount; index++)
            {
                entries[index] = BinaryPrimitives.ReadUInt16LittleEndian(rawBytes.Slice(index * EntrySizeInBytes, EntrySizeInBytes));
            }

            return new FrameworkEcPort80HistorySnapshot(writes, history_size, entries);
        }
        finally
        {
            buffer.Free();
        }
    }
}
