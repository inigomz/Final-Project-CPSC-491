using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Equipped Weapon")]
    public string weaponName = "Fists";
    public int damage = 1;
    public float attackRange = 1.5f;

    [Header("Attack Settings")]
    public KeyCode primaryAttackKey = KeyCode.E;
    public KeyCode secondaryAttackKey = KeyCode.Space;

    void Update()
    {
        if (Input.GetKeyDown(primaryAttackKey) || Input.GetKeyDown(secondaryAttackKey))
        {
            Attack();
        }
    }

    void Attack()
    {
        EnemyHealth[] enemies = Object.FindObjectsByType<EnemyHealth>(
            FindObjectsInactive.Exclude,
            FindObjectsSortMode.None
        );

        foreach (EnemyHealth enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            if (distance <= attackRange)
            {
                Debug.Log("Hit enemy with " + weaponName + ": " + enemy.name);
                enemy.TakeDamage(damage);
            }
        }
    }

    public void EquipWeapon(string newWeaponName, int newDamage, float newAttackRange)
    {
        weaponName = newWeaponName;
        damage = newDamage;
        attackRange = newAttackRange;

        Debug.Log("Equipped weapon: " + weaponName + " | Damage: " + damage + " | Range: " + attackRange);
    }
}