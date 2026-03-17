using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform spawnPoint;

    void Start()
    {
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        if (playerPrefab == null || spawnPoint == null)
        {
            Debug.LogError("Missing player prefab or spawn point!");
            return;
        }

        Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
    }
}