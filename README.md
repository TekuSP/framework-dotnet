# framework-dotnet

`FrameworkDotnet` is a .NET library for talking to Framework laptop and desktop embedded controller APIs through the native `framework-system` Rust FFI layer.

It provides a small managed API for:

- detecting supported embedded controller drivers
- reading Framework platform and product information
- opening an embedded controller connection without exposing unsafe code to callers
- reading firmware, power, fan, and thermal snapshots
- controlling fan RPM, fan duty, and restoring automatic fan control

## What this library wraps

The project uses:

- the `framework-system` Rust submodule for the native hardware access layer
- `csbindgen` to generate the low-level C# interop bindings
- a hand-written `FrameworkDotnet` API surface on top of those bindings so normal .NET callers do not need to work with pointers or unsafe code

## Intended use

This library is intended for applications that run on machines with supported Framework hardware and the required embedded controller access available on the host operating system.

It is best validated with a small smoke-test console app on a real device, because most behavior depends on native library loading, driver availability, permissions, and actual Framework hardware.

## Example

```csharp
using FrameworkDotnet;

Console.WriteLine($"Product: {FrameworkSystem.GetProductName()}");
Console.WriteLine($"Platform: {FrameworkSystem.GetPlatform()}");

using var ec = FrameworkSystem.OpenDefaultEc();

var power = ec.GetPowerSnapshot();
var thermal = ec.GetThermalSnapshot();

Console.WriteLine($"Battery: {power.ChargePercentage}%");
Console.WriteLine($"Temperature 0: {thermal.Temperature0.Celsius}C");
```

## NuGet packaging

The `framework-dotnet` project is configured to build the Rust native library before the managed project builds and to include the generated interop layer and native library in the resulting NuGet package.

The packaged native binary represents the platform that built the package, so cross-platform publishing should be done by building and packing on each target platform or by extending the packaging process to collect binaries for multiple runtimes.
