using System;
using System.Globalization;

namespace FrameworkDotnet.Exceptions;

/// <summary>
/// Represents a GPU descriptor CRC32 validation failure.
/// </summary>
public class FrameworkGpuDescriptorChecksumException : FrameworkException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkGpuDescriptorChecksumException"/> class.
    /// </summary>
    /// <param name="componentName">The GPU descriptor component whose CRC32 validation failed.</param>
    /// <param name="expectedChecksum">The CRC32 reported by the descriptor.</param>
    /// <param name="actualChecksum">The CRC32 computed from the descriptor bytes.</param>
    internal FrameworkGpuDescriptorChecksumException(string componentName, uint expectedChecksum, uint actualChecksum)
    {
        ArgumentException.ThrowIfNullOrEmpty(componentName);

        ComponentName = componentName;
        ExpectedChecksum = expectedChecksum;
        ActualChecksum = actualChecksum;
    }

    /// <summary>
    /// Gets the GPU descriptor component whose CRC32 validation failed.
    /// </summary>
    public string ComponentName { get; }

    /// <summary>
    /// Gets the CRC32 reported by the descriptor.
    /// </summary>
    public uint ExpectedChecksum { get; }

    /// <summary>
    /// Gets the CRC32 computed from the descriptor bytes.
    /// </summary>
    public uint ActualChecksum { get; }

    /// <inheritdoc/>
    public override string Message => $"{ComponentName} CRC32 is invalid. Expected 0x{ExpectedChecksum.ToString("X8", CultureInfo.InvariantCulture)}, but computed 0x{ActualChecksum.ToString("X8", CultureInfo.InvariantCulture)}.";
}
