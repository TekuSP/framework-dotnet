---
description: "Use when editing C# in framework-dotnet, framework-dotnet-cli-test, or framework-dotnet-hardware-tests. Covers repo-specific .NET style, XML docs, safe-wrapper patterns, fixed-slot snapshots, and UnitsNet usage."
name: "framework-dotnet C# conventions"
applyTo:
  - "framework-dotnet/**/*.cs"
  - "framework-dotnet-cli-test/**/*.cs"
  - "framework-dotnet-hardware-tests/**/*.cs"
---

# framework-dotnet C# conventions

- Follow [`.editorconfig`](../../.editorconfig): UTF-8 BOM, CRLF, 4 spaces, and file-scoped namespaces.
- The main library disables implicit usings; keep `using` directives explicit and avoid fully qualified names unless ambiguity requires them.
- Maintain XML docs for public types and public members.
- Keep the public surface safe and managed-first: translate native failures into managed exceptions instead of exposing raw status inspection to callers.
- Preserve the fixed-slot ABI-aligned model shape (`Battery_0`, `Temperature_0`, `Fan_0`, and similar members) and add count-aware enumerable helpers on top rather than replacing those members.
- Use `UnitsNet` on public model and response types when values carry units.
- When adding a new native-backed feature, update the wrapper call, managed conversion/projection, and exception or ownership handling in the same change.
- Use these files as pattern references:
  - [`framework-dotnet/FrameworkSystem.cs`](../../framework-dotnet/FrameworkSystem.cs)
  - [`framework-dotnet/FrameworkEcConnection.cs`](../../framework-dotnet/FrameworkEcConnection.cs)
  - [`framework-dotnet/Snapshots/FrameworkThermalSnapshot.cs`](../../framework-dotnet/Snapshots/FrameworkThermalSnapshot.cs)
  - [`framework-dotnet/Exceptions/FrameworkEcResponseException.cs`](../../framework-dotnet/Exceptions/FrameworkEcResponseException.cs)
