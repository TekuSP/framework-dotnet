using FrameworkDotnet;
using FrameworkDotnet.Enums;
using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Exceptions.StatusCodes;
using FrameworkDotnet.Interfaces;
using FrameworkDotnet.Snapshots;

using Spectre.Console;

using System.Globalization;
using System.Linq;
using System.Text;

namespace framework_dotnet_cli_test;

internal class Program
{
    private static readonly TimeSpan RefreshInterval = TimeSpan.FromSeconds(10);
    private const int HexPreviewBytes = 64;
    private const int HexBytesPerLine = 16;

    static void Main(string[] args)
    {
        try
        {
            FrameworkSystem frameworkSystem = new FrameworkSystem();
            using IFrameworkEcConnection ec = frameworkSystem.OpenDefaultEc();

            while (true)
            {
                AnsiConsole.Clear();

                FrameworkFanCapabilitiesSnapshot fanCapabilitiesSnapshot = ec.GetFanCapabilitiesSnapshot();
                FrameworkPowerSnapshot powerSnapshot = ec.GetPowerSnapshot();
                FrameworkThermalSnapshot thermalSnapshot = ec.GetThermalSnapshot();

                WritePanel(CreateSystemInfoPanel(frameworkSystem, ec));
                WritePanel(CreatePanel("[bold magenta]Fan Capabilities Snapshot[/]", Color.Magenta, FormatSimpleSnapshot(fanCapabilitiesSnapshot)));
                WritePanel(CreatePanel("[bold green]Power Snapshot[/]", Color.Green, FormatSimpleSnapshot(powerSnapshot)));
                WritePanel(CreatePanel("[bold red]Thermal Snapshot[/]", Color.Red, FormatSimpleSnapshot(thermalSnapshot)));

                WritePanel(CreateOptionalPanel("[bold yellow]Feature Flags[/]", Color.Yellow, () => FormatFeatureFlags(ec.GetFeatureFlags())));
                WritePanel(CreateOptionalPanel("[bold blue]Keyboard Backlight[/]", Color.Blue, () => FormatSimpleSnapshot(ec.GetKeyboardBacklightSnapshot())));
                WritePanel(CreateOptionalPanel("[bold magenta]Fingerprint LED[/]", Color.Magenta, () => FormatSimpleSnapshot(ec.GetFingerprintLedSnapshot())));
                WritePanel(CreateOptionalPanel("[bold green]Expansion Bay[/]", Color.Green, () => FormatSimpleSnapshot(ec.GetExpansionBaySnapshot())));
                WritePanel(CreateOptionalPanel("[bold red]GPU Descriptor[/]", Color.Red, () => FormatGpuDescriptor(ec)));
                WritePanel(CreateOptionalPanel("[bold blue]Module Inventory[/]", Color.Blue, () => FormatModuleInventory(ec.GetModuleInventorySnapshot())));

                Thread.Sleep(RefreshInterval);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    private static Panel CreateSystemInfoPanel(FrameworkSystem frameworkSystem, IFrameworkEcConnection ec)
    {
        var sysInfo = new StringBuilder();

        sysInfo.AppendLine($"[cyan]Product:[/] {Markup.Escape(frameworkSystem.GetProductName())}");
        sysInfo.AppendLine($"[cyan]Platform:[/] {Markup.Escape(frameworkSystem.GetPlatform().ToString())}");
        sysInfo.AppendLine($"[cyan]Family:[/] {Markup.Escape(frameworkSystem.GetPlatformFamily().ToString())}");
        sysInfo.AppendLine();

        sysInfo.AppendLine("[cyan bold]Driver support:[/]");
        foreach (FrameworkEcDriver driver in Enum.GetValues<FrameworkEcDriver>())
        {
            bool isSupported = frameworkSystem.IsDriverSupported(driver);
            string color = isSupported ? "green" : "red";
            sysInfo.AppendLine($"  [teal]{driver}:[/] [{color}]{isSupported}[/]");
        }

        sysInfo.AppendLine();
        sysInfo.AppendLine($"[cyan]Active driver:[/] {Markup.Escape(ec.GetActiveDriver().ToString())}");
        sysInfo.AppendLine($"[cyan]Build info:[/] [grey]{Markup.Escape(ec.GetBuildInfo())}[/]");

        FrameworkEcFlashSnapshot flash = ec.GetFlashSnapshot();
        sysInfo.AppendLine($"[cyan]Flash:[/] {Markup.Escape(flash.CurrentImage.ToString())}, [grey]RO=[/][yellow]{Markup.Escape(flash.RoVersion)}[/], [grey]RW=[/][yellow]{Markup.Escape(flash.RwVersion)}[/]");

        return new Panel(new Markup(sysInfo.ToString()))
            .Header("[bold blue]System Information[/]")
            .BorderColor(Color.Blue);
    }

    private static Panel CreatePanel(string title, Color borderColor, string content)
    {
        return new Panel(content)
            .Header(title)
            .BorderColor(borderColor);
    }

    private static Panel CreateOptionalPanel(string title, Color borderColor, Func<string> contentFactory)
    {
        try
        {
            return CreatePanel(title, borderColor, contentFactory());
        }
        catch (FrameworkDataUnavailableStatusException)
        {
            return CreatePanel(title, borderColor, "Unavailable on this device.");
        }
        catch (FrameworkException ex)
        {
            return CreatePanel(title, borderColor, $"Framework error: {ex.Message}");
        }
        catch (Exception ex)
        {
            return CreatePanel(title, borderColor, $"{ex.GetType().Name}: {ex.Message}");
        }
    }

    private static void WritePanel(Panel panel)
    {
        AnsiConsole.Write(panel);
        AnsiConsole.WriteLine();
    }

    private static string FormatSimpleSnapshot(object snapshot)
    {
        return snapshot.ToString()?.Replace(", ", Environment.NewLine) ?? string.Empty;
    }

    private static string FormatFeatureFlags(FrameworkEcFeatureFlags flags)
    {
        FrameworkEcFeatureFlags[] enabledFlags = Enum.GetValues<FrameworkEcFeatureFlags>()
            .Where(static flag => flag != FrameworkEcFeatureFlags.None && flag != FrameworkEcFeatureFlags.All)
            .Where(flag => (flags & flag) == flag)
            .ToArray();

        var content = new StringBuilder();
        content.AppendLine($"Raw Value: 0x{((ulong)flags).ToString("X", CultureInfo.InvariantCulture)}");

        if (enabledFlags.Length == 0)
        {
            content.Append("Enabled: None");
            return content.ToString();
        }

        content.AppendLine("Enabled:");
        foreach (FrameworkEcFeatureFlags enabledFlag in enabledFlags)
        {
            content.AppendLine($"- {enabledFlag}");
        }

        return content.ToString().TrimEnd();
    }

    private static string FormatGpuDescriptor(IFrameworkEcConnection ec)
    {
        FrameworkGpuDescriptorHeaderSnapshot header = ec.GetGpuDescriptorHeaderSnapshot();

        var content = new StringBuilder();
        content.AppendLine($"Bay Type: {header.BayType}");
        content.AppendLine($"Descriptor Version: {header.DescriptorVersion}");
        content.AppendLine($"Hardware Version: {header.HardwareVersion}");
        content.AppendLine($"Serial: {header.Serial}");
        content.AppendLine($"Magic Bytes: {FormatHexPreview(header.RawMagicBytes, 4)}");
        content.AppendLine($"Header Bytes: {header.Header.Count.ToString(CultureInfo.InvariantCulture)}");
        content.AppendLine("Header Preview:");
        content.AppendLine(FormatHexPreview(header.Header));

        if (header.Payload.Count == 0)
        {
            content.AppendLine("Payload: <none>");
        }
        else
        {
            content.AppendLine($"Payload Bytes: {header.Payload.Count.ToString(CultureInfo.InvariantCulture)}");
            content.AppendLine("Payload Preview:");
            content.AppendLine(FormatHexPreview(header.Payload));
        }

        if (header.BayType == FrameworkGpuDescriptorMagic.FrameworkExpansionBay)
        {
            try
            {
                byte[] descriptor = ec.ReadGpuDescriptor();
                content.AppendLine($"ReadGpuDescriptor Bytes: {descriptor.Length.ToString(CultureInfo.InvariantCulture)}");
                content.AppendLine($"ValidateGpuDescriptor: {ec.ValidateGpuDescriptor(descriptor)}");
            }
            catch (FrameworkException ex)
            {
                content.AppendLine($"Read/Validate Error: {ex.Message}");
            }
        }

        return content.ToString().TrimEnd();
    }

    private static string FormatModuleInventory(FrameworkModuleInventorySnapshot inventory)
    {
        var content = new StringBuilder();
        content.AppendLine($"Module Count: {inventory.ModuleCount.ToString(CultureInfo.InvariantCulture)}");

        AppendModuleGroup(content, "USB-C Slots", inventory.ReportedUsbCSlots);
        AppendModuleGroup(content, "Input Top Row Modules", inventory.ReportedInputTopRowModules);
        AppendModuleGroup(content, "Fixed Modules", inventory.ReportedFixedModules);
        AppendModuleGroup(content, "Detached Modules", inventory.ReportedDetachedModules);

        return content.ToString().TrimEnd();
    }

    private static void AppendModuleGroup(StringBuilder content, string groupName, IEnumerable<FrameworkModuleDescriptorSnapshot> modules)
    {
        List<FrameworkModuleDescriptorSnapshot> reportedModules = [.. modules];

        content.AppendLine();
        content.AppendLine($"{groupName}: {reportedModules.Count.ToString(CultureInfo.InvariantCulture)}");

        if (reportedModules.Count == 0)
        {
            content.AppendLine("  <none>");
            return;
        }

        foreach (FrameworkModuleDescriptorSnapshot module in reportedModules)
        {
            content.Append("  - ");
            content.Append(module.Identity);

            if (module.SlotKind != FrameworkModuleSlotKind.None)
            {
                content.Append(" [");
                content.Append(module.SlotKind);

                if (module.SlotIndex >= 0)
                {
                    content.Append(" #");
                    content.Append(module.SlotIndex.ToString(CultureInfo.InvariantCulture));
                }

                content.Append(']');
            }

            content.Append($", Bus={module.Bus}, Confidence={module.Confidence}");

            if (module.Flags != FrameworkModuleFlags.None)
            {
                content.Append($", Flags={module.Flags}");
            }

            if (module.VendorId != 0 || module.ProductId != 0)
            {
                content.Append($", VID:PID=0x{module.VendorId.ToString("X4", CultureInfo.InvariantCulture)}:0x{module.ProductId.ToString("X4", CultureInfo.InvariantCulture)}");
            }

            if (module.BoardId != 0)
            {
                content.Append($", Board ID={module.BoardId.ToString(CultureInfo.InvariantCulture)}");
            }

            content.AppendLine();
        }
    }

    private static string FormatHexPreview(IReadOnlyList<byte> bytes, int maxBytes = HexPreviewBytes)
    {
        if (bytes.Count == 0)
        {
            return "<none>";
        }

        int displayedByteCount = Math.Min(bytes.Count, maxBytes);
        var content = new StringBuilder();

        for (int index = 0; index < displayedByteCount; index++)
        {
            if (index > 0)
            {
                content.Append(index % HexBytesPerLine == 0 ? Environment.NewLine : " ");
            }

            content.Append(bytes[index].ToString("X2", CultureInfo.InvariantCulture));
        }

        if (bytes.Count > displayedByteCount)
        {
            content.AppendLine();
            content.Append($"... ({(bytes.Count - displayedByteCount).ToString(CultureInfo.InvariantCulture)} more bytes)");
        }

        return content.ToString();
    }
}
