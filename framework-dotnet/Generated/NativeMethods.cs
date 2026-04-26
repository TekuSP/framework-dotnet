using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

using GroupedNativeMethodsGenerator;

namespace Framework.System.Interop;

[GroupedNativeMethods(removePrefix: "framework_", removeSuffix: "_handle")]
internal static unsafe partial class NativeMethods
{
    private const string DllName = "framework_dotnet_ffi";

    static NativeMethods()
    {
        NativeLibrary.SetDllImportResolver(typeof(NativeMethods).Assembly, DllImportResolver);
    }

    internal static bool IsLibraryAvailable()
    {
        return ConfirmExistanceOfDll(DllName, typeof(NativeMethods).Assembly, null);
    }

    internal static FrameworkPlatformFamily GetPlatformFamilyOrThrow()
    {
        FrameworkPlatformFamily family;
        framework_get_platform_family(&family).ThrowIfError();
        return family;
    }

    internal static FrameworkPlatform GetPlatformOrThrow()
    {
        FrameworkPlatform platform;
        framework_get_platform(&platform).ThrowIfError();
        return platform;
    }

    internal static string GetProductNameOrThrow()
    {
        FrameworkByteBuffer buffer = default;
        framework_get_product_name(&buffer).ThrowIfError();

        try
        {
            return buffer.ToUtf8String();
        }
        finally
        {
            buffer.Free();
        }
    }

    internal static FrameworkEcHandle* OpenDefaultOrThrow()
    {
        FrameworkEcHandle* handle;
        framework_ec_open_default(&handle).ThrowIfError();
        return handle;
    }

    internal static FrameworkEcHandle* OpenWithDriverOrThrow(FrameworkEcDriver driver)
    {
        FrameworkEcHandle* handle;
        framework_ec_open_with_driver(driver, &handle).ThrowIfError();
        return handle;
    }

    private static IntPtr DllImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
    {
        if (!string.Equals(libraryName, DllName, StringComparison.Ordinal))
        {
            return IntPtr.Zero;
        }

        var assemblyDirectory = GetDirectoryOrDefault(Path.GetDirectoryName(assembly.Location));
        var baseDirectory = GetDirectoryOrDefault(AppContext.BaseDirectory);
        var fileName = GetLibraryFileName();
        var runtimeIdentifier = GetRuntimeIdentifier();

        if (TryLoadFromDirectory(assemblyDirectory, fileName, assembly, searchPath, out var handle) ||
            TryLoadFromDirectory(baseDirectory, fileName, assembly, searchPath, out handle) ||
            TryLoadFromDirectory(Path.Combine(assemblyDirectory, "runtimes", runtimeIdentifier, "native"), fileName, assembly, searchPath, out handle) ||
            TryLoadFromDirectory(Path.Combine(baseDirectory, "runtimes", runtimeIdentifier, "native"), fileName, assembly, searchPath, out handle))
        {
            return handle;
        }

        return IntPtr.Zero;
    }

    private static string GetDirectoryOrDefault(string? directory)
    {
        return string.IsNullOrWhiteSpace(directory) ? AppContext.BaseDirectory : directory;
    }

    private static string GetLibraryFileName()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return $"{DllName}.dll";
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return $"lib{DllName}.dylib";
        }

        return $"lib{DllName}.so";
    }

    private static string GetRuntimeIdentifier()
    {
        var platform = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
            ? "win"
            : RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
                ? "osx"
                : "linux";

        var architecture = RuntimeInformation.OSArchitecture switch
        {
            Architecture.X64 => "x64",
            Architecture.Arm64 => "arm64",
            Architecture.X86 => "x86",
            _ => throw new PlatformNotSupportedException($"Unsupported OS architecture: {RuntimeInformation.OSArchitecture}.")
        };

        return $"{platform}-{architecture}";
    }

    private static bool TryLoadFromDirectory(string directory, string fileName, Assembly assembly, DllImportSearchPath? searchPath, out IntPtr handle)
    {
        handle = IntPtr.Zero;

        if (string.IsNullOrWhiteSpace(directory))
        {
            return false;
        }

        var candidatePath = Path.Combine(directory, fileName);
        return File.Exists(candidatePath) && NativeLibrary.TryLoad(candidatePath, assembly, searchPath, out handle);
    }

    private static bool TryVerifyFromDirectory(string directory, string fileName, Assembly assembly, DllImportSearchPath? searchPath)
    {
        if (string.IsNullOrWhiteSpace(directory))
        {
            return false;
        }

        var candidatePath = Path.Combine(directory, fileName);
        return File.Exists(candidatePath);
    }

    private static bool ConfirmExistanceOfDll(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
    {
        if (!string.Equals(libraryName, DllName, StringComparison.Ordinal))
        {
            return false;
        }
        var assemblyDirectory = GetDirectoryOrDefault(Path.GetDirectoryName(assembly.Location));
        var baseDirectory = GetDirectoryOrDefault(AppContext.BaseDirectory);
        var fileName = GetLibraryFileName();
        var runtimeIdentifier = GetRuntimeIdentifier();

        if (TryVerifyFromDirectory(assemblyDirectory, fileName, assembly, searchPath) ||
            TryVerifyFromDirectory(baseDirectory, fileName, assembly, searchPath) ||
            TryVerifyFromDirectory(Path.Combine(assemblyDirectory, "runtimes", runtimeIdentifier, "native"), fileName, assembly, searchPath) ||
            TryVerifyFromDirectory(Path.Combine(baseDirectory, "runtimes", runtimeIdentifier, "native"), fileName, assembly, searchPath))
        {
            return true;
        }
        return false;
    }
}