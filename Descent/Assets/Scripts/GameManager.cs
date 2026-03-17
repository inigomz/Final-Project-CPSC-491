using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int playerHealth;
    public int playerXP;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetGame()
    {
        Debug.Log("Game Reset");

        playerHealth = 100;
        playerXP = 0;
    }

    // Future systems
    // inventory.Clear();
    // level = 1;
}
