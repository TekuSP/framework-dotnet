using FrameworkDotnet.Enums;
using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Snapshots;

using UnitsNet;

namespace Framework.System.Interop;

internal unsafe partial struct FrameworkEcProtocolInfoResult
{
    internal readonly FrameworkEcProtocolInfoSnapshot GetValueOrThrow()
    {
        if (status.IsFailure)
        {
            throw FrameworkStatusException.GetCorrectException(status);
        }

        return new FrameworkEcProtocolInfoSnapshot(
            protocol_versions,
            Information.FromBytes(max_request_packet_size),
            Information.FromBytes(max_response_packet_size),
            (FrameworkEcProtocolFlag)flags);
    }
}
