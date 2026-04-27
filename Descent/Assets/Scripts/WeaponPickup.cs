using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [Header("Weapon Stats")]
    public string weaponName = "Sword";
    public int damageBoost = 2;
    public float rangeBoost = 0.5f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        PlayerAttack playerAttack = other.GetComponent<PlayerAttack>();

        if (playerAttack != null)
        {
            playerAttack.IncreaseDamage(damageBoost);
            playerAttack.IncreaseRange(rangeBoost);

            Debug.Log("Picked up weapon power-up: " + weaponName);
            Destroy(gameObject);
        }
    }
}