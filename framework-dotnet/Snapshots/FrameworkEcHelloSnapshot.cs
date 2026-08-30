using System.Globalization;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents the answer to the embedded controller <c>hello</c> diagnostic command.
/// </summary>
/// <remarks>
/// A healthy controller answers the payload it was sent plus <c>0x01020304</c>, computed with
/// unsigned wraparound. <see cref="IsExpectedEcho"/> reports whether that held.
/// </remarks>
public sealed record FrameworkEcHelloSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkEcHelloSnapshot"/> class.
    /// </summary>
    /// <param name="outData">The payload the embedded controller echoed back.</param>
    /// <param name="isExpectedEcho">A value indicating whether the echoed payload matched the expected transform of the sent payload.</param>
    public FrameworkEcHelloSnapshot(uint outData, bool isExpectedEcho)
    {
        OutData = outData;
        IsExpectedEcho = isExpectedEcho;
    }

    /// <summary>
    /// Gets the payload the embedded controller echoed back.
    /// </summary>
    public uint OutData { get; init; }

    /// <summary>
    /// Gets a value indicating whether the echoed payload matched the expected transform of the sent payload.
    /// </summary>
    /// <remarks>
    /// <see langword="false"/> means the controller answered the command but answered with the
    /// wrong value, which points at a corrupt host command transport rather than at a controller
    /// that is not responding at all.
    /// </remarks>
    public bool IsExpectedEcho { get; init; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"EC Hello: Out Data: 0x{OutData.ToString("X8", CultureInfo.InvariantCulture)}, Expected Echo: {IsExpectedEcho}";
    }
}
