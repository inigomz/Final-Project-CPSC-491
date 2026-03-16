using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    public int currentXP = 0;

    public void AddXP(int amount)
    {
        currentXP += amount;
        Debug.Log("Player gained XP. Current XP: " + currentXP);
    }
}