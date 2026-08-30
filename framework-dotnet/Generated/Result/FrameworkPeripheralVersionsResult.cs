using FrameworkDotnet.Exceptions;

using ManagedPeripheralVersionsSnapshot = FrameworkDotnet.Snapshots.FrameworkPeripheralVersionsSnapshot;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkPeripheralVersionsResult
{
    /// <summary>
    /// Copies every peripheral slot into managed memory and releases the native record exactly once.
    /// </summary>
    /// <returns>A managed snapshot that shares no memory with the native record.</returns>
    /// <remarks>
    /// <para>
    /// This is the single release path for the four native entry points that return this record
    /// (<c>framework_get_camera_versions</c>, <c>framework_get_input_module_versions</c>,
    /// <c>framework_get_usb_hub_versions</c> and <c>framework_get_audio_card_version</c>). The record owns
    /// eight <c>product_name</c> buffers that must be released as a whole through
    /// <c>framework_peripheral_versions_free</c>; the individual buffers must never be released with
    /// <c>framework_byte_buffer_free</c>, and the aggregate must never be released twice.
    /// </para>
    /// <para>
    /// The record is copied into a local first so the native free receives a writable address, and the free
    /// runs in a <see langword="finally"/> block so the buffers are released on the throwing path as well.
    /// Every string is copied into managed memory before the free, so the returned snapshot stays valid
    /// afterwards.
    /// </para>
    /// </remarks>
    internal readonly ManagedPeripheralVersionsSnapshot GetValueOrThrow()
    {
        FrameworkPeripheralVersionsResult owned = this;

        try
        {
            if (owned.status.IsFailure)
            {
                throw FrameworkStatusException.GetCorrectException(owned.status);
            }

            return new ManagedPeripheralVersionsSnapshot(
                owned.count,
                owned.peripheral_0.ToManagedSnapshot(0),
                owned.peripheral_1.ToManagedSnapshot(1),
                owned.peripheral_2.ToManagedSnapshot(2),
                owned.peripheral_3.ToManagedSnapshot(3),
                owned.peripheral_4.ToManagedSnapshot(4),
                owned.peripheral_5.ToManagedSnapshot(5),
                owned.peripheral_6.ToManagedSnapshot(6),
                owned.peripheral_7.ToManagedSnapshot(7));
        }
        finally
        {
            NativeMethods.framework_peripheral_versions_free(&owned);
        }
    }
}
