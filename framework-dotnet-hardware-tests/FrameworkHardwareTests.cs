using FrameworkDotnet.Enums;
using FrameworkDotnet.Exceptions;
using FrameworkDotnet.Exceptions.EcResponseDetails;
using FrameworkDotnet.Exceptions.StatusCodes;
using FrameworkDotnet.Interfaces;
using FrameworkDotnet.Responses;
using FrameworkDotnet.Snapshots;

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
                AssertExpansionBayClassification(expansionBay, allowGenericBaseType: true);
                Assert.That(Enum.IsDefined(expansionBay.Board));
                Assert.That(Enum.IsDefined(expansionBay.Vendor));
                Assert.That(expansionBay.SerialNumber, Is.Not.Null);
            });
    }

    [Test]
    [Description("Framework 16 exposes 4 mainboard USB-C PD ports; a graphics module adds a 5th (4 + GPU = 5).")]
    public void ModuleInventory_Framework16_ReportsFourMainboardPdPortsPlusGraphicsModule()
    {
        if (frameworkSystem.GetPlatformFamily() != FrameworkPlatformFamily.Framework16)
        {
            Assert.Ignore("This test only applies to the Framework Laptop 16.");
        }

        var inventory = ec.GetModuleInventorySnapshot();

        // Upstream framework-system exposes exactly 4 EC PD ports on the mainboard; the six physical bays mux onto
        // them (power.rs: `let ports = 4`).
        Assert.That(inventory.UsbCSlotCount, Is.EqualTo(4), "Framework 16 exposes 4 mainboard PD ports.");
        var mainboardPorts = inventory.ReportedUsbCSlots.ToList();
        Assert.That(mainboardPorts, Has.Count.EqualTo(4));

        foreach (var port in mainboardPorts)
        {
            Assert.That(port.Capability.IsDocumented, Is.True, $"Port {port.SlotIndex} should have a documented capability.");
            Assert.That(port.Capability.Position, Is.Not.EqualTo(FrameworkUsbCPortPosition.Unknown), $"Port {port.SlotIndex} should have a known position.");
            Assert.That(port.Capability.PositionName, Is.Not.Empty);
        }

        // Upstream index order: 0 Right Back, 1 Right Middle, 2 Left Middle, 3 Left Back; ports 2 & 3 are on the left.
        Assert.That(inventory.UsbCSlot_0.Capability.Position, Is.EqualTo(FrameworkUsbCPortPosition.RightBack));
        Assert.That(inventory.UsbCSlot_1.Capability.Position, Is.EqualTo(FrameworkUsbCPortPosition.RightMiddle));
        Assert.That(inventory.UsbCSlot_2.Capability.Position, Is.EqualTo(FrameworkUsbCPortPosition.LeftMiddle));
        Assert.That(inventory.UsbCSlot_3.Capability.Position, Is.EqualTo(FrameworkUsbCPortPosition.LeftBack));
        Assert.That(inventory.UsbCSlot_2.Capability.IsLeftSide, Is.True);
        Assert.That(inventory.UsbCSlot_0.Capability.IsLeftSide, Is.False);

        // A graphics module adds a 5th PD port (its rear USB-C port).
        var expansionBay = ec.GetExpansionBaySnapshot();
        var totalPdPorts = inventory.UsbCSlotCount + (expansionBay.HasUsbCPort ? 1 : 0);
        if (expansionBay.HasUsbCPort)
        {
            Assert.That(expansionBay.UsbCCapability, Is.Not.Null);
            Assert.That(expansionBay.UsbCCapability!.Position, Is.EqualTo(FrameworkUsbCPortPosition.GraphicsModule));
            Assert.That(totalPdPorts, Is.EqualTo(5), "4 mainboard PD ports + 1 graphics-module port = 5.");
        }
        else
        {
            Assert.That(totalPdPorts, Is.EqualTo(4), "Without a graphics module, Framework 16 exposes 4 PD ports.");
            Assert.Warn("No graphics module detected; a Framework 16 with a dGPU should report 5 (4 + GPU).");
        }
    }

    [Test]
    public void ExpansionBayModulesSnapshot_ShouldReturnExpectedInformationOrReportUnavailable()
    {
        AssertOptionalReadback(
            () => ec.GetExpansionBayModulesSnapshot(),
            modules =>
            {
                Assert.That(modules.ExpansionBayCount, Is.InRange((byte)0, (byte)1));
                Assert.That(modules.ExpansionBays, Has.Count.EqualTo(1));
                Assert.That(modules.ReportedExpansionBays.Count(), Is.EqualTo(modules.ExpansionBayCount));

                if (modules.ExpansionBayCount == 0)
                {
                    Assert.That(modules.ExpansionBay_0.IsPresent, Is.False);
                }

                foreach (var bay in modules.ReportedExpansionBays)
                {
                    AssertExpansionBayClassification(bay, allowGenericBaseType: false);
                    Assert.That(Enum.IsDefined(bay.Board));
                    Assert.That(Enum.IsDefined(bay.Vendor));
                    Assert.That(bay.SerialNumber, Is.Not.Null);

                    if (bay is FrameworkPcieExpansionBaySnapshot pcieExpansionBay)
                    {
                        Assert.That(Enum.IsDefined(pcieExpansionBay.PcieConfiguration));
                    }

                    if (bay is FrameworkGpuExpansionBaySnapshot gpuExpansionBay)
                    {
                        Assert.That(gpuExpansionBay.HasGpuDescriptor, Is.True);
                        Assert.That(gpuExpansionBay.GpuDescriptorRawMagicBytes, Has.Count.EqualTo(4));
                        Assert.That(gpuExpansionBay.GpuDescriptorBayType, Is.Not.Null);
                        Assert.That(Enum.IsDefined(gpuExpansionBay.GpuDescriptorBayType!.Value));
                        Assert.That(gpuExpansionBay.GpuDescriptorVersion, Is.Not.Null);
                        Assert.That(gpuExpansionBay.GpuDescriptorHardwareVersion, Is.Not.Null);
                        Assert.That(gpuExpansionBay.GpuDescriptorSerial, Is.Not.Null);
                        Assert.That(gpuExpansionBay.GpuDescriptorHeader, Is.Not.Null.And.Not.Empty);
                        Assert.That(gpuExpansionBay.GpuDescriptorPayload, Is.Not.Null);
                    }
                }
            });
    }

    [Test]
    public void GpuDescriptorReadback_ShouldValidateOrReportUnavailable()
    {
        AssertOptionalReadback(
            () => ec.GetExpansionBayModulesSnapshot(),
            modules =>
            {
                FrameworkGpuExpansionBaySnapshot? gpuExpansionBay = modules.ReportedExpansionBays.OfType<FrameworkGpuExpansionBaySnapshot>().SingleOrDefault();

                Assume.That(
                    gpuExpansionBay,
                    Is.Not.Null,
                    "This device did not report a GPU expansion bay module.");
                Assert.That(gpuExpansionBay!.HasGpuDescriptor, Is.True);

                Assume.That(
                    gpuExpansionBay.GpuDescriptorBayType,
                    Is.EqualTo(FrameworkGpuDescriptorMagic.FrameworkExpansionBay),
                    "This device did not report a readable Framework expansion bay GPU descriptor.");

                var descriptor = ec.ReadGpuDescriptor();

                Assert.That(descriptor, Is.Not.Null.And.Not.Empty);
                Assert.That(descriptor, Has.Length.EqualTo(gpuExpansionBay.GpuDescriptorHeader!.Count + gpuExpansionBay.GpuDescriptorPayload!.Count));
                Assert.That(descriptor.Take(gpuExpansionBay.GpuDescriptorHeader.Count).SequenceEqual(gpuExpansionBay.GpuDescriptorHeader), Is.True);
                Assert.That(descriptor.Skip(gpuExpansionBay.GpuDescriptorHeader.Count).SequenceEqual(gpuExpansionBay.GpuDescriptorPayload), Is.True);
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
                Assert.That(inventory.UsbCSlots, Has.Count.EqualTo(6));
                Assert.That(inventory.InputTopRowModules, Has.Count.EqualTo(5));
                Assert.That(inventory.FixedModules, Has.Count.EqualTo(7));
                Assert.That(inventory.DetachedModules, Has.Count.EqualTo(4));
                Assert.That(
                    inventory.ReportedUsbCSlots.Count() + inventory.ReportedInputTopRowModules.Count() + inventory.ReportedFixedModules.Count() + inventory.ReportedDetachedModules.Count(),
                    Is.EqualTo(inventory.ModuleCount));
                Assert.That(inventory.ReportedUsbCSlots.Count(), Is.EqualTo(inventory.UsbCSlotCount));
                Assert.That(inventory.ReportedInputTopRowModules.Count(), Is.EqualTo(inventory.InputTopRowCount));
                Assert.That(inventory.ReportedFixedModules.Count(), Is.EqualTo(inventory.FixedModuleCount));
                Assert.That(inventory.ReportedDetachedModules.Count(), Is.EqualTo(inventory.DetachedCount));

                foreach (var module in inventory.ReportedFixedModules)
                {
                    Assert.That(module.IsPresent, Is.True);
                }

                foreach (var slot in inventory.ReportedUsbCSlots)
                {
                    Assert.That(Enum.IsDefined(slot.Identity));
                    Assert.That(Enum.IsDefined(slot.Bus));
                    Assert.That(Enum.IsDefined(slot.SlotKind));
                    Assert.That(Enum.IsDefined(slot.Confidence));
                    Assert.That((uint)slot.Flags & ~(uint)FrameworkModuleFlags.All, Is.EqualTo(0U));
                }

                foreach (var module in inventory.ReportedInputTopRowModules.Concat(inventory.ReportedFixedModules).Concat(inventory.ReportedDetachedModules))
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

    private static void AssertExpansionBayClassification(FrameworkExpansionBaySnapshot expansionBay, bool allowGenericBaseType)
    {
        Assert.That(Enum.IsDefined(expansionBay.Identity));

        if (!expansionBay.IsPresent)
        {
            Assert.That(expansionBay.Identity, Is.EqualTo(FrameworkModuleIdentity.None));
            return;
        }

        switch (expansionBay.Identity)
        {
            case FrameworkModuleIdentity.ExpansionBay:
                if (!allowGenericBaseType)
                {
                    Assert.That(expansionBay, Is.InstanceOf<FrameworkGenericExpansionBaySnapshot>());
                }

                break;
            case FrameworkModuleIdentity.ExpansionBayDualInterposer:
                Assert.That(expansionBay, Is.InstanceOf<FrameworkDualInterposerExpansionBaySnapshot>());
                break;
            case FrameworkModuleIdentity.ExpansionBaySingleInterposer:
                Assert.That(expansionBay, Is.InstanceOf<FrameworkSingleInterposerExpansionBaySnapshot>());
                break;
            case FrameworkModuleIdentity.ExpansionBayUmaFans:
                Assert.That(expansionBay, Is.InstanceOf<FrameworkUmaFansExpansionBaySnapshot>());
                break;
            case FrameworkModuleIdentity.ExpansionBaySsdHolder:
                Assert.That(expansionBay, Is.InstanceOf<FrameworkSsdHolderExpansionBaySnapshot>());
                break;
            case FrameworkModuleIdentity.ExpansionBayPcieAccessory:
                Assert.That(expansionBay, Is.InstanceOf<FrameworkPcieAccessoryExpansionBaySnapshot>());
                break;
            case FrameworkModuleIdentity.ExpansionBayAmdGpu:
                Assert.That(expansionBay, Is.InstanceOf<FrameworkAmdGpuExpansionBaySnapshot>());
                break;
            case FrameworkModuleIdentity.ExpansionBayNvidiaGpu:
                Assert.That(expansionBay, Is.InstanceOf<FrameworkNvidiaGpuExpansionBaySnapshot>());
                break;
            case FrameworkModuleIdentity.ExpansionBayFanOnly:
                Assert.That(expansionBay, Is.InstanceOf<FrameworkFanOnlyExpansionBaySnapshot>());
                break;
            default:
                Assert.Fail($"Unexpected expansion-bay identity {expansionBay.Identity}.");
                break;
        }
    }

    [Test]
    [Description("Port 80 ordering is pure decode logic, so it is verified without hardware. Writes is the NEXT slot the EC will write, so the newest entry is the slot before it.")]
    public void Port80History_NewestEntry_IsTheSlotBeforeTheWriteCursor()
    {
        // A wrapped ring: 10 writes into a 4-entry buffer. The cursor sits at 10 % 4 == 2, so the
        // newest code is at index 1 and the walk backwards is 1, 0, 3, 2.
        var wrapped = new FrameworkEcPort80HistorySnapshot(10, 4, [0xAA, 0xBB, 0xCC, 0xDD]);

        Assert.That(wrapped.NewestIndex, Is.EqualTo(1));
        Assert.That(wrapped.CodesNewestFirst, Is.EqualTo(new ushort[] { 0xBB, 0xAA, 0xDD, 0xCC }).AsCollection);

        // A partially filled ring reports only the slots that were actually written.
        var partial = new FrameworkEcPort80HistorySnapshot(2, 4, [0xAA, 0xBB, 0x00, 0x00]);

        Assert.That(partial.NewestIndex, Is.EqualTo(1));
        Assert.That(partial.CodesNewestFirst, Is.EqualTo(new ushort[] { 0xBB, 0xAA }).AsCollection);

        // Nothing recorded yet: the sentinel, not a fabricated ordering.
        var empty = new FrameworkEcPort80HistorySnapshot(0, 4, [0x00, 0x00, 0x00, 0x00]);

        Assert.That(empty.NewestIndex, Is.EqualTo(-1));
        Assert.That(empty.CodesNewestFirst, Is.Empty);
    }

    [Test]
    public void Diagnostics_ShouldReturnExpectedInformationOrReportUnavailable()
    {
        AssertOptionalReadback(
            () => ec.Diagnostics.GetSwitches(),
            switches => Assert.That(switches.ToString(), Is.Not.Null.And.Not.Empty));

        AssertOptionalReadback(
            () => ec.Diagnostics.GetSystemInfo(),
            systemInfo => Assert.That(Enum.IsDefined(systemInfo.CurrentImage)));

        AssertOptionalReadback(
            () => ec.Diagnostics.GetProtocolInfo(),
            protocolInfo =>
            {
                Assert.That(protocolInfo.MaxRequestPacketSize.Bytes, Is.GreaterThan(0));
                Assert.That(protocolInfo.MaxResponsePacketSize.Bytes, Is.GreaterThan(0));
                Assert.That(protocolInfo.SupportedProtocolVersions, Is.Not.Empty);
            });

        AssertOptionalReadback(
            () => ec.Diagnostics.GetApThrottleStatus(),
            throttle => Assert.That(throttle.ToString(), Is.Not.Null.And.Not.Empty));

        AssertOptionalReadback(
            () => ec.Diagnostics.GetPanicInfo(),
            panic => Assert.That(panic.Data, Is.Not.Null));
    }

    [Test]
    [Description("hello echoes a fixed transform of the input, so a matching response proves the EC is answering rather than returning stale bytes.")]
    public void Diagnostics_Hello_ShouldEchoTheRequestOrReportUnavailable()
    {
        AssertOptionalReadback(
            () => ec.Diagnostics.CheckHello(),
            hello => Assert.That(hello.IsExpectedEcho, Is.True, "The EC did not echo the expected hello response."));

        AssertOptionalReadback(
            () => ec.Diagnostics.SendHello(0xA0B0C0D0),
            hello => Assert.That(hello.IsExpectedEcho, Is.True, "The EC did not echo the expected hello response."));
    }

    [Test]
    public void Diagnostics_Port80History_ShouldBeSelfConsistentOrReportUnavailable()
    {
        // The FFI crate reads the history itself rather than calling CrosEc::port80_read, which
        // rejects the longer-than-requested buffers the Windows driver reports. A DeviceError here
        // means that workaround regressed, so it is deliberately not tolerated.
        AssertOptionalReadback(
            () => ec.Diagnostics.GetPort80History(),
            AssertPort80HistoryIsSelfConsistent);
    }

    private static void AssertPort80HistoryIsSelfConsistent(FrameworkEcPort80HistorySnapshot history)
    {
        Assert.That(history.Codes, Is.Not.Null);
        Assert.That(history.CodesNewestFirst.Count, Is.LessThanOrEqualTo(history.Codes.Count));

        if (history.CodesNewestFirst.Count == 0)
        {
            Assert.That(history.NewestIndex, Is.EqualTo(-1));
            return;
        }

        Assert.That(history.NewestIndex, Is.InRange(0, history.Codes.Count - 1));
        Assert.That(history.CodesNewestFirst[0], Is.EqualTo(history.Codes[history.NewestIndex]));
    }

    [Test]
    public void Diagnostics_CommandVersionProbe_ShouldAnswerForAKnownCommand()
    {
        AssertOptionalReadback(
            () => ec.Diagnostics.IsCommandVersionSupported(0x0000, 0),
            _ => Assert.Pass());
    }

    [Test]
    public void Gpio_Enumeration_ShouldMatchReportedCountOrReportUnavailable()
    {
        AssertOptionalReadback(
            () => ec.Gpio.GetAll(),
            lines =>
            {
                Assert.That(lines, Is.Not.Null);
                Assert.That(lines.Count, Is.EqualTo(ec.Gpio.GetCount()));

                foreach (var line in lines)
                {
                    Assert.That(line.Name, Is.Not.Null.And.Not.Empty, "Every enumerated GPIO must carry a firmware name.");
                }
            });
    }

    [Test]
    public void Gpio_GetValue_ShouldRejectNullAndEmptyNames()
    {
        Assert.That(() => ec.Gpio.GetValue(null!), Throws.TypeOf<ArgumentNullException>());
        Assert.That(() => ec.Gpio.GetValue(string.Empty), Throws.InstanceOf<ArgumentException>());
    }

    [Test]
    [Description("A disabled threshold reads back as -273 C from firmware, so it must surface as null rather than as a temperature.")]
    public void ThermalThresholds_DisabledThresholds_ShouldBeNullNeverMinus273()
    {
        FrameworkThermalSnapshot thermal = ec.GetThermalSnapshot();

        for (byte sensorIndex = 0; sensorIndex < thermal.SensorCount; sensorIndex++)
        {
            byte index = sensorIndex;

            AssertOptionalReadback(
                () => ec.Thermal.GetThresholds(index),
                thresholds =>
                {
                    foreach (var threshold in new[] { thresholds.Warn, thresholds.High, thresholds.Halt, thresholds.FanOff, thresholds.FanMax })
                    {
                        if (threshold.HasValue)
                        {
                            Assert.That(
                                threshold.Value.DegreesCelsius,
                                Is.GreaterThan(-273),
                                "A threshold that reads back as -273 C is disabled and must be surfaced as null.");
                        }
                    }
                });
        }
    }

    [Test]
    public void ThermalControl_FanCountAndSensorNames_ShouldAgreeWithTheThermalSnapshot()
    {
        AssertOptionalReadback(
            () => ec.Thermal.GetFanCount(),
            fanCount => Assert.That(fanCount, Is.EqualTo(ec.GetThermalSnapshot().FanCount)));

        AssertOptionalReadback(
            () => ec.Thermal.GetSensorName(0),
            name =>
            {
                Assert.That(name.FirmwareName, Is.Not.Null);
                Assert.That(Enum.IsDefined(name.MappedName));
                Assert.That(Enum.IsDefined(name.SensorType));

                // The second read must come from the cache and agree with the first.
                Assert.That(ec.Thermal.GetSensorName(0), Is.EqualTo(name));
            });
    }

    [Test]
    public void ThermalControl_SensorNameCache_ShouldThrowAfterTheConnectionIsDisposed()
    {
        IFrameworkEcConnection connection = frameworkSystem.OpenDefaultEc();
        IFrameworkEcThermalControl thermal = connection.Thermal;

        try
        {
            _ = thermal.GetSensorName(0);
        }
        catch (FrameworkDataUnavailableStatusException)
        {
            Assert.Ignore("Sensor names are not available on this device.");
        }

        connection.Dispose();

        Assert.That(
            () => thermal.GetSensorName(0),
            Throws.TypeOf<ObjectDisposedException>(),
            "A cached sensor name must not be served after the owning connection is disposed.");
    }

    [Test]
    public void Battery_ReadOnlySurfaces_ShouldReturnExpectedInformationOrReportUnavailable()
    {
        AssertOptionalReadback(
            () => ec.Battery.GetChargingState(),
            charging => Assert.That(charging.ToString(), Is.Not.Null.And.Not.Empty));

        AssertOptionalReadback(
            () => ec.Battery.GetCutoffState(),
            cutoff => Assert.That(Enum.IsDefined(cutoff)));
    }

    [Test]
    [Description("The Smart Battery read costs many I2C round trips, so it is exercised exactly once.")]
    public void Battery_SmartBatterySnapshot_ShouldReturnConsistentUnitsOrReportUnavailable()
    {
        AssertOptionalReadback(
            () => ec.Battery.GetSmartBatterySnapshot(),
            battery =>
            {
                Assert.That(battery.ManufacturerName, Is.Not.Null);
                Assert.That(battery.CellVoltages.Count, Is.EqualTo(4));

                // Exactly one of the two parallel capacity sets is populated, chosen by CAPACITY_MODE.
                if (battery.IsCapacityReportedInEnergyUnits)
                {
                    Assert.That(battery.RemainingCapacity, Is.Null);
                    Assert.That(battery.RemainingEnergy, Is.Not.Null);
                }
                else
                {
                    Assert.That(battery.RemainingCapacity, Is.Not.Null);
                    Assert.That(battery.RemainingEnergy, Is.Null);
                }

                // The sealed groups are null rather than zero-filled.
                if (!battery.IsUnsealed)
                {
                    Assert.That(battery.StateOfHealth, Is.Null);
                    Assert.That(battery.Safety, Is.Null);
                    Assert.That(battery.LifetimeData, Is.Null);
                }
            });
    }

    [Test]
    public void Battery_Authenticate_ShouldRejectKeysThatAreNotSixteenBytes()
    {
        Assert.That(() => ec.Battery.Authenticate(null!), Throws.TypeOf<ArgumentNullException>());
        Assert.That(() => ec.Battery.Authenticate(new byte[15]), Throws.InstanceOf<ArgumentException>());
        Assert.That(() => ec.Battery.Authenticate(new byte[17]), Throws.InstanceOf<ArgumentException>());
    }

    [Test]
    public void PowerDelivery_ControllerVersions_ShouldOnlyReportPresentSlots()
    {
        AssertOptionalReadback(
            () => ec.PowerDelivery.GetControllerVersions(),
            versions =>
            {
                foreach (var controller in versions.PresentControllers)
                {
                    Assert.That(controller.IsPresent, Is.True, "PresentControllers must not yield an absent slot.");
                    Assert.That(Enum.IsDefined(controller.Slot));
                }
            });
    }

    [Test]
    public void PowerDelivery_GetPowerInfo_ShouldRejectPortsOutsideTheByteRange()
    {
        Assert.That(() => ec.PowerDelivery.GetPowerInfo(-1), Throws.TypeOf<ArgumentOutOfRangeException>());
        Assert.That(() => ec.PowerDelivery.GetPowerInfo(256), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    [Description("The retimer sits behind the Framework 16 expansion bay; other families reject the underlying EC command.")]
    public void PowerDelivery_RetimerVersion_ShouldReadOnFramework16OrThrowElsewhere()
    {
        if (frameworkSystem.GetPlatformFamily() != FrameworkPlatformFamily.Framework16)
        {
            Assert.That(() => ec.PowerDelivery.GetRetimerVersion(), Throws.InstanceOf<FrameworkStatusException>());
            return;
        }

        AssertOptionalReadback(
            () => ec.PowerDelivery.GetRetimerVersion(),
            retimer =>
            {
                Assert.That(retimer.Version, Is.Not.Null);

                // The version is four raw register bytes, never text.
                if (retimer.IsPresent && retimer.Version.Count >= 4)
                {
                    Assert.That(retimer.VersionString, Does.Match("^[0-9A-F]+(\\.[0-9A-F]+){3}$"));
                }
            });
    }

    [Test]
    public void PowerManagement_ReadOnlySurfaces_ShouldReturnExpectedInformationOrReportUnavailable()
    {
        AssertOptionalReadback(
            () => ec.PowerManagement.GetHibernateDelay(),
            delay => Assert.That(delay.Seconds, Is.GreaterThanOrEqualTo(0)));

        AssertOptionalReadback(
            () => ec.PowerManagement.GetStandaloneMode(),
            standalone => Assert.That(standalone.ToString(), Is.Not.Null.And.Not.Empty));
    }

    [Test]
    public void Input_WriteGuards_ShouldRejectImpossibleArgumentsBeforeTouchingHardware()
    {
        // 64 keys is the per-call maximum the native ABI accepts.
        Assert.That(
            () => ec.Input.SetRgbKeyboardColors(0, new FrameworkKeyboardColor[65]),
            Throws.TypeOf<ArgumentOutOfRangeException>());

        Assert.That(
            () => ec.Input.SetRgbKeyboardColors(0, []),
            Throws.TypeOf<ArgumentOutOfRangeException>());

        Assert.That(
            () => ec.Input.SetRgbKeyboardColors(0, null!),
            Throws.TypeOf<ArgumentNullException>());

        Assert.That(
            () => ec.Input.SetRgbKeyboardColors(-1, [default]),
            Throws.TypeOf<ArgumentOutOfRangeException>());

        Assert.That(
            () => ec.Input.SetFingerprintLedBrightness(Ratio.FromPercent(-1)),
            Throws.TypeOf<ArgumentOutOfRangeException>());

        Assert.That(
            () => ec.Input.SetFingerprintLedBrightness(Ratio.FromPercent(101)),
            Throws.TypeOf<ArgumentOutOfRangeException>());

        Assert.That(
            () => ec.Input.RemapKey(-1, 0, 0x0014),
            Throws.TypeOf<ArgumentOutOfRangeException>());

        Assert.That(
            () => ec.Input.RemapKey(0, 256, 0x0014),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void Peripherals_ReadOnlySurfaces_ShouldReturnExpectedInformationOrReportUnavailable()
    {
        IFrameworkPeripherals peripherals = new FrameworkPeripherals();

        AssertOptionalReadback(
            () => peripherals.GetStylusBattery(),
            stylus => Assert.That(stylus.ChargeLevel.Percent, Is.InRange(0, 100)));

        AssertOptionalReadback(
            () => peripherals.GetCameraVersions(),
            cameras => Assert.That(cameras.Peripherals, Is.Not.Null));

        AssertOptionalReadback(
            () => peripherals.GetUsbHubVersions(),
            hubs => Assert.That(hubs.Peripherals, Is.Not.Null));
    }

    [Test]
    public void Peripherals_WriteGuards_ShouldRejectImpossibleArgumentsBeforeTouchingHardware()
    {
        IFrameworkPeripherals peripherals = new FrameworkPeripherals();

        Assert.That(
            () => peripherals.SetTouchpadHapticIntensity(Ratio.FromPercent(-1)),
            Throws.TypeOf<ArgumentOutOfRangeException>());

        Assert.That(
            () => peripherals.SetTouchpadHapticIntensity(Ratio.FromPercent(101)),
            Throws.TypeOf<ArgumentOutOfRangeException>());

        Assert.That(
            () => peripherals.GetNvmeVersion(null!),
            Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    [Platform("Win", Reason = "The NVMe passthrough is gated to Linux upstream, so other platforms must report NotSupported.")]
    [Description("Verifies the new FrameworkStatusCode.NotSupported maps to a managed exception instead of falling through to ArgumentOutOfRangeException.")]
    public void Peripherals_NvmeVersion_ShouldReportNotSupportedOnNonLinuxPlatforms()
    {
        IFrameworkPeripherals peripherals = new FrameworkPeripherals();

        Assert.That(
            () => peripherals.GetNvmeVersion("/dev/nvme0"),
            Throws.TypeOf<FrameworkNotSupportedStatusException>(),
            "NotSupported must map to FrameworkNotSupportedStatusException, not to an unhandled status code.");
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
        catch (FrameworkNotSupportedStatusException)
        {
        }
    }
}
