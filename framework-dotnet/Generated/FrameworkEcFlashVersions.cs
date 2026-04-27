using System;

using ManagedFlashSnapshot = FrameworkDotnet.Snapshots.FrameworkEcFlashSnapshot;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcFlashVersions
{
    internal readonly ManagedFlashSnapshot ToManagedSnapshot()
    {
        var roVersion = ro_version;
        var rwVersion = rw_version;

        return new ManagedFlashSnapshot(ToManagedCurrentImage(), roVersion.ToUtf8StringAndFree(), rwVersion.ToUtf8StringAndFree());
    }

    private readonly FrameworkDotnet.Enums.FrameworkEcCurrentImage ToManagedCurrentImage()
    {
        return current_image switch
        {
            FrameworkEcCurrentImage.Unknown => FrameworkDotnet.Enums.FrameworkEcCurrentImage.Unknown,
            FrameworkEcCurrentImage.Ro => FrameworkDotnet.Enums.FrameworkEcCurrentImage.Ro,
            FrameworkEcCurrentImage.Rw => FrameworkDotnet.Enums.FrameworkEcCurrentImage.Rw,
            _ => throw new ArgumentOutOfRangeException(nameof(current_image), current_image, "Unhandled EC current image.")
        };
    }
}
