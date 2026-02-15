using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [Header("Scene names (must match exactly)")]
    public string gameplaySceneName = "SampleScene";   // change later if your real level is different
    public string optionsSceneName  = "OptionsScene";  // change to your real options scene name

    public void NewStart()
    {
        // Later you can also clear save data here.
        SceneManager.LoadScene(gameplaySceneName);
    }

    public void Options()
    {
        SceneManager.LoadScene(optionsSceneName);
    }

    public void Resume()
    {
        // If you haven’t built saving/loading yet, “Resume” should behave safely:
        // If a game was paused, unpause; otherwise start the game.
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            return;
        }

        // No paused game running? Just start gameplay for now.
        SceneManager.LoadScene(gameplaySceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
