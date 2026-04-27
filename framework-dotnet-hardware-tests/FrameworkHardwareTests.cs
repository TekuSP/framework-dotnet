using FrameworkDotnet.Enums;
using FrameworkDotnet.Exceptions.EcResponseDetails;
using FrameworkDotnet.Interfaces;
using FrameworkDotnet.Responses;

using Microsoft.Extensions.DependencyInjection;

using NUnit.Framework;
using NUnit.Framework.Internal;

using UnitsNet;

namespace FrameworkDotnet.HardwareTests;

[TestFixture]
[Description("Hardware-dependent tests intended for manual execution on supported Framework devices.")]
[Author("TekuSP", "richard.torhan@windowslive.com")]
[Category("Hardware")]
[Platform(platforms: "Windows10,Windows11,WindowsServer10,Linux", Reason ="Hardware tests require a supported Framework device.")]
[Platform(platforms: "64-Bit-OS", Reason = "Hardware tests require a supported Framework device.")]
public sealed class FrameworkHardwareTests
{
    private IFrameworkSystem frameworkSystem = null!;
    private IFrameworkEcConnection ec = null!;

    [OneTimeSetUp]
    public void PrepareHardwareTestEnvironment()
    {
        frameworkSystem = new FrameworkSystem();

        Assert.That(frameworkSystem.IsLibraryAvailable, Is.True, "The native library must be available for hardware tests.");
        Assert.That(frameworkSystem.IsFrameworkDevice, Is.True, "Hardware tests require a supported Framework device.");
    }

    [SetUp]
    public void OpenDefaultEcConnection()
    {
        ec = frameworkSystem.OpenDefaultEc();
        Assert.That(ec, Is.Not.Null, "The default EC connection could not be opened for the test.");
    }

    [TearDown]
    public void DisposeDefaultEcConnection()
    {
        ec?.Dispose();
        ec = null!;
    }

    [Test]
    public void SystemDiscovery_ShouldReturnBasicInformation()
    {
        Assert.That(frameworkSystem.GetProductName(), Is.Not.Null.And.Not.Empty);
        Assert.That(Enum.IsDefined(frameworkSystem.GetPlatform()));
        Assert.That(Enum.IsDefined(frameworkSystem.GetPlatformFamily()));
    }

    [Test]
    public void DriverSupportQuery_ShouldCompleteForAllDrivers()
    {
        foreach (var driver in Enum.GetValues<FrameworkEcDriver>())
        {
            Assert.DoesNotThrow(() => _ = frameworkSystem.IsDriverSupported(driver), $"Driver support query failed for {driver}.");
        }
    }

    [Test]
    public void DependencyInjection_ShouldResolvePublicApiServices()
    {
        var services = new ServiceCollection();

        services.AddSingleton<IFrameworkEcConnectionFactory, FrameworkSystem>(_ => (FrameworkSystem)this.frameworkSystem);
        services.AddSingleton<IFrameworkSystem, FrameworkSystem>(_ => (FrameworkSystem)this.frameworkSystem);

        using var serviceProvider = services.BuildServiceProvider();

        var frameworkSystem = serviceProvider.GetRequiredService<IFrameworkSystem>();
        var ecConnectionFactory = serviceProvider.GetRequiredService<IFrameworkEcConnectionFactory>();

        Assert.That(frameworkSystem, Is.Not.Null);
        Assert.That(ecConnectionFactory, Is.Not.Null);
        Assert.That(ReferenceEquals(frameworkSystem, ecConnectionFactory), Is.True);
        Assert.That(frameworkSystem.IsLibraryAvailable, Is.True);
        Assert.That(frameworkSystem.IsFrameworkDevice, Is.True);

        using var ec = ecConnectionFactory.OpenDefaultEc();
        Assert.That(ec, Is.Not.Null);
    }

    [Test]
    public void Connection_ShouldReturnActiveDriverAndBuildInfo()
    {
        Assert.That(Enum.IsDefined(ec.GetActiveDriver()));
        Assert.That(ec.GetBuildInfo(), Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void OpenEcWithSupportedDrivers_ShouldReturnConnection_WhenDriverIsReportedSupported()
    {
        foreach (var driver in Enum.GetValues<FrameworkEcDriver>())
        {
            if (!frameworkSystem.IsDriverSupported(driver))
            {
                continue;
            }

            using var ec = frameworkSystem.OpenEcWithDriver(driver);
            Assert.That(ec, Is.Not.Null, $"OpenEcWithDriver returned null for {driver}.");
            Assert.That(Enum.IsDefined(ec.GetActiveDriver()));
        }
    }

    [Test]
    public void FlashSnapshot_ShouldReturnExpectedInformation()
    {
        var flash = ec.GetFlashSnapshot();
        Assert.That(Enum.IsDefined(flash.CurrentImage));
        Assert.That(flash.RoVersion, Is.Not.Null.And.Not.Empty);
        Assert.That(flash.RwVersion, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void PowerSnapshot_ShouldReturnExpectedInformation()
    {
        var power = ec.GetPowerSnapshot();
        Assert.That(Enum.IsDefined(power.PowerSourceState));
        Assert.That(power.BatteryCount, Is.GreaterThanOrEqualTo((byte)0));
        Assert.That(power.Batteries, Has.Count.EqualTo(1));
        Assert.That(power.ReportedBatteries.Count(), Is.EqualTo(power.BatteryCount));

        foreach (var battery in power.ReportedBatteries)
        {
            Assert.That(battery.Manufacturer, Is.Not.Null);
            Assert.That(battery.ModelNumber, Is.Not.Null);
            Assert.That(battery.SerialNumber, Is.Not.Null);
            Assert.That(battery.BatteryType, Is.Not.Null);
            Assert.That(battery.CycleCount, Is.GreaterThanOrEqualTo(0U));
            Assert.That(battery.ChargeLevel.Percent, Is.InRange(0, 100));
        }
    }

    [Test]
    public void FanCapabilitiesSnapshot_ShouldReturnExpectedInformation()
    {
        var fanCapabilities = ec.GetFanCapabilitiesSnapshot();
        Assert.That(fanCapabilities.FanCount, Is.GreaterThanOrEqualTo((byte)0));
        Assert.That(Enum.IsDefined(fanCapabilities.Features));
    }

    [Test]
    public void ThermalSnapshot_ShouldReturnExpectedInformation()
    {
        var thermal = ec.GetThermalSnapshot();
        Assert.That(thermal.SensorCount, Is.InRange((byte)0, (byte)8));
        Assert.That(thermal.FanCount, Is.InRange((byte)0, (byte)4));
        Assert.That(thermal.Temperatures, Has.Count.EqualTo(8));
        Assert.That(thermal.Fans, Has.Count.EqualTo(4));
        Assert.That(thermal.ReportedTemperatures.Count(), Is.EqualTo(thermal.SensorCount));
        Assert.That(thermal.ReportedFans.Count(), Is.EqualTo(thermal.FanCount));

        foreach (var temperature in thermal.ReportedTemperatures)
        {
            Assert.That(Enum.IsDefined(temperature.State));
            Assert.That(double.IsNaN(temperature.Temperature.DegreesCelsius), Is.False);
        }

        foreach (var fan in thermal.ReportedFans)
        {
            Assert.That(Enum.IsDefined(fan.FanState));
            Assert.That(fan.Speed.RevolutionsPerMinute, Is.GreaterThanOrEqualTo(0));
        }
    }

    [Test]
    public void FanControlCommands_ShouldReturnStructuredResponses_WhenFansAreReported()
    {
        var fanCapabilities = ec.GetFanCapabilitiesSnapshot();

        Assume.That(fanCapabilities.FanCount, Is.GreaterThan((byte)0), "This device did not report any controllable fans.");

        FrameworkRestoreAutoFanControlResponse restoreBefore = ec.RestoreAutoFanControl(0);
        Assert.That(restoreBefore.FanIndex, Is.EqualTo(0));

        FrameworkSetFanDutyResponse dutyResponse = ec.SetFanDuty(0, Ratio.FromPercent(30));
        Assert.That(dutyResponse.FanIndex, Is.EqualTo(0));
        Assert.That(dutyResponse.AppliedDutyCycle.Percent, Is.EqualTo(30).Within(0.001));

        FrameworkSetFanRpmResponse rpmResponse = ec.SetFanRpm(0, RotationalSpeed.FromRevolutionsPerMinute(2500));
        Assert.That(rpmResponse.FanIndex, Is.EqualTo(0));
        Assert.That(rpmResponse.AppliedSpeed.RevolutionsPerMinute, Is.EqualTo(2500).Within(0.001));

        FrameworkRestoreAutoFanControlResponse restoreAfter = ec.RestoreAutoFanControl(0);
        Assert.That(restoreAfter.FanIndex, Is.EqualTo(0));
    }

    [Test]
    public void SetFanDuty_ShouldThrowArgumentOutOfRangeException_ForImpossibleDutyCycleValues()
    {
        Assert.That(
            () => ec.SetFanDuty(0, Ratio.FromPercent(-1)),
            Throws.TypeOf<ArgumentOutOfRangeException>());

        Assert.That(
            () => ec.SetFanDuty(0, Ratio.FromPercent(30.5)),
            Throws.TypeOf<ArgumentOutOfRangeException>());

        Assert.That(
            () => ec.SetFanDuty(50, Ratio.FromPercent(10)),
            Throws.TypeOf<FrameworkErrorEcResponseException>());
    }

    [Test]
    public void SetFanRpm_ShouldThrowArgumentOutOfRangeException_ForImpossibleSpeedValues()
    {
        Assert.That(
            () => ec.SetFanRpm(0, RotationalSpeed.FromRevolutionsPerMinute(-1)),
            Throws.TypeOf<ArgumentOutOfRangeException>());

        Assert.That(
            () => ec.SetFanRpm(0, RotationalSpeed.FromRevolutionsPerMinute(2500.5)),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }
}
