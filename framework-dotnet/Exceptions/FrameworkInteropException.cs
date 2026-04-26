using System;
using System.Collections.Generic;
using System.Text;

using Framework.System.Interop;

namespace FrameworkDotnet.Exceptions;

public class FrameworkInteropException : InvalidOperationException
{
    internal FrameworkInteropException(string message) : base(message)
    {
    }
    internal FrameworkInteropException(string message, Exception innerException) : base(message, innerException)
    {
    }
    internal FrameworkInteropException(FrameworkStatusCode status, int detail) : base($"Framework interop call failed with {status} (detail: {detail}).")
    {
    }
}
