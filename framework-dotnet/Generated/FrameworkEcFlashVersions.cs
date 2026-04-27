using FrameworkDotnet.Generated;
using ManagedFlashSnapshot = FrameworkDotnet.Snapshots.FrameworkEcFlashSnapshot;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcFlashVersions
{
    public readonly string GetRoVersion()
    {
        fixed (byte* value = ro_version)
        {
            return InteropStringHelpers.ReadNullTerminatedUtf8(value, 32);
        }
    }

    public readonly string GetRwVersion()
    {
        fixed (byte* value = rw_version)
        {
            return InteropStringHelpers.ReadNullTerminatedUtf8(value, 32);
        }
    }

    internal readonly ManagedFlashSnapshot ToManagedSnapshot()
    {
        return new ManagedFlashSnapshot(ToManagedCurrentImage(), GetRoVersion(), GetRwVersion());
    }

    private readonly global::FrameworkDotnet.Enums.FrameworkEcCurrentImage ToManagedCurrentImage()
    {
        return (global::FrameworkDotnet.Enums.FrameworkEcCurrentImage)(int)current_image;
    }
}
