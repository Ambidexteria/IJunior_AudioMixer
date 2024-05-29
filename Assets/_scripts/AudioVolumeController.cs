using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioVolumeController : MonoBehaviour
{
    private const string MasterVolume = nameof(MasterVolume);
    private const string BackgroundMusicVolume = nameof(BackgroundMusicVolume);
    private const string SoundsVolume = nameof(SoundsVolume);

    private const int MinVolume = -80;
    private const int VolumeConvertCoefficient = 20;

    [SerializeField] private AudioMixerGroup _masterAudioMixer;

    private float _currentMasterVolume;
    private bool _IsMuted = false;

    public void ToggleMute(bool value)
    {
        _IsMuted = value;

        if (value)
        {
            _masterAudioMixer.audioMixer.GetFloat(MasterVolume, out _currentMasterVolume);
            _masterAudioMixer.audioMixer.SetFloat(MasterVolume, MinVolume);
        }
        else
        {
            _masterAudioMixer.audioMixer.SetFloat(MasterVolume, _currentMasterVolume);
        }
    }

    public void SetMasterVolume(float volume)
    {
        if (volume < MinVolume)
            throw new ArgumentOutOfRangeException();

        _currentMasterVolume = Mathf.Log10(volume) * VolumeConvertCoefficient;

        if (_IsMuted == false)
            _masterAudioMixer.audioMixer.SetFloat(MasterVolume, _currentMasterVolume);
    }

    public void SetBackgroundMusicVolume(float volume)
    {
        if (volume < MinVolume)
            throw new ArgumentOutOfRangeException();

        ChangeVolume(BackgroundMusicVolume, volume);
    }

    public void SetSoundsVolume(float volume)
    {
        if (volume < MinVolume)
            throw new ArgumentOutOfRangeException();

        ChangeVolume(SoundsVolume, volume);
    }

    private void ChangeVolume(string volumeGroup, float value)
    {
        _masterAudioMixer.audioMixer.SetFloat(volumeGroup, Mathf.Log10(value) * VolumeConvertCoefficient);
    }
}