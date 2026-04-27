using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage = 1;
    public float attackRange = 5f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
        {
            EnemyHealth[] enemies = FindObjectsByType<EnemyHealth>(FindObjectsSortMode.None);

            foreach (EnemyHealth enemy in enemies)
            {
                float distance = Vector2.Distance(transform.position, enemy.transform.position);

                if (distance <= attackRange)
                {
                    Debug.Log("Hit enemy: " + enemy.name);
                    enemy.TakeDamage(damage);
                }
            }
        }
    }
}