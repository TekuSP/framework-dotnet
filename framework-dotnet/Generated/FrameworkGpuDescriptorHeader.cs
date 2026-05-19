using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO.Hashing;
using System.Text;

using FrameworkDotnet.Enums;
using FrameworkDotnet.Exceptions;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkGpuDescriptorHeader
{
    private const int FixedHeaderLength = 48;
    private const int HeaderBytesBeforeCrc = FixedHeaderLength - sizeof(uint);

    private static readonly byte[] EmptyMagicBytes = [0x00, 0x00, 0x00, 0x00];
    private static readonly byte[] FrameworkExpansionBayMagicBytes = [0x32, 0xAC, 0x00, 0x00];

    internal readonly FrameworkGpuDescriptorMagic GetBayType()
    {
        return ToManagedMagic(GetMagicBytes());
    }

    internal readonly (IReadOnlyList<byte> RawMagicBytes, FrameworkGpuDescriptorMagic BayType, Version DescriptorVersion, Version HardwareVersion, string Serial, IReadOnlyList<byte> Header, IReadOnlyList<byte> Payload) ToManagedDescriptor()
    {
        return CreateManagedDescriptor(GetSerializedHeaderBytes(), Array.Empty<byte>());
    }

    internal readonly (IReadOnlyList<byte> RawMagicBytes, FrameworkGpuDescriptorMagic BayType, Version DescriptorVersion, Version HardwareVersion, string Serial, IReadOnlyList<byte> Header, IReadOnlyList<byte> Payload) ToManagedDescriptor(byte[] rawDescriptorBytes)
    {
        ArgumentNullException.ThrowIfNull(rawDescriptorBytes);

        ValidateDescriptor(rawDescriptorBytes, out byte[] headerBytes, out byte[] payloadBytes);
        return CreateManagedDescriptor(headerBytes, payloadBytes);
    }

    private readonly (IReadOnlyList<byte> RawMagicBytes, FrameworkGpuDescriptorMagic BayType, Version DescriptorVersion, Version HardwareVersion, string Serial, IReadOnlyList<byte> Header, IReadOnlyList<byte> Payload) CreateManagedDescriptor(byte[] headerBytes, byte[] payloadBytes)
    {
        var rawMagicBytes = GetMagicBytes();

        return (
            rawMagicBytes,
            ToManagedMagic(rawMagicBytes),
            new Version(desc_ver_major, desc_ver_minor),
            new Version(hardware_version, hardware_revision),
            GetAsciiString(GetSerialBytes()),
            headerBytes,
            payloadBytes);
    }

    private readonly void ValidateDescriptor(byte[] rawDescriptorBytes, out byte[] headerBytes, out byte[] payloadBytes)
    {
        if (length != FixedHeaderLength)
        {
            throw new FrameworkGpuDescriptorLengthException("GPU descriptor header", FixedHeaderLength, length);
        }

        var expectedTotalLength = (ulong)length + descriptor_length;
        if ((ulong)rawDescriptorBytes.LongLength != expectedTotalLength)
        {
            throw new FrameworkGpuDescriptorLengthException("GPU descriptor blob", expectedTotalLength, (ulong)rawDescriptorBytes.LongLength);
        }

        int headerLength = checked((int)length);
        headerBytes = rawDescriptorBytes.AsSpan(0, headerLength).ToArray();
        payloadBytes = rawDescriptorBytes.AsSpan(headerLength).ToArray();

        if ((ulong)payloadBytes.LongLength != descriptor_length)
        {
            throw new FrameworkGpuDescriptorLengthException("GPU descriptor payload", descriptor_length, (ulong)payloadBytes.LongLength);
        }

        uint actualHeaderChecksum = Crc32.HashToUInt32(headerBytes.AsSpan(0, HeaderBytesBeforeCrc));
        if (actualHeaderChecksum != crc32)
        {
            throw new FrameworkGpuDescriptorChecksumException("GPU descriptor header", crc32, actualHeaderChecksum);
        }

        uint actualDescriptorChecksum = Crc32.HashToUInt32(payloadBytes);
        if (actualDescriptorChecksum != descriptor_crc32)
        {
            throw new FrameworkGpuDescriptorChecksumException("GPU descriptor payload", descriptor_crc32, actualDescriptorChecksum);
        }
    }

    private readonly byte[] GetSerializedHeaderBytes()
    {
        var bytes = new byte[FixedHeaderLength];
        GetMagicBytes().CopyTo(bytes, 0);

        BinaryPrimitives.WriteUInt32LittleEndian(bytes.AsSpan(4, sizeof(uint)), length);
        BinaryPrimitives.WriteUInt16LittleEndian(bytes.AsSpan(8, sizeof(ushort)), desc_ver_major);
        BinaryPrimitives.WriteUInt16LittleEndian(bytes.AsSpan(10, sizeof(ushort)), desc_ver_minor);
        BinaryPrimitives.WriteUInt16LittleEndian(bytes.AsSpan(12, sizeof(ushort)), hardware_version);
        BinaryPrimitives.WriteUInt16LittleEndian(bytes.AsSpan(14, sizeof(ushort)), hardware_revision);
        GetSerialBytes().CopyTo(bytes, 16);
        BinaryPrimitives.WriteUInt32LittleEndian(bytes.AsSpan(36, sizeof(uint)), descriptor_length);
        BinaryPrimitives.WriteUInt32LittleEndian(bytes.AsSpan(40, sizeof(uint)), descriptor_crc32);
        BinaryPrimitives.WriteUInt32LittleEndian(bytes.AsSpan(44, sizeof(uint)), crc32);

        return bytes;
    }

    private static FrameworkGpuDescriptorMagic ToManagedMagic(byte[] rawMagicBytes)
    {
        if (rawMagicBytes.AsSpan().SequenceEqual(EmptyMagicBytes))
        {
            return FrameworkGpuDescriptorMagic.NoExpansionBay;
        }

        if (rawMagicBytes.AsSpan().SequenceEqual(FrameworkExpansionBayMagicBytes))
        {
            return FrameworkGpuDescriptorMagic.FrameworkExpansionBay;
        }

        return FrameworkGpuDescriptorMagic.Unknown;
    }

    private readonly byte[] GetMagicBytes()
    {
        fixed (byte* magicPointer = magic)
        {
            return new ReadOnlySpan<byte>(magicPointer, 4).ToArray();
        }
    }

    private readonly byte[] GetSerialBytes()
    {
        fixed (byte* serialPointer = serial)
        {
            return new ReadOnlySpan<byte>(serialPointer, 20).ToArray();
        }
    }

    private static string GetAsciiString(byte[] value)
    {
        return Encoding.ASCII.GetString(value).TrimEnd('\0');
    }
}
