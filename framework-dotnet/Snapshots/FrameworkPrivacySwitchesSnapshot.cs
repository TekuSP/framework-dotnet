namespace FrameworkDotnet.Snapshots;

/// <summary>
/// Represents the hardware privacy switch states reported by the EC.
/// </summary>
/// <remarks>
/// A value of <see langword="true"/> means the device is enabled (the privacy switch is in the off/unlocked position).
/// A value of <see langword="false"/> means the device is disabled (the privacy switch is engaged).
/// </remarks>
public sealed record FrameworkPrivacySwitchesSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkPrivacySwitchesSnapshot"/> class.
    /// </summary>
    /// <param name="microphoneEnabled">A value indicating whether the microphone is enabled.</param>
    /// <param name="cameraEnabled">A value indicating whether the camera is enabled.</param>
    public FrameworkPrivacySwitchesSnapshot(bool microphoneEnabled, bool cameraEnabled)
    {
        MicrophoneEnabled = microphoneEnabled;
        CameraEnabled = cameraEnabled;
    }

    /// <summary>
    /// Gets a value indicating whether the microphone is enabled.
    /// </summary>
    /// <remarks>
    /// <see langword="true"/> means the privacy switch is in the off position (microphone active).
    /// <see langword="false"/> means the privacy switch is engaged (microphone muted at hardware level).
    /// </remarks>
    public bool MicrophoneEnabled { get; init; }

    /// <summary>
    /// Gets a value indicating whether the camera is enabled.
    /// </summary>
    /// <remarks>
    /// <see langword="true"/> means the privacy switch is in the off position (camera active).
    /// <see langword="false"/> means the privacy switch is engaged (camera disabled at hardware level).
    /// </remarks>
    public bool CameraEnabled { get; init; }

    public override string ToString()
    {
        return $"Privacy Switches: Microphone Enabled: {MicrophoneEnabled}, Camera Enabled: {CameraEnabled}";
    }
}
