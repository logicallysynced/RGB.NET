using RGB.NET.Core;

namespace RGB.NET.Devices.PlayStation;

/// <inheritdoc />
/// <summary>
/// Represents a Sony DualShock 4 controller.
/// </summary>
public sealed class DualShock4RGBDevice : AbstractRGBDevice<PlayStationDeviceInfo>
{
    #region Properties & Fields

    private readonly DualShock4UpdateQueue _updateQueue;

    #endregion

    #region Constructors

    internal DualShock4RGBDevice(PlayStationDeviceInfo deviceInfo, DualShock4UpdateQueue updateQueue)
        : base(deviceInfo, updateQueue)
    {
        _updateQueue = updateQueue;
        InitializeLayout();
    }

    #endregion

    #region Methods

    // DS4 has a single RGB lightbar above the touchpad. Custom1 keeps the LED
    // enum stable across DS4 / DS5 — DualSenseRGBDevice's Custom1 is also the
    // lightbar so a host mapping for "Custom 1" carries sensible meaning across
    // both controller types.
    private void InitializeLayout()
    {
        Led? lightbar = AddLed(LedId.Custom1, new Point(0, 0), new Size(60, 14));
        if (lightbar != null)
            lightbar.Shape = Shape.Rectangle;
    }

    internal void SuspendWrites() => _updateQueue.SuspendWrites();
    internal void Shutdown(bool sendOffFrame = true) => _updateQueue.Shutdown(sendOffFrame);

    #endregion
}
