using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FrameworkDotnet.Exceptions;

public class FrameworkException : ArgumentException
{
    public FrameworkException()
    {
    }

    public FrameworkException(string? message) : base(message)
    {
    }

    public FrameworkException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public FrameworkException(string? message, string? paramName) : base(message, paramName)
    {
    }

    public FrameworkException(string? message, string? paramName, Exception? innerException) : base(message, paramName, innerException)
    {
    }
}
