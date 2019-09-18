using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] TextMesh healthText;
    float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthText();
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;

        if (currentHealth < 0)
        {
            HandleEnemyDeath();
        }

        UpdateHealthText();
    }

    public void IncreaseHealth(float amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateHealthText();
    }

    private void HandleEnemyDeath()
    {
        print(gameObject.name + " has died");
    }

    void UpdateHealthText() {
        healthText.text = "HP: " + currentHealth + "/" + maxHealth;
    }
}
