using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents the firmware versions of every USB Power Delivery controller slot reported by the embedded controller.
/// </summary>
/// <remarks>
/// The three controller slots are fixed and are always reported: <c>Controller_0</c> is the right-hand controller, <c>Controller_1</c> the left-hand controller and
/// <c>Controller_2</c> the rear controller of a Framework Desktop. Framework laptops populate slots 0 and 1, Framework Desktop populates slot 2 only, so the
/// populated slots are not contiguous. Enumerate <see cref="PresentControllers"/> rather than indexing blindly.
/// </remarks>
public sealed record FrameworkPowerDeliveryControllerVersionsSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkPowerDeliveryControllerVersionsSnapshot"/> class.
    /// </summary>
    /// <param name="controllerCount">The number of populated controller slots reported by the native layer.</param>
    /// <param name="controller_0">The right-hand controller slot.</param>
    /// <param name="controller_1">The left-hand controller slot.</param>
    /// <param name="controller_2">The rear controller slot.</param>
    public FrameworkPowerDeliveryControllerVersionsSnapshot(byte controllerCount, FrameworkPowerDeliveryControllerFirmwareSnapshot controller_0, FrameworkPowerDeliveryControllerFirmwareSnapshot controller_1, FrameworkPowerDeliveryControllerFirmwareSnapshot controller_2)
    {
        ControllerCount = controllerCount;
        Controller_0 = controller_0;
        Controller_1 = controller_1;
        Controller_2 = controller_2;
    }

    /// <summary>
    /// Gets the number of populated controller slots reported by the native layer.
    /// </summary>
    /// <remarks>
    /// This is a count of populated slots, not a contiguous slot range: a Framework Desktop reports a single populated controller that occupies slot index 2.
    /// Use it to size a display, but use <see cref="FrameworkPowerDeliveryControllerFirmwareSnapshot.IsPresent"/> to decide whether a given slot may be read.
    /// </remarks>
    public byte ControllerCount { get; init; }

    /// <summary>
    /// Gets the right-hand controller slot.
    /// </summary>
    public FrameworkPowerDeliveryControllerFirmwareSnapshot Controller_0 { get; init; }

    /// <summary>
    /// Gets the left-hand controller slot.
    /// </summary>
    public FrameworkPowerDeliveryControllerFirmwareSnapshot Controller_1 { get; init; }

    /// <summary>
    /// Gets the rear controller slot.
    /// </summary>
    public FrameworkPowerDeliveryControllerFirmwareSnapshot Controller_2 { get; init; }

    /// <summary>
    /// Gets all controller slots in index order, populated or not.
    /// </summary>
    public IReadOnlyList<FrameworkPowerDeliveryControllerFirmwareSnapshot> Controllers => [Controller_0, Controller_1, Controller_2];

    /// <summary>
    /// Gets the populated controller slots in index order.
    /// </summary>
    /// <seealso cref="ControllerCount"/>
    public IEnumerable<FrameworkPowerDeliveryControllerFirmwareSnapshot> PresentControllers => Controllers.Where(controller => controller.IsPresent);

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"Power Delivery Controller Versions: Controller Count: {ControllerCount.ToString(CultureInfo.InvariantCulture)}, Controllers: {string.Join(", ", PresentControllers)}";
    }
}
