using UnityEngine;

public class PowerUpPickup : MonoBehaviour
{
    public enum PowerUpType
    {
        DamageBoost,
        RangeBoost,
        Heal
    }

    [Header("Power-Up Settings")]
    public PowerUpType powerUpType;
    public int damageBoostAmount = 1;
    public float rangeBoostAmount = 1f;
    public int healAmount = 3;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        PlayerAttack playerAttack = other.GetComponent<PlayerAttack>();
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        switch (powerUpType)
        {
            case PowerUpType.DamageBoost:
                if (playerAttack != null)
                {
                    playerAttack.IncreaseDamage(damageBoostAmount);
                    Debug.Log("Picked up Damage Boost!");
                }
                break;

            case PowerUpType.RangeBoost:
                if (playerAttack != null)
                {
                    playerAttack.IncreaseRange(rangeBoostAmount);
                    Debug.Log("Picked up Range Boost!");
                }
                break;

            case PowerUpType.Heal:
                if (playerHealth != null)
                {
                    playerHealth.Heal(healAmount);
                    Debug.Log("Picked up Health Power-Up!");
                }
                break;
        }

        Destroy(gameObject);
    }
}