using System;
using System.Globalization;

namespace FrameworkDotnet.Exceptions;

/// <summary>
/// Represents a GPU descriptor length validation failure.
/// </summary>
public class FrameworkGpuDescriptorLengthException : FrameworkException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkGpuDescriptorLengthException"/> class.
    /// </summary>
    /// <param name="componentName">The GPU descriptor component whose length validation failed.</param>
    /// <param name="expectedLength">The expected length in bytes.</param>
    /// <param name="actualLength">The actual length in bytes.</param>
    internal FrameworkGpuDescriptorLengthException(string componentName, ulong expectedLength, ulong actualLength)
    {
        ArgumentException.ThrowIfNullOrEmpty(componentName);

        ComponentName = componentName;
        ExpectedLength = expectedLength;
        ActualLength = actualLength;
    }

    /// <summary>
    /// Gets the GPU descriptor component whose length validation failed.
    /// </summary>
    public string ComponentName { get; }

    /// <summary>
    /// Gets the expected length in bytes.
    /// </summary>
    public ulong ExpectedLength { get; }

    /// <summary>
    /// Gets the actual length in bytes.
    /// </summary>
    public ulong ActualLength { get; }

    /// <inheritdoc/>
    public override string Message => $"{ComponentName} length is invalid. Expected {ExpectedLength.ToString(CultureInfo.InvariantCulture)} bytes, but got {ActualLength.ToString(CultureInfo.InvariantCulture)} bytes.";
}
