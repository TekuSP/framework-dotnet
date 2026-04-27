using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FrameworkDotnet.Exceptions;

/// <summary>
/// Represents the base exception for FrameworkDotnet managed API failures.
/// </summary>
public class FrameworkException : ArgumentException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkException"/> class.
    /// </summary>
    public FrameworkException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public FrameworkException(string? message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public FrameworkException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkException"/> class with a specified error message and parameter name.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="paramName">The name of the parameter that caused the current exception.</param>
    public FrameworkException(string? message, string? paramName) : base(message, paramName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkException"/> class with a specified error message, parameter name, and inner exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="paramName">The name of the parameter that caused the current exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public FrameworkException(string? message, string? paramName, Exception? innerException) : base(message, paramName, innerException)
    {
    }
}
