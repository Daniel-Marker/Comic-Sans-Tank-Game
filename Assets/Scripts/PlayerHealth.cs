using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] Text healthText;
    float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthText();
    }

    public void DecreaseHealth(float amount) {
        currentHealth -= amount;

        if (currentHealth < 0)
        {
            HandlePlayerDeath();
        }

        UpdateHealthText();
    }

    public void IncreaseHealth(float amount) {
        currentHealth += amount;

        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }

        UpdateHealthText();
    }

    private void HandlePlayerDeath()
    {
        print("Dead");
    }

    private void UpdateHealthText() {
        healthText.text = "HP: " + currentHealth + "/" + maxHealth;
    }
}
