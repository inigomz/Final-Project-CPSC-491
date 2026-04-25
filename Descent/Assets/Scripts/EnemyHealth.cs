using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;

    public Slider healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthBar();

        if (currentHealth == 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        healthBar.value = (float)currentHealth / maxHealth;
    }

    void Die()
    {
        Debug.Log(gameObject.name + " died");

        PlayerXP xp = FindFirstObjectOfType<PlayerXP>();
        if (xp != null)
        {
            xp.AddXP(5);
        }

        Destroy(gameObject);
    }

    private T FindFirstObjectOfType<T>()
    {
        throw new NotImplementedException();
    }

}