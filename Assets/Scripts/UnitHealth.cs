using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth
{
    // Fields
    float currentHealth;
    float currentMaxHealth;

    // Properties
    public float Health
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }
    public float MaxHealth
    {
        get { return currentMaxHealth; }
        set { currentMaxHealth = value; }
    }

    // Constructor
    public UnitHealth(float health, float maxHealth)
    {
        currentHealth = health;
        currentMaxHealth = maxHealth;
    }

    // Methods
    public void SetHealth(float newHealth) // Method to change the current health
    {
        // Clamp the health value [0, maxHealth]
        currentHealth = Mathf.Clamp(newHealth, 0f, currentMaxHealth);
    }
    public void DamageUnit(float damageAmount)
    {
        if (currentHealth > 0) // Apply damage
        {
            currentHealth -= damageAmount;
        }
    }
    public void HealUnit(float healAmount)
    {
        if (currentHealth < currentMaxHealth) // Apply heal
        {
            currentHealth += healAmount;
        }
        if (currentHealth > currentMaxHealth) // Remove overheal
        {
            currentHealth = currentMaxHealth;
        }
    }
}
