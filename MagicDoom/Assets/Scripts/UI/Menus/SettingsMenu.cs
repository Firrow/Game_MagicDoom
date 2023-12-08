using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] AudioClip soundClick;

    public Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;
    public AudioMixer musicAudioMixer;
    public AudioMixer soundAudioMixer;
    public Slider musicSlider;
    public Slider soundSlider;

    private AudioSource audioSource;



    private void Start()
    {
        AllResolutionSettings();
        audioSource = this.GetComponent<AudioSource>();

        musicAudioMixer.GetFloat("musicVolume", out float musicValueForSlider);
        musicSlider.value = musicValueForSlider;

        soundAudioMixer.GetFloat("soundVolume", out float soundValueForSlider);
        soundSlider.value = soundValueForSlider;
    }

    public void SetVolumeMusic(float volume)
    {
        musicAudioMixer.SetFloat("musicVolume", volume);
    }

    public void SetVolumeSound(float volume)
    {
        soundAudioMixer.SetFloat("soundVolume", volume);
        soundSlider.GetComponent<AudioSource>().PlayOneShot(soundClick);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void ReturnMenu()
    {
        audioSource.PlayOneShot(soundClick);
        StartCoroutine(DelaySceneLoad("Menu"));
    }

    public void ReturnGame()
    {
        GameObject pauseUI = GameObject.FindGameObjectWithTag("PauseMenu");
        StartCoroutine(DelayPauseLoad(pauseUI));

        gameObject.SetActive(false);
        if (pauseUI != null)
            pauseUI.SetActive(true);
    }

    private void AllResolutionSettings()
    {
        //Get all resolutions in one occurence only + get the good one in starting game
        resolutions = Screen.resolutions.Select(Resolution => new Resolution { width = Resolution.width, height = Resolution.height }).Distinct().ToArray();

        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;

            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    IEnumerator DelayPauseLoad(GameObject pauseUI)
    {
        audioSource.PlayOneShot(soundClick);
        yield return new WaitWhile(() => audioSource.isPlaying);
    }

    IEnumerator DelaySceneLoad(string sceneName)
    {
        yield return new WaitForSeconds(soundClick.length - 0.2f);
        SceneManager.LoadScene(sceneName);
    }
}
