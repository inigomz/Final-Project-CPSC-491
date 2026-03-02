using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioSource musicSource;

    void Start()
    {
        // Load saved volume (default 1 if not set)
        float volume = PlayerPrefs.GetFloat("MasterVolume", 1f);

        // Apply to AudioMixer
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);

        // Optional: start playing music
        if (musicSource != null)
        {
            musicSource.Play();
        }
    }

    public void StartGame()
    {
        Debug.Log("START CLICKED");
        SceneManager.LoadScene("TextScene");
    }

    public void OpenOptions()
    {
        Debug.Log("OPTIONS CLICKED");
        SceneManager.LoadScene("OptionsMenu");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT CLICKED");

        // Only quit in a build
        if (Application.isEditor)
        {
            Debug.Log("Application.Quit() ignored in Editor.");
            return;
        }

        // Quit application
        Application.Quit();
    }
}