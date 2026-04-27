using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;

    public Slider healthBar;
    public GameObject GameOverUI;

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

        Debug.Log("Current Health: " + currentHealth);

        if (currentHealth == 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
    }

        void UpdateHealthBar()
    {
        healthBar.value = (float)currentHealth / maxHealth;
    }


    void Die()
    {
        Debug.Log("Player Died");

        if (GameOverUI != null)
        {
            GameOverUI.SetActive(true);
        }
        else
        {
            Debug.LogError("GameOverUI is NOT assigned in Inspector!");
        }

        Time.timeScale = 0f;
    }
}