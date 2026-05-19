---
description: "Use when syncing framework-dotnet to new FFI or submodule features, reviewing interop changes, or deciding whether generated/native files should be edited. Covers submodule boundaries and downstream sync workflow."
name: "framework-dotnet FFI sync"
---

# framework-dotnet FFI sync

- Treat `framework-system-ffi-extensions/` and its nested `framework-system/` repository as read-only submodules unless the user explicitly asks for submodule work.
- Before extending the managed layer for new native features, review:
  - [`framework-system-ffi-extensions/README.md`](../../framework-system-ffi-extensions/README.md)
  - [`framework-system-ffi-extensions/FFI_NOTES.md`](../../framework-system-ffi-extensions/FFI_NOTES.md)
  - [`framework-system-ffi-extensions/FRAMEWORK_DOTNET_CHANGES_TO_BE_MADE.md`](../../framework-system-ffi-extensions/FRAMEWORK_DOTNET_CHANGES_TO_BE_MADE.md)
- Default workflow: consume new native capabilities by extending `framework-dotnet/`; do not patch the submodule just to make the managed layer compile.
- Treat `framework-dotnet/Generated/**/*.cs` and `**/*.g.cs` as generated or generator-adjacent. Prefer regeneration or source-ABI changes over manual edits.
- If new FFI types add `FrameworkByteBuffer`, fixed buffers, or new `FrameworkStatus` payloads, update ownership/free helpers, managed projections, and exception mapping together.
- If the generated shape looks awkward, prefer fixing the upstream FFI or generator shape rather than layering workaround code across many managed files.
