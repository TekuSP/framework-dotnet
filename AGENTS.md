# framework-dotnet Guidelines

## Start here

- Read [README.md](README.md) for the library purpose, public API, and source-build prerequisites.
- The shipping library is in `framework-dotnet/`.
- `framework-dotnet-cli-test/` is the smoke-test console app.
- `framework-dotnet-hardware-tests/` contains NUnit tests that expect real Framework hardware and a loadable native library.

## Submodule boundaries

- `framework-system-ffi-extensions/` is a separate Git submodule, and `framework-system-ffi-extensions/framework-system/` is a nested upstream submodule.
- Do not edit either submodule from this repository unless the user explicitly asks for submodule work.
- It is fine to read those folders to understand newly added native features. Start with [framework-system-ffi-extensions/README.md](framework-system-ffi-extensions/README.md), [framework-system-ffi-extensions/FFI_NOTES.md](framework-system-ffi-extensions/FFI_NOTES.md), and [framework-system-ffi-extensions/FRAMEWORK_DOTNET_CHANGES_TO_BE_MADE.md](framework-system-ffi-extensions/FRAMEWORK_DOTNET_CHANGES_TO_BE_MADE.md).
- When new native features land there, extend `framework-dotnet/` to consume them instead of patching the submodule by default.

## Build and test

- Main library build: `dotnet build framework-dotnet/framework-dotnet.csproj`
- If generated bindings and native assets are already supplied externally: `dotnet build framework-dotnet/framework-dotnet.csproj /p:SkipRustBuild=true`
- Smoke-test app build: `dotnet build framework-dotnet-cli-test/framework-dotnet-cli-test.csproj`
- Hardware tests: `dotnet test framework-dotnet-hardware-tests/framework-dotnet-hardware-tests.csproj` on supported Framework hardware.

## C# conventions

- Follow [`.editorconfig`](.editorconfig): UTF-8 BOM, CRLF, 4-space indentation, and file-scoped namespaces.
- Keep XML documentation on the public API surface; internal and private implementation details do not need full documentation coverage.
- In the main library, `ImplicitUsings` are disabled. Add explicit `using` directives instead of assuming ambient imports.
- Preserve the repo's safe-wrapper shape: public code should expose managed types, `UnitsNet` quantities, and managed exceptions rather than raw interop/status handling.
- Preserve fixed-slot snapshot members that mirror the native ABI, such as `Battery_0`, `Temperature_0`, and `Fan_0`, and keep the count-aware enumerable helpers layered on top.

## Generated and interop surfaces

- Treat `framework-dotnet/Generated/**/*.cs` and `**/*.g.cs` as generated or generator-adjacent. Prefer regeneration or upstream/source changes over hand-editing them.
- When adding FFI-backed features, update the managed wrapper, ownership/freeing path, domain projection, and exception/status mapping together.
- Good pattern references: `framework-dotnet/FrameworkSystem.cs`, `framework-dotnet/FrameworkEcConnection.cs`, `framework-dotnet/Snapshots/FrameworkThermalSnapshot.cs`, and `framework-dotnet/Exceptions/FrameworkEcResponseException.cs`.
