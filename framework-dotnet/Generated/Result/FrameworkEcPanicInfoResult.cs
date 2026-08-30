using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Snapshots;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcPanicInfoResult
{
    internal readonly FrameworkEcPanicInfoSnapshot GetValueOrThrow()
    {
        var buffer = data;

        if (status.IsFailure)
        {
            try
            {
                throw FrameworkStatusException.GetCorrectException(status);
            }
            finally
            {
                buffer.Free();
            }
        }

        return new FrameworkEcPanicInfoSnapshot(
            buffer.ToArrayAndFree(),
            arch,
            struct_version,
            flags,
            is_valid != 0,
            struct_size,
            magic);
    }
}
