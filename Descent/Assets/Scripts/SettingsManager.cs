using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;
    public SettingsData currentSettings;
    string savePath;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            savePath = Application.persistentDataPath + "/settings.json";
            LoadSettings();

        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Debug.Log("Settings Manager Initialized");
    }

    public void SaveSettings()
    {
        string json = JsonUtility.ToJson(currentSettings, true);
        System.IO.File.WriteAllText(savePath, json);
        Debug.Log("Settings saved to: " + savePath);
    }

    public void LoadSettings()
    {
        if (System.IO.File.Exists(savePath))
        {
            string json = System.IO.File.ReadAllText(savePath);
            currentSettings = JsonUtility.FromJson<SettingsData>(json);
            Debug.Log("Settings loaded.");
        }
        else
        {
            currentSettings = new SettingsData();
            SaveSettings();
            Debug.Log("Created default settings.");
        }
    }
}

