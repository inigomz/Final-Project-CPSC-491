using NUnit.Framework;
using UnityEngine;
using System;
using System.Reflection;

public class PlayerHealthTests
{
    private GameObject player;
    private Component playerHealth;
    private Type playerHealthType;

    [SetUp]
    public void Setup()
    {
        // Find PlayerHealth class from main assembly
        playerHealthType = Type.GetType("PlayerHealth, Assembly-CSharp");
        Assert.IsNotNull(playerHealthType, "PlayerHealth class could not be found.");

        // Create test GameObject
        player = new GameObject("Test Player");
        playerHealth = player.AddComponent(playerHealthType);

        // Set initial values
        playerHealthType.GetField("maxHealth").SetValue(playerHealth, 10);
        playerHealthType.GetField("currentHealth").SetValue(playerHealth, 10);
    }

    [TearDown]
    public void Teardown()
    {
        UnityEngine.Object.DestroyImmediate(player);
    }

    [Test]
    public void TakeDamage_ReducesHealth()
    {
        playerHealthType.GetMethod("TakeDamage").Invoke(playerHealth, new object[] { 3 });

        int currentHealth = (int)playerHealthType.GetField("currentHealth").GetValue(playerHealth);
        Assert.AreEqual(7, currentHealth);
    }

    [Test]
    public void TakeDamage_DoesNotGoBelowZero()
    {
        playerHealthType.GetMethod("TakeDamage").Invoke(playerHealth, new object[] { 50 });

        int currentHealth = (int)playerHealthType.GetField("currentHealth").GetValue(playerHealth);
        Assert.AreEqual(0, currentHealth);
    }

    [Test]
    public void Heal_DoesNotExceedMaxHealth()
    {
        playerHealthType.GetField("currentHealth").SetValue(playerHealth, 8);

        playerHealthType.GetMethod("Heal").Invoke(playerHealth, new object[] { 10 });

        int currentHealth = (int)playerHealthType.GetField("currentHealth").GetValue(playerHealth);
        Assert.AreEqual(10, currentHealth);
    }
}