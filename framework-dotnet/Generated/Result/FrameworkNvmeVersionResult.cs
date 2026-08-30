using FrameworkDotnet.Exceptions;

using ManagedNvmeVersionSnapshot = FrameworkDotnet.Snapshots.FrameworkNvmeVersionSnapshot;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkNvmeVersionResult
{
    /// <summary>
    /// Copies both identity strings into managed memory and releases the native buffers exactly once.
    /// </summary>
    /// <returns>A managed snapshot that shares no memory with the native record.</returns>
    /// <remarks>
    /// Unlike <see cref="FrameworkPeripheralVersionsResult"/>, this record has no aggregate free. Both
    /// buffers are released individually through the plain <c>framework_byte_buffer_free</c> path, on the
    /// throwing path as well as the successful one. On a non-Linux host the native layer answers with
    /// <see cref="FrameworkStatusCode.NotSupported"/> and two empty buffers, which release cleanly.
    /// </remarks>
    internal readonly ManagedNvmeVersionSnapshot GetValueOrThrow()
    {
        FrameworkByteBuffer modelNumberBuffer = model_number;
        FrameworkByteBuffer firmwareVersionBuffer = firmware_version;

        try
        {
            if (status.IsFailure)
            {
                throw FrameworkStatusException.GetCorrectException(status);
            }

            return new ManagedNvmeVersionSnapshot(
                modelNumberBuffer.ToUtf8String(),
                firmwareVersionBuffer.ToUtf8String());
        }
        finally
        {
            modelNumberBuffer.Free();
            firmwareVersionBuffer.Free();
        }
    }
}
