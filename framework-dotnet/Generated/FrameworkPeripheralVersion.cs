using ManagedPeripheralVersionSnapshot = FrameworkDotnet.Snapshots.FrameworkPeripheralVersionSnapshot;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkPeripheralVersion
{
    /// <summary>
    /// Copies one peripheral slot into managed memory without releasing the native product name buffer.
    /// </summary>
    /// <param name="slotIndex">The zero-based fixed slot this record occupies in the enclosing result.</param>
    /// <returns>A managed snapshot holding its own copy of every value in the slot.</returns>
    /// <remarks>
    /// The <c>product_name</c> buffer is owned by the enclosing <see cref="FrameworkPeripheralVersionsResult"/>
    /// and must be released as part of that aggregate through <c>framework_peripheral_versions_free</c>.
    /// This method therefore copies the string with <see cref="FrameworkByteBuffer.ToUtf8String"/> and must
    /// never free the buffer itself.
    /// </remarks>
    internal readonly ManagedPeripheralVersionSnapshot ToManagedSnapshot(int slotIndex)
    {
        return new ManagedPeripheralVersionSnapshot(
            slotIndex,
            present != 0,
            version_major,
            version_minor,
            version_sub_minor,
            vendor_id,
            product_id,
            product_name.ToUtf8String());
    }
}
