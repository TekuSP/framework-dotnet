using System.Globalization;

using FrameworkDotnet.Enums;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents a numbered expansion card slot descriptor with full Power Delivery port state and card type identification.
/// </summary>
public sealed record FrameworkExpansionCardSlotSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkExpansionCardSlotSnapshot"/> class.
    /// </summary>
    /// <param name="identity">The best-effort module classification.</param>
    /// <param name="bus">The source or bus that produced the observation.</param>
    /// <param name="slotKind">The logical slot category.</param>
    /// <param name="confidence">The slot-assignment classification confidence.</param>
    /// <param name="isPresent">A value indicating whether the slot appears populated.</param>
    /// <param name="slotIndex">The zero-based slot index (0–5).</param>
    /// <param name="flags">Additional module flags.</param>
    /// <param name="vendorId">The observed USB/HID vendor ID, when available.</param>
    /// <param name="productId">The observed USB/HID product ID, when available.</param>
    /// <param name="boardId">The board-specific numeric identifier, when available.</param>
    /// <param name="powerDelivery">The full USB Power Delivery port state for this slot.</param>
    /// <param name="cardType">The identified card type.</param>
    /// <param name="cardConfidence">The confidence in the card-type identification.</param>
    public FrameworkExpansionCardSlotSnapshot(FrameworkModuleIdentity identity, FrameworkModuleBus bus, FrameworkModuleSlotKind slotKind, FrameworkModuleConfidence confidence, bool isPresent, int slotIndex, FrameworkModuleFlags flags, uint vendorId, uint productId, int boardId, FrameworkPowerDeliveryPortStateSnapshot powerDelivery, FrameworkExpansionCardType cardType, FrameworkModuleConfidence cardConfidence)
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
        PowerDelivery = powerDelivery;
        CardType = cardType;
        CardConfidence = cardConfidence;
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
    /// Gets the slot-assignment classification confidence.
    /// </summary>
    public FrameworkModuleConfidence Confidence { get; init; }

    /// <summary>
    /// Gets a value indicating whether the slot appears populated.
    /// </summary>
    public bool IsPresent { get; init; }

    /// <summary>
    /// Gets the zero-based slot index (0–5).
    /// </summary>
    public int SlotIndex { get; init; }

    /// <summary>
    /// Gets additional module flags.
    /// </summary>
    public FrameworkModuleFlags Flags { get; init; }

    /// <summary>
    /// Gets the observed USB/HID vendor ID, when available.
    /// </summary>
    public uint VendorId { get; init; }

    /// <summary>
    /// Gets the observed USB/HID product ID, when available.
    /// </summary>
    public uint ProductId { get; init; }

    /// <summary>
    /// Gets the board-specific numeric identifier, when available.
    /// </summary>
    public int BoardId { get; init; }

    /// <summary>
    /// Gets the full USB Power Delivery port state for this expansion card slot.
    /// </summary>
    public FrameworkPowerDeliveryPortStateSnapshot PowerDelivery { get; init; }

    /// <summary>
    /// Gets the identified card type.
    /// </summary>
    public FrameworkExpansionCardType CardType { get; init; }

    /// <summary>
    /// Gets the confidence in the card-type identification, independent of slot-assignment confidence.
    /// </summary>
    public FrameworkModuleConfidence CardConfidence { get; init; }

    public override string ToString()
    {
        return $"Expansion Card Slot {SlotIndex.ToString(CultureInfo.InvariantCulture)}: Present: {IsPresent}, Card Type: {CardType}, Identity: {Identity}, Confidence: {Confidence}, Power Delivery: [{PowerDelivery}]";
    }
}
