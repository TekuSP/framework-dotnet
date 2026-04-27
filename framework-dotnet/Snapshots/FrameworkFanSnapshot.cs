using System;
using System.Collections.Generic;
using System.Text;

using FrameworkDotnet.Enums;

namespace FrameworkDotnet.Snapshots;

public sealed class FrameworkFanSnapshot
{
    public FrameworkFanSnapshot(FrameworkFanState fanState, ushort rpm)
    {
        FanState = fanState;
        Rpm = rpm;
    }

    public FrameworkFanState FanState
    {
        get; init;
    }
    public ushort Rpm
    {
        get; init;
    }
}
