using System;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{


    public GameObject Player;
    public int maxHealth = 100;
    public  int currentHealth;

    public TextMeshProUGUI healthText;
    //[SerializeField] ParticleSystem deathParticle;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("HURT");
            //deathParticle.Play();
            currentHealth--;
        }
        Death();
        UpdateHealthUI();
    }


    void Death()
    {


        if (currentHealth <= 0)
        {
            //deathParticle.Play();
            Debug.Log("Player Died");
            Destroy(gameObject, 0.5f);
        }
        else
        {
            return;
        }
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = $"{currentHealth}/{maxHealth}";
        }
        else
        {
            Debug.LogError("Health TextMeshPro object is not assigned!");
        }
    }

    public void TakeDamage(int damage)
    {
        // Reduce the player's health by the damage amount
        currentHealth -= damage;

        // Clamp health to prevent it from going below zero
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }
}
