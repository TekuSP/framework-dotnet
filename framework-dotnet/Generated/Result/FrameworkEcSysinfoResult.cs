using System;

using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Snapshots;

using ManagedEcCurrentImage = FrameworkDotnet.Enums.FrameworkEcCurrentImage;
using ManagedEcResetFlag = FrameworkDotnet.Enums.FrameworkEcResetFlag;
using ManagedEcSysinfoFlag = FrameworkDotnet.Enums.FrameworkEcSysinfoFlag;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcSysinfoResult
{
    internal readonly FrameworkEcSystemInfoSnapshot GetValueOrThrow()
    {
        if (status.IsFailure)
        {
            throw FrameworkStatusException.GetCorrectException(status);
        }

        return new FrameworkEcSystemInfoSnapshot(
            ToManagedCurrentImage(),
            (ManagedEcResetFlag)reset_flags,
            (ManagedEcSysinfoFlag)flags);
    }

    private readonly ManagedEcCurrentImage ToManagedCurrentImage()
    {
        return current_image switch
        {
            FrameworkEcCurrentImage.Unknown => ManagedEcCurrentImage.Unknown,
            FrameworkEcCurrentImage.Ro => ManagedEcCurrentImage.Ro,
            FrameworkEcCurrentImage.Rw => ManagedEcCurrentImage.Rw,
            _ => throw new ArgumentOutOfRangeException(nameof(current_image), current_image, "Unhandled EC current image.")
        };
    }
}
