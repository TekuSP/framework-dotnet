using System;
using System.Text;

namespace FrameworkDotnet.Generated;

internal static unsafe class InteropStringHelpers
{
    internal static string ReadNullTerminatedUtf8(byte* value, int maxLength)
    {
        if (value == null || maxLength <= 0)
        {
            return string.Empty;
        }

        var span = new ReadOnlySpan<byte>(value, maxLength);
        var terminatorIndex = span.IndexOf((byte)0);
        if (terminatorIndex >= 0)
        {
            span = span[..terminatorIndex];
        }

        return Encoding.UTF8.GetString(span);
    }
}
