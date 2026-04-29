# framework-dotnet
[![Build](https://github.com/TekuSP/framework-dotnet/actions/workflows/build.yml/badge.svg)](https://github.com/TekuSP/framework-dotnet/actions/workflows/build.yml)

`FrameworkDotnet` is a .NET library for talking to Framework laptop and desktop embedded controller APIs through the native `framework-system` Rust FFI layer.

The current managed API is intentionally small, but it is planned to expand over time toward broader coverage of the native `framework-system` functionality.

It provides a small managed API for:

- detecting supported embedded controller drivers
- reading Framework platform and product information
- opening an embedded controller connection without exposing unsafe code to callers
- reading firmware, power, fan, and thermal snapshots
- controlling fan RPM, fan duty, and restoring automatic fan control

The public API keeps the native fixed-slot snapshot shape from Rust for consistency, while also exposing count-aware enumerable properties and Units.NET quantities for unit-bearing values.

## What this library wraps

The project uses:

- the `framework-system-ffi-extensions` Rust repository for the native .NET FFI layer, with the upstream `framework-system` repository nested inside it as a submodule for the hardware access layer
- `csbindgen` to generate the low-level C# interop bindings
- `UnitsNet` for public unit-bearing values such as temperature, fan speed, voltage, current, charge, and ratio values
- a hand-written `FrameworkDotnet` API surface on top of those bindings so normal .NET callers do not need to work with pointers or unsafe code

## Intended use

This library is intended for applications that run on machines with supported Framework hardware and the required embedded controller access available on the host operating system.

It is best validated with a small smoke-test console app on a real device, because most behavior depends on native library loading, driver availability, permissions, actual Framework hardware, and the installed BIOS and firmware versions on the target machine.

## Example

```csharp
using FrameworkDotnet;
using FrameworkDotnet.Enums;
using Spectre.Console;
using System.Text;
using UnitsNet;

FrameworkSystem FrameworkSystem = new FrameworkSystem();
using var ec = FrameworkSystem.OpenDefaultEc();
while (true)
{
    AnsiConsole.Clear();

    var fanCapabilitiesSnapshot = ec.GetFanCapabilitiesSnapshot();
    var powerSnapshot = ec.GetPowerSnapshot();
    var thermalSnapshot = ec.GetThermalSnapshot();

    // 1. Build the top section using a StringBuilder
    var sysInfo = new StringBuilder();

    sysInfo.AppendLine($"[cyan]Product:[/] {FrameworkSystem.GetProductName()}");
    sysInfo.AppendLine($"[cyan]Platform:[/] {FrameworkSystem.GetPlatform()}");
    sysInfo.AppendLine($"[cyan]Family:[/] {FrameworkSystem.GetPlatformFamily()}");
    sysInfo.AppendLine();

    sysInfo.AppendLine("[cyan bold]Driver support:[/]");
    foreach (FrameworkEcDriver driver in Enum.GetValues<FrameworkEcDriver>())
    {
        bool isSupported = FrameworkSystem.IsDriverSupported(driver);
        string color = isSupported ? "green" : "red";
        sysInfo.AppendLine($"  [teal]{driver}:[/] [{color}]{isSupported}[/]");
    }
    sysInfo.AppendLine();

    sysInfo.AppendLine($"[cyan]Active driver:[/] {ec.GetActiveDriver()}");
    sysInfo.AppendLine($"[cyan]Build info:[/] [grey]{ec.GetBuildInfo()}[/]");

    var flash = ec.GetFlashSnapshot();
    sysInfo.AppendLine($"[cyan]Flash:[/] {flash.CurrentImage}, [grey]RO=[/][yellow]{flash.RoVersion}[/], [grey]RW=[/][yellow]{flash.RwVersion}[/]");

    // 2. Render the top section inside a Panel
    AnsiConsole.Write(
        new Panel(new Markup(sysInfo.ToString()))
            .Header("[bold blue]System Information[/]")
            .BorderColor(Color.Blue));

    // 3. Render the Snapshots (as we did before)
    var fanText = fanCapabilitiesSnapshot.ToString().Replace(", ", Environment.NewLine);
    AnsiConsole.Write(
        new Panel(fanText)
            .Header("[bold magenta]Fan Capabilities Snapshot[/]")
            .BorderColor(Color.Magenta));

    var powerText = powerSnapshot.ToString().Replace(", ", Environment.NewLine);
    AnsiConsole.Write(
        new Panel(powerText)
            .Header("[bold green]Power Snapshot[/]")
            .BorderColor(Color.Green));

    var thermalText = thermalSnapshot.ToString().Replace(", ", Environment.NewLine);
    AnsiConsole.Write(
        new Panel(thermalText)
            .Header("[bold red]Thermal Snapshot[/]")
            .BorderColor(Color.Red));

    Thread.Sleep(10000);
}
```

<img width="939" height="1419" alt="image" src="https://github.com/user-attachments/assets/9e45e991-2cfa-4b30-bfda-d680f2cc9b88" />


## Public API at a glance

The current public API is centered around two main entry points:

- `FrameworkSystem`
  - detects platform and product information
  - checks driver support
  - opens EC connections
- `IFrameworkEcConnection`
  - reads firmware, power, fan capability, and thermal snapshots
  - sends fan control commands

Main snapshot types:

- `FrameworkEcFlashSnapshot`
- `FrameworkPowerSnapshot`
- `FrameworkBatterySnapshot`
- `FrameworkFanCapabilitiesSnapshot`
- `FrameworkThermalSnapshot`
- `FrameworkTemperatureSnapshot`
- `FrameworkFanSnapshot`

Main response types:

- `FrameworkSetFanRpmResponse`
- `FrameworkSetFanDutyResponse`
- `FrameworkRestoreAutoFanControlResponse`

## Snapshot shape and enumeration

The public snapshot API intentionally stays aligned with the native Rust fixed-slot structs.

Examples:

- `FrameworkPowerSnapshot.Battery_0`
- `FrameworkThermalSnapshot.Temperature_0` through `Temperature_7`
- `FrameworkThermalSnapshot.Fan_0` through `Fan_3`

To make these snapshots easier to consume in normal .NET code, the library also exposes count-aware enumerable properties:

- `FrameworkPowerSnapshot.ReportedBatteries`
- `FrameworkThermalSnapshot.ReportedTemperatures`
- `FrameworkThermalSnapshot.ReportedFans`

These enumerable properties return only the entries reported by the corresponding count field.

## Units

Public unit-bearing values use `UnitsNet`.

Examples:

- temperatures use `UnitsNet.Temperature`
- fan speeds use `UnitsNet.RotationalSpeed`
- fan duty uses `UnitsNet.Ratio`
- battery voltage uses `UnitsNet.ElectricPotential`
- battery rate uses `UnitsNet.ElectricCurrent`
- battery capacities use `UnitsNet.ElectricCharge`

This allows consumers to work with quantities directly instead of manually tracking raw unit conventions.

## Example: exceptions and count-aware enumeration

```csharp
using FrameworkDotnet;
using FrameworkDotnet.Exceptions;

var frameworkSystem = new FrameworkSystem();

try
{
    using var ec = frameworkSystem.OpenDefaultEc();

    var thermal = ec.GetThermalSnapshot();

    foreach (var temperature in thermal.ReportedTemperatures)
    {
        Console.WriteLine($"Temperature: {temperature.Temperature.DegreesCelsius}C ({temperature.State})");
    }

    foreach (var fan in thermal.ReportedFans)
    {
        Console.WriteLine($"Fan: {fan.Speed.RevolutionsPerMinute} RPM ({fan.FanState})");
    }
}
catch (FrameworkEcResponseException ex)
{
    Console.WriteLine($"EC response failure: {ex.Detail}");
    Console.WriteLine($"Description: {ex.Description}");
}
catch (FrameworkTemperatureStateException ex)
{
    Console.WriteLine($"Temperature state failure: {ex.TemperatureState}");
    Console.WriteLine($"Description: {ex.Description}");
}
catch (FrameworkFanStateException ex)
{
    Console.WriteLine($"Fan state failure: {ex.FanState}");
    Console.WriteLine($"Description: {ex.Description}");
}
```

## Exception behavior

Public API methods throw specific managed exception types rather than requiring callers to inspect status codes.

Examples include:

- `FrameworkEcResponseException` and its derived EC response exceptions
- `FrameworkInvalidFanIndexException`
- `FrameworkBatteryStateException` and derived battery state exceptions
- `FrameworkTemperatureStateException` and derived temperature state exceptions
- `FrameworkFanStateException` and derived fan state exceptions

When native context is available, exceptions attempt to include native descriptions and device error messages from the Rust layer. This diagnostic enrichment is best-effort and does not prevent the primary exception from being thrown if native message retrieval fails.

## Installing

Add the package reference to your project:

```xml
<PackageReference Include="FrameworkDotnet" Version="0.1.0" />
```

`UnitsNet` is already a dependency of the library, so callers do not need to add separate unit types manually unless they want direct compile-time references in their own project code.

## Building from source

The repository normally builds the `framework-system-ffi-extensions` Rust repository before compiling the managed library.

Typical source build:

```powershell
dotnet build framework-dotnet/framework-dotnet.csproj
```

If generated bindings and native assets are already supplied externally, you can skip the Rust build step:

```powershell
dotnet build framework-dotnet/framework-dotnet.csproj /p:SkipRustBuild=true
```

## Native loading and driver notes

### Windows

- The managed library expects the native `framework_lib_ffi.dll` asset to be available beside the application or in the packaged runtime-specific native layout.
- Embedded controller access depends on the selected native driver, the permissions available on the host system, and the installed BIOS and firmware versions on the target machine.
- Driver support can vary by machine and configuration, so `IsDriverSupported(...)` should be treated as a runtime check.

### Linux

- The managed library expects the native `libframework_lib_ffi.so` asset to be available beside the application or in the packaged runtime-specific native layout.
- Embedded controller access depends on kernel support, available native driver support, device permissions, and the installed BIOS and firmware versions on the target machine.
- On Linux, successful native loading alone does not guarantee that EC operations will succeed; permissions and supported driver availability still matter.

## Trademark and affiliation notice

Framework is a separate company and brand with its own copyrights and trademarks.

This project is an independent community project and is not affiliated with, endorsed by, or officially associated with Framework.

## Current limitations

- The managed API currently infers `FrameworkThermalSnapshot.SensorCount` because the Rust layer does not yet provide a dedicated sensor count value.
- The public fixed-slot snapshot members intentionally mirror the current native Rust struct layout.
- Some command responses still echo request identity such as `FanIndex` for clarity and traceability.
