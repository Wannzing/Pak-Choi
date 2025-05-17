using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public GameObject Player;
    public GameObject loseScreen;
    public int maxHealth = 100;
    public int currentHealth;
    public AudioSource hurtSfx;

    public TextMeshProUGUI healthText;
    public Image hurtFlashImage; // UI image for the red flash

    public float flashDuration = 0.2f;

    void Start()
    {
        currentHealth = maxHealth;
        hurtFlashImage.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("HURT");
            TakeDamage(1);
        }

        Death();
        UpdateHealthUI();
    }

    void Death()
    {
        if (currentHealth <= 0)
        {
            Debug.Log("Player Died");
            Time.timeScale = 0f;
            loseScreen.SetActive(true);
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
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        ShowHurtEffect();
        hurtSfx.Play();
    }

    private void ShowHurtEffect()
    {
        if (hurtFlashImage != null)
        {
            StopAllCoroutines();
            StartCoroutine(FlashHurt());
        }
    }

    private IEnumerator FlashHurt()
    {
        hurtFlashImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(flashDuration);

        hurtFlashImage.gameObject.SetActive(false);
    }
}
