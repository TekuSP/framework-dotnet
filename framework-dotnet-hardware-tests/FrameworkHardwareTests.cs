using FrameworkDotnet.Enums;
using FrameworkDotnet.Exceptions.EcResponseDetails;
using FrameworkDotnet.Exceptions.StatusCodes;
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
[Platform(platforms: "Windows10,Windows11,WindowsServer10,Linux,64-Bit-OS", Reason = "Hardware tests require a supported Framework device.")]
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
    public void FeatureFlags_ShouldReturnKnownManagedBits()
    {
        var featureFlags = ec.GetFeatureFlags();
        Assert.That((ulong)featureFlags & ~(ulong)FrameworkEcFeatureFlags.All, Is.EqualTo(0UL));
    }

    [Test]
    public void KeyboardBacklightSnapshot_ShouldReturnExpectedInformationOrReportUnavailable()
    {
        AssertOptionalReadback(
            () => ec.GetKeyboardBacklightSnapshot(),
            keyboardBacklight =>
            {
                Assert.That(double.IsNaN(keyboardBacklight.Brightness.Percent), Is.False);
                Assert.That(keyboardBacklight.Brightness.Percent, Is.InRange(0, 100));
            });
    }

    [Test]
    public void FingerprintLedSnapshot_ShouldReturnExpectedInformationOrReportUnavailable()
    {
        AssertOptionalReadback(
            () => ec.GetFingerprintLedSnapshot(),
            fingerprintLed =>
            {
                Assert.That(Enum.IsDefined(fingerprintLed.Level));
            });
    }

    [Test]
    public void ExpansionBaySnapshot_ShouldReturnExpectedInformationOrReportUnavailable()
    {
        AssertOptionalReadback(
            () => ec.GetExpansionBaySnapshot(),
            expansionBay =>
            {
                Assert.That(Enum.IsDefined(expansionBay.Board));
                Assert.That(Enum.IsDefined(expansionBay.Vendor));
                Assert.That(Enum.IsDefined(expansionBay.PcieConfiguration));
                Assert.That(expansionBay.SerialNumber, Is.Not.Null);
            });
    }

    [Test]
    public void GpuDescriptorHeaderSnapshot_ShouldReturnExpectedInformationOrReportUnavailable()
    {
        AssertOptionalReadback(
            () => ec.GetGpuDescriptorHeaderSnapshot(),
            header =>
            {
                Assert.That(header.RawMagicBytes, Has.Count.EqualTo(4));
                Assert.That(Enum.IsDefined(header.BayType));
                Assert.That(header.DescriptorVersion, Is.Not.Null);
                Assert.That(header.HardwareVersion, Is.Not.Null);
                Assert.That(header.Serial, Is.Not.Null);
                Assert.That(header.Header, Is.Not.Null);
                Assert.That(header.Header, Has.Count.GreaterThan(0));
                Assert.That(header.Payload, Is.Not.Null);

                if (header.BayType == FrameworkGpuDescriptorMagic.FrameworkExpansionBay)
                {
                    Assert.That(header.Payload, Is.Not.Empty);
                }
                else
                {
                    Assert.That(header.Payload, Is.Empty);
                }
            });
    }

    [Test]
    public void GpuDescriptorReadback_ShouldValidateOrReportUnavailable()
    {
        AssertOptionalReadback(
            () => ec.GetGpuDescriptorHeaderSnapshot(),
            header =>
            {
                Assume.That(
                    header.BayType,
                    Is.EqualTo(FrameworkGpuDescriptorMagic.FrameworkExpansionBay),
                    "This device did not report a readable Framework expansion bay GPU descriptor.");

                var descriptor = ec.ReadGpuDescriptor();

                Assert.That(descriptor, Is.Not.Null.And.Not.Empty);
                Assert.That(descriptor, Has.Length.EqualTo(header.Header.Count + header.Payload.Count));
                Assert.That(descriptor.Take(header.Header.Count).SequenceEqual(header.Header), Is.True);
                Assert.That(descriptor.Skip(header.Header.Count).SequenceEqual(header.Payload), Is.True);
                Assert.That(ec.ValidateGpuDescriptor(descriptor), Is.True);
            });
    }

    [Test]
    public void ModuleInventorySnapshot_ShouldReturnExpectedInformationOrReportUnavailable()
    {
        AssertOptionalReadback(
            () => ec.GetModuleInventorySnapshot(),
            inventory =>
            {
                Assert.That(inventory.Modules, Has.Count.EqualTo(22));
                Assert.That(inventory.UsbCSlots, Has.Count.EqualTo(6));
                Assert.That(inventory.InputTopRowModules, Has.Count.EqualTo(5));
                Assert.That(inventory.FixedModules, Has.Count.EqualTo(7));
                Assert.That(inventory.DetachedModules, Has.Count.EqualTo(4));
                Assert.That(inventory.ReportedModules.Count(), Is.EqualTo(inventory.ModuleCount));
                Assert.That(inventory.ReportedUsbCSlots.Count(), Is.EqualTo(inventory.UsbCSlotCount));
                Assert.That(inventory.ReportedInputTopRowModules.Count(), Is.EqualTo(inventory.InputTopRowCount));
                Assert.That(inventory.ReportedFixedModules.Count(), Is.EqualTo(inventory.FixedModuleCount));
                Assert.That(inventory.ReportedDetachedModules.Count(), Is.EqualTo(inventory.DetachedCount));

                foreach (var module in inventory.ReportedFixedModules)
                {
                    Assert.That(module.IsPresent, Is.True);
                }

                foreach (var module in inventory.ReportedModules)
                {
                    Assert.That(Enum.IsDefined(module.Identity));
                    Assert.That(Enum.IsDefined(module.Bus));
                    Assert.That(Enum.IsDefined(module.SlotKind));
                    Assert.That(Enum.IsDefined(module.Confidence));
                    Assert.That((uint)module.Flags & ~(uint)FrameworkModuleFlags.All, Is.EqualTo(0U));
                }
            });
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

    private static void AssertOptionalReadback<T>(Func<T> readback, Action<T> assertions)
    {
        try
        {
            assertions(readback());
        }
        catch (FrameworkDataUnavailableStatusException)
        {
        }
    }
}
