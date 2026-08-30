using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents the five raw Smart Battery lifetime data blocks read through manufacturer access.
/// </summary>
/// <remarks>
/// The blocks are only readable once the pack has been unsealed, so this snapshot is only produced when <see cref="FrameworkSmartBatterySnapshot.IsUnsealed"/> is <see langword="true"/>. Their layout is defined by the pack's gas-gauge firmware and varies between vendors, so the bytes are surfaced verbatim rather than decoded.
/// </remarks>
public sealed record FrameworkBatteryLifetimeDataSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkBatteryLifetimeDataSnapshot"/> class.
    /// </summary>
    /// <param name="block_1">The first raw lifetime data block.</param>
    /// <param name="block_2">The second raw lifetime data block.</param>
    /// <param name="block_3">The third raw lifetime data block.</param>
    /// <param name="block_4">The fourth raw lifetime data block.</param>
    /// <param name="block_5">The fifth raw lifetime data block.</param>
    public FrameworkBatteryLifetimeDataSnapshot(
        IReadOnlyList<byte> block_1,
        IReadOnlyList<byte> block_2,
        IReadOnlyList<byte> block_3,
        IReadOnlyList<byte> block_4,
        IReadOnlyList<byte> block_5)
    {
        Block_1 = block_1;
        Block_2 = block_2;
        Block_3 = block_3;
        Block_4 = block_4;
        Block_5 = block_5;
    }

    /// <summary>
    /// Gets the first raw lifetime data block.
    /// </summary>
    public IReadOnlyList<byte> Block_1 { get; init; }

    /// <summary>
    /// Gets the second raw lifetime data block.
    /// </summary>
    public IReadOnlyList<byte> Block_2 { get; init; }

    /// <summary>
    /// Gets the third raw lifetime data block.
    /// </summary>
    public IReadOnlyList<byte> Block_3 { get; init; }

    /// <summary>
    /// Gets the fourth raw lifetime data block.
    /// </summary>
    public IReadOnlyList<byte> Block_4 { get; init; }

    /// <summary>
    /// Gets the fifth raw lifetime data block.
    /// </summary>
    public IReadOnlyList<byte> Block_5 { get; init; }

    /// <summary>
    /// Gets all five raw lifetime data blocks in index order.
    /// </summary>
    public IReadOnlyList<IReadOnlyList<byte>> Blocks => [Block_1, Block_2, Block_3, Block_4, Block_5];

    public override string ToString()
    {
        return $"Battery Lifetime Data: Block Lengths: {string.Join(", ", Blocks.Select(block => block.Count.ToString(CultureInfo.InvariantCulture)))}";
    }
}
