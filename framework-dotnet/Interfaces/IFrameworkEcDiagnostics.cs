using System;

using FrameworkDotnet.Enums;
using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Snapshots;

namespace FrameworkDotnet.Interfaces;

/// <summary>
/// Defines the embedded controller diagnostic surface: liveness probes, host command protocol
/// capabilities, boot and reset provenance, stored panic data, port 80 POST code history, raw
/// ADC channels and the physical switch positions.
/// </summary>
/// <remarks>
/// <para>
/// Instances are obtained from an <see cref="IFrameworkEcConnection"/> and borrow that
/// connection's native handle, so they are only usable for as long as the owning connection is
/// open. Disposing the connection invalidates the facet; the facet itself owns nothing and is
/// therefore not <see cref="IDisposable"/>.
/// </para>
/// <para>
/// Every member issues at least one host command against the embedded controller, so none of
/// them are free. Treat them as on-demand diagnostics rather than poll-loop telemetry.
/// </para>
/// </remarks>
public interface IFrameworkEcDiagnostics
{
    /// <summary>
    /// Sends the embedded controller <c>hello</c> command with a caller-supplied payload and
    /// reports what the controller echoed back.
    /// </summary>
    /// <param name="inData">The arbitrary payload to send. A healthy controller answers with <paramref name="inData"/> plus <c>0x01020304</c>, computed with unsigned wraparound.</param>
    /// <returns>The echoed payload together with a flag indicating whether it matched the expected transform.</returns>
    /// <remarks>
    /// This is the cheapest end-to-end check that host command communication actually works. A
    /// returned snapshot whose <see cref="FrameworkEcHelloSnapshot.IsExpectedEcho"/> is
    /// <see langword="false"/> means the controller answered but answered wrongly, which
    /// indicates a corrupt transport rather than a failed call, so it is reported as a value and
    /// not as an exception.
    /// </remarks>
    /// <exception cref="ObjectDisposedException">Thrown when the owning connection has been disposed.</exception>
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkEcHelloSnapshot SendHello(uint inData);

    /// <summary>
    /// Sends the embedded controller <c>hello</c> command using the same magic payload the
    /// upstream liveness check uses, and reports what the controller echoed back.
    /// </summary>
    /// <returns>The echoed payload together with a flag indicating whether it matched the expected transform.</returns>
    /// <remarks>
    /// This is a convenience wrapper over <see cref="SendHello(uint)"/>. Inspect
    /// <see cref="FrameworkEcHelloSnapshot.IsExpectedEcho"/> to decide whether the controller is
    /// responding correctly.
    /// </remarks>
    /// <exception cref="ObjectDisposedException">Thrown when the owning connection has been disposed.</exception>
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkEcHelloSnapshot CheckHello();

    /// <summary>
    /// Gets the host command protocol capabilities reported by the embedded controller.
    /// </summary>
    /// <returns>The supported protocol versions, maximum packet sizes and optional protocol capabilities.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the owning connection has been disposed.</exception>
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkEcProtocolInfoSnapshot GetProtocolInfo();

    /// <summary>
    /// Gets the embedded controller system information: which firmware image is running, why the
    /// controller last reset, and the current lock and jump state.
    /// </summary>
    /// <returns>The embedded controller system information.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the owning connection has been disposed.</exception>
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the native layer reports an EC current image value that is not recognized by the managed API.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkEcSystemInfoSnapshot GetSystemInfo();

    /// <summary>
    /// Gets the panic data the embedded controller saved from its last crash.
    /// </summary>
    /// <returns>The raw panic blob together with the decoded header and trailer fields. An empty blob means the controller has no stored panic.</returns>
    /// <remarks>
    /// The panic payload is exposed as an opaque blob because the per-architecture decode
    /// structures are private to the upstream firmware headers. Use
    /// <see cref="FrameworkEcPanicInfoSnapshot.Architecture"/> and
    /// <see cref="FrameworkEcPanicInfoSnapshot.StructVersion"/> to select a decoder. Where the
    /// controller implements the versioned read command this call does not mark the panic as
    /// consumed, so other tools still observe it.
    /// </remarks>
    /// <exception cref="ObjectDisposedException">Thrown when the owning connection has been disposed.</exception>
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkEcPanicInfoSnapshot GetPanicInfo();

    /// <summary>
    /// Gets the port 80 POST code history recorded by the embedded controller.
    /// </summary>
    /// <returns>The history ring in buffer order together with a newest-first ordered view.</returns>
    /// <remarks>
    /// The controller stores POST codes in a wrapping ring buffer. Entries matching a
    /// <see cref="FrameworkPort80Event"/> value are markers the controller inserted itself
    /// rather than codes emitted by host firmware.
    /// </remarks>
    /// <exception cref="ObjectDisposedException">Thrown when the owning connection has been disposed.</exception>
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkEcPort80HistorySnapshot GetPort80History();

    /// <summary>
    /// Gets the live positions of the physical switches the embedded controller monitors.
    /// </summary>
    /// <returns>The lid, power button, write protect and dedicated recovery switch positions.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the owning connection has been disposed.</exception>
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkEcSwitchesSnapshot GetSwitches();

    /// <summary>
    /// Gets whether the embedded controller is currently throttling the application processor
    /// for thermal reasons.
    /// </summary>
    /// <returns>The soft and hard throttle states.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the owning connection has been disposed.</exception>
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    FrameworkEcApThrottleSnapshot GetApThrottleStatus();

    /// <summary>
    /// Reads one raw analog-to-digital converter channel on the embedded controller.
    /// </summary>
    /// <param name="channel">The zero-based ADC channel index. The set of valid channels is firmware defined; the controller rejects unknown channels.</param>
    /// <returns>The raw converter count for the channel.</returns>
    /// <remarks>
    /// The value is the unscaled converter reading. The native layer documents no unit or
    /// reference voltage for it, so no physical quantity conversion is applied here; the meaning
    /// of a channel and its scaling are firmware defined.
    /// </remarks>
    /// <exception cref="ObjectDisposedException">Thrown when the owning connection has been disposed.</exception>
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure, which includes the controller rejecting an unknown channel.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    int ReadAdcChannel(byte channel);

    /// <summary>
    /// Determines whether the embedded controller implements a host command at a given version.
    /// </summary>
    /// <param name="command">The host command identifier to probe.</param>
    /// <param name="version">The host command version to probe.</param>
    /// <returns><see langword="true"/> when the controller implements <paramref name="command"/> at <paramref name="version"/>; otherwise <see langword="false"/>.</returns>
    /// <remarks>
    /// Worth calling before the newer commands: support varies by platform and firmware
    /// revision, and this separates "not implemented" from "the call failed".
    /// </remarks>
    /// <exception cref="ObjectDisposedException">Thrown when the owning connection has been disposed.</exception>
    /// <exception cref="FrameworkEcResponseException">Thrown when the native Framework library returns an EC response failure.</exception>
    /// <exception cref="DllNotFoundException">Thrown when the native Framework library cannot be located.</exception>
    /// <exception cref="BadImageFormatException">Thrown when the native Framework library is incompatible with the current process architecture.</exception>
    /// <exception cref="EntryPointNotFoundException">Thrown when the required native entry point is unavailable.</exception>
    bool IsCommandVersionSupported(uint command, byte version);
}
