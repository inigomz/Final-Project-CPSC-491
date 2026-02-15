using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    public AudioSource uiAudioSource;

    private Resolution[] resolutions;

    void Start()
    {
        // Populate resolutions without duplicates
        List<Resolution> uniqueRes = new List<Resolution>();
        foreach (Resolution res in Screen.resolutions)
        {
            if (!uniqueRes.Exists(r => r.width == res.width && r.height == res.height))
                uniqueRes.Add(res);
        }
        resolutions = uniqueRes.ToArray();

        resolutionDropdown.ClearOptions();
        foreach (Resolution res in resolutions)
        {
            resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(res.width + " x " + res.height));
        }

        // Set default index to 1920x1080 if available
        int defaultIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == 1920 && resolutions[i].height == 1080)
            {
                defaultIndex = i;
                break;
            }
        }

        // Apply the default resolution immediately
        resolutionDropdown.value = defaultIndex;
        resolutionDropdown.RefreshShownValue();
        SetResolution(defaultIndex);

        // Fullscreen toggle
        fullscreenToggle.isOn = Screen.fullScreen;
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);

        // Initialize volume slider
        if (volumeSlider != null)
        {
            // Load saved value, default to 1
            float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
            volumeSlider.value = savedVolume;
            SetVolume(); // Apply immediately
        }

        // Add listeners to UI elements
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
        volumeSlider.onValueChanged.AddListener(delegate { SetVolume(); });
    }

    public void SetVolume()
    {
        if (volumeSlider == null || audioMixer == null) return;

        float volume = Mathf.Clamp(volumeSlider.value, 0.0001f, 1f);
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);

        // Save volume to PlayerPrefs so other scenes can access it
        PlayerPrefs.SetFloat("MasterVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetResolution(int index)
    {
        if (index < 0 || index >= resolutions.Length) return;
        Resolution res = resolutions[index];

        // Apply resolution and fullscreen properly
        if (Screen.fullScreen)
        {
            // Fullscreen: use desktop resolution
            Screen.SetResolution(res.width, res.height, true);
        }
        else
        {
            // Windowed: subtract OS border if needed
            Screen.SetResolution(res.width, res.height, false);
        }

        Debug.Log("Resolution set to: " + res.width + "x" + res.height + " Fullscreen: " + Screen.fullScreen);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        // Apply fullscreen
        Screen.fullScreenMode = isFullscreen 
            ? FullScreenMode.FullScreenWindow 
            : FullScreenMode.Windowed;

        // Play the switch sound
        if (uiAudioSource != null)
        {
            uiAudioSource.Play(); // Plays the AudioSource.clip assigned in Inspector
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("TitleMenuScene");
    }
}