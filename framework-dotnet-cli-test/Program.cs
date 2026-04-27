using FrameworkDotnet;
using FrameworkDotnet.Enums;

using Spectre.Console;

using System.Text;

namespace framework_dotnet_cli_test;

internal class Program
{
    static void Main(string[] args)
    {
        try
        {
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
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}
