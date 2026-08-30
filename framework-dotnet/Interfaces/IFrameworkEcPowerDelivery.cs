using System;

using FrameworkDotnet.Attributes;
using FrameworkDotnet.Enums;
using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Snapshots;

namespace FrameworkDotnet.Interfaces;

/// <summary>
/// Defines the USB Power Delivery surface of an embedded controller connection.
/// </summary>
/// <remarks>
/// The members of this facet describe the charger side of the USB-C subsystem: the firmware carried by the Power Delivery controllers, the charger
/// negotiation state of an individual port, and the retimer firmware version. The Type-C link state of a port is reported separately through the module
/// inventory as <see cref="FrameworkPowerDeliveryPortStateSnapshot"/>.
/// </remarks>
public interface IFrameworkEcPowerDelivery
{
    /// <summary>
    /// Gets the firmware versions of every USB Power Delivery controller slot.
    /// </summary>
    /// <returns>The Power Delivery controller firmware versions.</returns>
    /// <remarks>
    /// The three controller slots are fixed by the native probe order: slot 0 is <see cref="FrameworkPowerDeliveryControllerSlot.Right01"/>, slot 1 is
    /// <see cref="FrameworkPowerDeliveryControllerSlot.Left23"/> and slot 2 is <see cref="FrameworkPowerDeliveryControllerSlot.Back"/>. Framework laptops
    /// populate slots 0 and 1, Framework Desktop populates slot 2 only, so the populated slots are not contiguous. Enumerate
    /// <see cref="FrameworkPowerDeliveryControllerVersionsSnapshot.PresentControllers"/>, or test
    /// <see cref="FrameworkPowerDeliveryControllerFirmwareSnapshot.IsPresent"/>, before reading any controller version.
    /// </remarks>
    /// <exception cref="ObjectDisposedException">Thrown when the owning connection has been disposed.</exception>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library returns an error status.</exception>
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkPowerDeliveryControllerVersionsSnapshot GetControllerVersions();

    /// <summary>
    /// Gets the charger negotiation state of a single USB Power Delivery port.
    /// </summary>
    /// <param name="port">The zero-based port index to query.</param>
    /// <returns>The charger negotiation state of the requested port.</returns>
    /// <remarks>
    /// This reports what the attached charger offers and what the port has negotiated from it: the power role, the charging type, the advertised voltage and
    /// current limits and the maximum negotiated power. It is deliberately distinct from <see cref="FrameworkPowerDeliveryPortStateSnapshot"/> in the module
    /// inventory, which reports the Type-C link itself. A port with nothing attached reports
    /// <see cref="FrameworkUsbPowerRole.Disconnected"/> and <see cref="FrameworkUsbChargingType.None"/> rather than failing.
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="port"/> is negative or greater than 255, or when the native layer reports a power role that is not recognized by the managed API.</exception>
    /// <exception cref="ObjectDisposedException">Thrown when the owning connection has been disposed.</exception>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library returns an error status, including when the requested port does not exist on this platform.</exception>
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkPowerDeliveryPowerInfoSnapshot GetPowerInfo(int port);

    /// <summary>
    /// Gets the retimer firmware version reported by the embedded controller.
    /// </summary>
    /// <returns>The retimer firmware version.</returns>
    /// <remarks>
    /// <para>
    /// The Parade retimer sits behind the Framework Laptop 16 expansion-bay discrete GPU. This query is backed by the expansion-bay GPU PCIe host
    /// command, which other platform families reject, so calling it on Framework 12, Framework 13 or Desktop raises
    /// <see cref="FrameworkStatusException"/> rather than returning a not-present reading.
    /// </para>
    /// <para>
    /// On a Framework Laptop 16 whose expansion bay carries no compatible discrete GPU, the query succeeds with
    /// <see cref="FrameworkPowerDeliveryRetimerVersionSnapshot.IsPresent"/> set to <see langword="false"/> and an empty version; that is a normal
    /// reading rather than an error.
    /// </para>
    /// </remarks>
    /// <exception cref="ObjectDisposedException">Thrown when the owning connection has been disposed.</exception>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library returns an error status, including on every platform family other than Framework Laptop 16, whose embedded controller rejects the underlying command.</exception>
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    [FrameworkPlatformSpecific(FrameworkPlatformFamily.Framework16, Message = "The Parade retimer sits behind the Framework Laptop 16 expansion-bay discrete GPU; other platform families reject the underlying EC command.")]
    FrameworkPowerDeliveryRetimerVersionSnapshot GetRetimerVersion();
}
