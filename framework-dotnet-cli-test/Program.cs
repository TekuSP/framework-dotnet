using FrameworkDotnet;
using FrameworkDotnet.Enums;

namespace framework_dotnet_cli_test;

internal class Program
{
    static void Main(string[] args)
    {
        try
        {
            FrameworkSystem FrameworkSystem = new FrameworkSystem();
            Console.WriteLine($"Product: {FrameworkSystem.GetProductName()}");
            Console.WriteLine($"Platform: {FrameworkSystem.GetPlatform()}");
            Console.WriteLine($"Family: {FrameworkSystem.GetPlatformFamily()}");

            Console.WriteLine("Driver support:");
            foreach (FrameworkEcDriver driver in Enum.GetValues<FrameworkEcDriver>())
            {
                Console.WriteLine($"  {driver}: {FrameworkSystem.IsDriverSupported(driver)}");
            }

            using var ec = FrameworkSystem.OpenDefaultEc();

            Console.WriteLine($"Active driver: {ec.GetActiveDriver()}");
            Console.WriteLine($"Build info: {ec.GetBuildInfo()}");

            var flash = ec.GetFlashSnapshot();
            Console.WriteLine($"Flash: {flash.CurrentImage}, RO={flash.RoVersion}, RW={flash.RwVersion}");

            var power = ec.GetPowerSnapshot();

            var thermal = ec.GetThermalSnapshot();
            Console.WriteLine($"Fan count: {thermal.FanCount}");
            Console.WriteLine($"Temp0: {thermal.Temperature_0.Temperature.DegreesCelsius}C ({thermal.Temperature_0.State})");

            var restoreAutoFanControlResponse = ec.RestoreAutoFanControl(0);
            Console.WriteLine($"Auto fan control restored for fan {restoreAutoFanControlResponse.FanIndex}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}
