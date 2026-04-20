using FrameworkDotnet.Generated;

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
}
