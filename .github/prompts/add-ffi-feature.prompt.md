---
description: "Add a new framework-system FFI-backed capability to framework-dotnet. Use when a native API or generated interop type already exists and you want the managed library extended without editing the submodule by default."
name: "Add FFI Feature"
argument-hint: "Which FFI feature, exported API, or pending downstream item should be added?"
agent: "framework-dotnet FFI Sync"
---

Add the requested FFI-backed feature to `framework-dotnet/` using the existing safe-wrapper patterns in this repository.

Start from these repo sources as needed:

- [AGENTS.md](../../AGENTS.md)
- [`.github/instructions/ffi-sync.instructions.md`](../instructions/ffi-sync.instructions.md)
- [`.github/instructions/framework-dotnet-csharp.instructions.md`](../instructions/framework-dotnet-csharp.instructions.md)
- [`framework-system-ffi-extensions/FRAMEWORK_DOTNET_CHANGES_TO_BE_MADE.md`](../../framework-system-ffi-extensions/FRAMEWORK_DOTNET_CHANGES_TO_BE_MADE.md)
- [`framework-system-ffi-extensions/FFI_NOTES.md`](../../framework-system-ffi-extensions/FFI_NOTES.md)
- [`framework-dotnet/Generated/NativeMethods.cs`](../../framework-dotnet/Generated/NativeMethods.cs)
- [`framework-dotnet/FrameworkEcConnection.cs`](../../framework-dotnet/FrameworkEcConnection.cs)

Instructions:

1. Treat the user argument as the feature to add. If no argument is given, inspect `FRAMEWORK_DOTNET_CHANGES_TO_BE_MADE.md` and choose the highest-value pending managed follow-up.
2. Confirm the native feature already exists in `framework-system-ffi-extensions/` or in generated interop available to this repo.
3. Do not edit `framework-system-ffi-extensions/` or its nested `framework-system/` submodule unless the user explicitly asks for submodule work.
4. Extend `framework-dotnet/` with the managed wrapper, domain projection, ownership/freeing logic, and exception/status mapping required for the feature.
5. Preserve the repo's safe-wrapper shape: expose managed types, `UnitsNet` quantities where relevant, and fixed-slot or count-aware helpers where the ABI pattern requires them.
6. Avoid hand-editing generated files unless regeneration or generator-adjacent upkeep is clearly the right fix.
7. Validate with the smallest relevant `dotnet build` or `dotnet test` command you can justify.
8. End with:
   - the feature that was added
   - files changed and why
   - validation performed
   - any remaining follow-up items or upstream gaps
