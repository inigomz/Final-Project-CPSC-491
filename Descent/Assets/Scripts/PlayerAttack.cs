using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage = 1;
    public float attackRange = 5f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
        {
            EnemyHealth[] enemies = FindObjectsOfType<EnemyHealth>();

            foreach (EnemyHealth enemy in enemies)
            {
                float distance = Vector2.Distance(transform.position, enemy.transform.position);

                if (distance <= attackRange)
                {
                    enemy.TakeDamage(damage);
                }
            }
        }
    }
}