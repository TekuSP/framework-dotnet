using System;
using System.Collections.Generic;
using System.Text;

using FrameworkDotnet.Enums;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents a fan reading from the EC.
/// </summary>
public sealed class FrameworkFanSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkFanSnapshot"/> class.
    /// </summary>
    /// <param name="fanState">The fan reading state.</param>
    /// <param name="rpm">The fan speed in revolutions per minute.</param>
    public FrameworkFanSnapshot(FrameworkFanState fanState, ushort rpm)
    {
        FanState = fanState;
        Rpm = rpm;
    }

    /// <summary>
    /// Gets the fan reading state.
    /// </summary>
    public FrameworkFanState FanState
    {
        get; init;
    }

    /// <summary>
    /// Gets the fan speed in revolutions per minute.
    /// </summary>
    public ushort Rpm
    {
        get; init;
    }
}
