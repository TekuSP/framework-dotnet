---
description: "Use when editing generated interop files, deciding whether Generated/**/*.cs or *.g.cs should be modified, or reviewing boundaries between generated bindings and handwritten wrappers. Covers regeneration-first rules and where custom logic should live."
name: "Generated interop boundary"
applyTo:
  - "framework-dotnet/Generated/**/*.cs"
  - "framework-dotnet/**/*.g.cs"
---

# Generated interop boundary

- Treat `framework-dotnet/Generated/**/*.cs` and `**/*.g.cs` as generated or generator-adjacent first, handwritten code second.
- Prefer changing the upstream/native source, generator input, or regeneration path over hand-editing generated output.
- When manual changes are necessary, keep them limited to handwritten wrapper helpers around the generated surface rather than rewriting generated declarations in place.
- `framework-dotnet/Generated/NativeMethods.cs` is the main managed interop convenience layer; use it for resolver and helper logic that belongs above raw generated P/Invoke declarations.
- If a generated shape exposes new `FrameworkByteBuffer`, fixed buffers, or new `FrameworkStatus` payloads, update ownership/free helpers and managed exception mapping in the handwritten layer during the same change.
- Before making ABI-sensitive decisions, review:
  - [`AGENTS.md`](../../AGENTS.md)
  - [`ffi-sync.instructions.md`](./ffi-sync.instructions.md)
  - [`framework-system-ffi-extensions/.github/instructions/ffi-generated-surface.instructions.md`](../../framework-system-ffi-extensions/.github/instructions/ffi-generated-surface.instructions.md)
- Preserve downstream assumptions that matter to the managed layer, such as fixed-slot snapshot shapes and generated result types that feed existing wrapper code.
