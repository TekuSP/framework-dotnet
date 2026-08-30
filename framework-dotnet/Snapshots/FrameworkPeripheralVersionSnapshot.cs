using System;
using System.Globalization;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents one USB or HID peripheral and the firmware version it reports.
/// </summary>
/// <remarks>
/// <para>
/// The peripheral occupies a fixed slot in the enclosing <see cref="FrameworkPeripheralVersionsSnapshot"/>,
/// so an empty slot is still reported. When <see cref="IsPresent"/> is <see langword="false"/> no device
/// was found for that slot and every other member carries no meaningful data.
/// </para>
/// <para>
/// For cameras, Framework 16 input modules and USB hubs the version is decoded from the USB
/// <c>bcdDevice</c> descriptor field; for the audio expansion card it comes from the Synaptics CAPE
/// version command instead. Both are reported through the same three components.
/// </para>
/// </remarks>
public sealed record FrameworkPeripheralVersionSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkPeripheralVersionSnapshot"/> class.
    /// </summary>
    /// <param name="slotIndex">The zero-based fixed slot the peripheral occupies.</param>
    /// <param name="isPresent">A value indicating whether the slot is populated.</param>
    /// <param name="major">The major component of the reported firmware version.</param>
    /// <param name="minor">The minor component of the reported firmware version.</param>
    /// <param name="subMinor">The sub-minor component of the reported firmware version.</param>
    /// <param name="vendorId">The USB vendor identifier of the peripheral.</param>
    /// <param name="productId">The USB product identifier of the peripheral.</param>
    /// <param name="productName">The USB product string of the peripheral, or an empty string when it could not be read.</param>
    public FrameworkPeripheralVersionSnapshot(int slotIndex, bool isPresent, byte major, byte minor, byte subMinor, ushort vendorId, ushort productId, string productName)
    {
        SlotIndex = slotIndex;
        IsPresent = isPresent;
        Major = major;
        Minor = minor;
        SubMinor = subMinor;
        VendorId = vendorId;
        ProductId = productId;
        ProductName = productName;
    }

    /// <summary>
    /// Gets the zero-based fixed slot the peripheral occupies.
    /// </summary>
    /// <remarks>
    /// The slot is a position in the native report, not a physical port. It is stable only within a single
    /// read, so do not persist it as a device identity; use <see cref="VendorId"/> and
    /// <see cref="ProductId"/> for that.
    /// </remarks>
    public int SlotIndex { get; init; }

    /// <summary>
    /// Gets a value indicating whether the slot is populated.
    /// </summary>
    /// <remarks>This flag is authoritative. Read the remaining members only when it is <see langword="true"/>.</remarks>
    public bool IsPresent { get; init; }

    /// <summary>
    /// Gets the major component of the reported firmware version.
    /// </summary>
    public byte Major { get; init; }

    /// <summary>
    /// Gets the minor component of the reported firmware version.
    /// </summary>
    public byte Minor { get; init; }

    /// <summary>
    /// Gets the sub-minor component of the reported firmware version.
    /// </summary>
    public byte SubMinor { get; init; }

    /// <summary>
    /// Gets the USB vendor identifier of the peripheral.
    /// </summary>
    public ushort VendorId { get; init; }

    /// <summary>
    /// Gets the USB product identifier of the peripheral.
    /// </summary>
    public ushort ProductId { get; init; }

    /// <summary>
    /// Gets the USB product string of the peripheral, or an empty string when it could not be read.
    /// </summary>
    /// <remarks>
    /// Reading the product string requires opening the device. A device that is present but could not be
    /// opened, typically for want of permission, still reports its version and identifiers with an empty
    /// product name.
    /// </remarks>
    public string ProductName { get; init; }

    /// <summary>
    /// Gets the reported firmware version as a comparable value.
    /// </summary>
    /// <remarks>
    /// The components map onto <see cref="System.Version.Major"/>, <see cref="System.Version.Minor"/> and
    /// <see cref="System.Version.Build"/>; the revision component is unused. The value is only meaningful
    /// when <see cref="IsPresent"/> is <see langword="true"/>.
    /// </remarks>
    public Version Version => new Version(Major, Minor, SubMinor);

    /// <inheritdoc/>
    public override string ToString()
    {
        if (!IsPresent)
        {
            return $"Peripheral {SlotIndex.ToString(CultureInfo.InvariantCulture)}: Not Present";
        }

        string name = string.IsNullOrEmpty(ProductName) ? "Unknown" : ProductName;

        return $"Peripheral {SlotIndex.ToString(CultureInfo.InvariantCulture)} ({name}): {Major.ToString(CultureInfo.InvariantCulture)}.{Minor.ToString(CultureInfo.InvariantCulture)}.{SubMinor.ToString(CultureInfo.InvariantCulture)}, USB {VendorId.ToString("X4", CultureInfo.InvariantCulture)}:{ProductId.ToString("X4", CultureInfo.InvariantCulture)}";
    }
}
