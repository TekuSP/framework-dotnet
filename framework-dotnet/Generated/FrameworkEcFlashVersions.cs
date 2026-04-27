using ManagedFlashSnapshot = FrameworkDotnet.Snapshots.FrameworkEcFlashSnapshot;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcFlashVersions
{
    internal ManagedFlashSnapshot ToManagedSnapshot()
    {
        var roVersion = ro_version;
        var rwVersion = rw_version;

        return new ManagedFlashSnapshot(ToManagedCurrentImage(), roVersion.ToUtf8StringAndFree(), rwVersion.ToUtf8StringAndFree());
    }

    private readonly global::FrameworkDotnet.Enums.FrameworkEcCurrentImage ToManagedCurrentImage()
    {
        return (global::FrameworkDotnet.Enums.FrameworkEcCurrentImage)(int)current_image;
    }
}
