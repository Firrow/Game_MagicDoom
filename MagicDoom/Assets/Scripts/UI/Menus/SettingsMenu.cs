using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer musicAudioMixer;
    public AudioMixer soundAudioMixer;
    public void SetVolumeMusic(float volume)
    {
        musicAudioMixer.SetFloat("musicVolume", volume);
    }

    public void SetVolumeSound(float volume)
    {
        soundAudioMixer.SetFloat("soundVolume", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
