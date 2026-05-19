using System.Globalization;

using FrameworkDotnet.Enums;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents one detected or inferred module descriptor.
/// </summary>
public sealed record FrameworkModuleDescriptorSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkModuleDescriptorSnapshot"/> class.
    /// </summary>
    /// <param name="identity">The best-effort module classification.</param>
    /// <param name="bus">The source or bus that produced the observation.</param>
    /// <param name="slotKind">The logical slot category.</param>
    /// <param name="confidence">The classification confidence.</param>
    /// <param name="isPresent">A value indicating whether the slot or module is considered present.</param>
    /// <param name="slotIndex">The zero-based slot index within its group, when applicable.</param>
    /// <param name="flags">Additional module flags.</param>
    /// <param name="vendorId">The observed vendor ID, when available.</param>
    /// <param name="productId">The observed product ID, when available.</param>
    /// <param name="boardId">The board-specific numeric identifier, when available.</param>
    public FrameworkModuleDescriptorSnapshot(FrameworkModuleIdentity identity, FrameworkModuleBus bus, FrameworkModuleSlotKind slotKind, FrameworkModuleConfidence confidence, bool isPresent, int slotIndex, FrameworkModuleFlags flags, uint vendorId, uint productId, int boardId)
    {
        Identity = identity;
        Bus = bus;
        SlotKind = slotKind;
        Confidence = confidence;
        IsPresent = isPresent;
        SlotIndex = slotIndex;
        Flags = flags;
        VendorId = vendorId;
        ProductId = productId;
        BoardId = boardId;
    }

    /// <summary>
    /// Gets the best-effort module classification.
    /// </summary>
    public FrameworkModuleIdentity Identity { get; init; }

    /// <summary>
    /// Gets the source or bus that produced the observation.
    /// </summary>
    public FrameworkModuleBus Bus { get; init; }

    /// <summary>
    /// Gets the logical slot category.
    /// </summary>
    public FrameworkModuleSlotKind SlotKind { get; init; }

    /// <summary>
    /// Gets the classification confidence.
    /// </summary>
    public FrameworkModuleConfidence Confidence { get; init; }

    /// <summary>
    /// Gets a value indicating whether the slot or module is considered present.
    /// </summary>
    public bool IsPresent { get; init; }

    /// <summary>
    /// Gets the zero-based slot index within its group, when applicable.
    /// </summary>
    public int SlotIndex { get; init; }

    /// <summary>
    /// Gets additional module flags.
    /// </summary>
    public FrameworkModuleFlags Flags { get; init; }

    /// <summary>
    /// Gets the observed vendor ID, when available.
    /// </summary>
    public uint VendorId { get; init; }

    /// <summary>
    /// Gets the observed product ID, when available.
    /// </summary>
    public uint ProductId { get; init; }

    /// <summary>
    /// Gets the board-specific numeric identifier, when available.
    /// </summary>
    public int BoardId { get; init; }

    public override string ToString()
    {
        return $"Module Descriptor: Identity: {Identity}, Present: {IsPresent}, Slot Kind: {SlotKind}, Slot Index: {SlotIndex.ToString(CultureInfo.InvariantCulture)}, Bus: {Bus}, Confidence: {Confidence}, Flags: {Flags}, Vendor ID: 0x{VendorId.ToString("X", CultureInfo.InvariantCulture)}, Product ID: 0x{ProductId.ToString("X", CultureInfo.InvariantCulture)}, Board ID: {BoardId.ToString(CultureInfo.InvariantCulture)}";
    }
}
