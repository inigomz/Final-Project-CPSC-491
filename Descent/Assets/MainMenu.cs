using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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
}
