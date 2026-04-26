using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [Header("Weapon Stats")]
    public string weaponName = "Sword";
    public int damage = 3;
    public float attackRange = 2.0f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        PlayerAttack playerAttack = other.GetComponent<PlayerAttack>();

        if (playerAttack != null)
        {
            playerAttack.EquipWeapon(weaponName, damage, attackRange);
            Debug.Log("Picked up weapon: " + weaponName);
            Destroy(gameObject);
        }
    }
}