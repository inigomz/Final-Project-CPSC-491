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

    private Resolution[] resolutions;

    void Start()
    {
        resolutions = new Resolution[]
        {
            new Resolution { width = 1280, height = 720 },   // good smaller option
            new Resolution { width = 1600, height = 900 },   // medium
            new Resolution { width = 1920, height = 1080 }   // full HD
        };

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        foreach (Resolution res in resolutions)
        {
            options.Add(res.width + " x " + res.height);
        }

        resolutionDropdown.AddOptions(options);

        // Set default index to 1920x1080 if available
        int defaultIndex = 2; // 1920x1080
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

        // Initialize volume slider
        if (volumeSlider != null)
        {
            SetVolume();
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
    }

    public void SetResolution(int index)
    {
        if (index < 0 || index >= resolutions.Length) return;
        Resolution res = resolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreenMode);
        Debug.Log("Resolution set to: " + res.width + "x" + res.height);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreenMode = isFullscreen
            ? FullScreenMode.FullScreenWindow   // BORDERLESS
            : FullScreenMode.Windowed;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("TitleMenuScene");
    }
}