using System;
using System.Collections.Generic;

using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Snapshots;

namespace FrameworkDotnet.Interfaces;

/// <summary>
/// Defines the general-purpose input/output surface of a Framework embedded controller connection.
/// </summary>
/// <remarks>
/// <para>
/// This is an <b>advanced diagnostic API</b>. The embedded controller GPIO table is the firmware's own
/// pin map: it contains power-sequencing enables, reset lines, bus selects, interrupt lines and rail
/// monitors. Reading a line is harmless, but <see cref="SetValue(string, bool)"/> drives a real hardware
/// line and can cut a power rail, hold a device in reset, or contend with firmware that is driving the
/// same pin. Writing an arbitrary GPIO can destabilise or hard-hang the system, corrupt an in-flight
/// device transaction, or leave the machine in a state that only a full power cycle clears. Use it only
/// against a line you have positively identified, and never as part of routine telemetry.
/// </para>
/// <para>
/// Line names are addressed as UTF-8 and firmware accepts at most 32 bytes. Names longer than that are
/// rejected by the managed layer rather than silently truncated by the embedded controller.
/// </para>
/// <para>
/// Every member issues a synchronous host command to the embedded controller. Keep calls off the UI
/// thread, and prefer <see cref="GetAll"/> over a hand-rolled loop when enumerating the whole table.
/// </para>
/// </remarks>
public interface IFrameworkEcGpio
{
    /// <summary>
    /// Gets the number of general-purpose input/output lines the embedded controller exposes.
    /// </summary>
    /// <returns>The number of lines in the embedded controller GPIO table.</returns>
    /// <remarks>Valid indices for <see cref="GetInfo(int)"/> are <c>0</c> through the returned count minus one.</remarks>
    /// <exception cref="ObjectDisposedException">Thrown when the owning connection has been disposed.</exception>
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure.</exception>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library reports any other failure status.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    int GetCount();

    /// <summary>
    /// Reads the current logic level of a general-purpose input/output line addressed by name.
    /// </summary>
    /// <param name="name">The firmware-assigned name of the line, at most 32 UTF-8 bytes.</param>
    /// <returns><see langword="true"/> when the line reads as logic high; otherwise, <see langword="false"/>.</returns>
    /// <remarks>Names are matched exactly and case-sensitively against the firmware pin map; use <see cref="GetAll"/> to discover them.</remarks>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="name"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> is empty or encodes to more than 32 UTF-8 bytes.</exception>
    /// <exception cref="ObjectDisposedException">Thrown when the owning connection has been disposed.</exception>
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure, including when the embedded controller does not know the requested line name.</exception>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library reports any other failure status.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    bool GetValue(string name);

    /// <summary>
    /// Drives a general-purpose input/output line addressed by name to the requested logic level.
    /// </summary>
    /// <param name="name">The firmware-assigned name of the line, at most 32 UTF-8 bytes.</param>
    /// <param name="value"><see langword="true"/> to drive the line logic high; <see langword="false"/> to drive it logic low.</param>
    /// <remarks>
    /// <para>
    /// <b>This writes a real hardware line.</b> Driving an arbitrary embedded controller GPIO can power
    /// down a rail, hold a peripheral in reset, break an in-flight bus transaction, or fight firmware that
    /// owns the same pin — any of which can destabilise or hang the running system, and some of which
    /// survive until a full power cycle. Restrict this to lines you have positively identified, and treat
    /// it as a debugging and bring-up tool rather than a supported control surface.
    /// </para>
    /// <para>
    /// The embedded controller reports success once it has accepted the command; it does not confirm that
    /// the pin settled at the requested level. Read the line back with <see cref="GetValue(string)"/> when
    /// the resulting state matters, and be aware that firmware may immediately re-drive a pin it owns.
    /// </para>
    /// </remarks>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="name"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> is empty or encodes to more than 32 UTF-8 bytes.</exception>
    /// <exception cref="ObjectDisposedException">Thrown when the owning connection has been disposed.</exception>
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure, including when the embedded controller does not know the requested line name or refuses to drive it.</exception>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library reports any other failure status.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    void SetValue(string name, bool value);

    /// <summary>
    /// Reads the name, logic level and configuration flags of a single line addressed by table index.
    /// </summary>
    /// <param name="index">The zero-based index of the line, in the range <c>0</c> through <see cref="GetCount"/> minus one.</param>
    /// <returns>A snapshot describing the requested line.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="index"/> is negative or greater than 255, the largest index the embedded controller command can address.</exception>
    /// <exception cref="ObjectDisposedException">Thrown when the owning connection has been disposed.</exception>
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure, including when <paramref name="index"/> is beyond the reported line count.</exception>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library reports any other failure status.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkEcGpioSnapshot GetInfo(int index);

    /// <summary>
    /// Reads every general-purpose input/output line the embedded controller exposes, in index order.
    /// </summary>
    /// <returns>A snapshot for each line, ordered by <see cref="FrameworkEcGpioSnapshot.Index"/>.</returns>
    /// <remarks>
    /// The enumeration is count-aware: it reads <see cref="GetCount"/> once and then issues one
    /// <see cref="GetInfo(int)"/> host command per index. The result is fully materialised before it is
    /// returned, so the embedded controller is not held open while a caller iterates. On a laptop the table
    /// typically holds well over a hundred lines, so treat this as an on-demand discovery call rather than
    /// something to poll.
    /// </remarks>
    /// <exception cref="ObjectDisposedException">Thrown when the owning connection has been disposed.</exception>
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure.</exception>
    /// <exception cref="FrameworkStatusException">Thrown when the native Framework library reports any other failure status.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    IReadOnlyList<FrameworkEcGpioSnapshot> GetAll();
}
