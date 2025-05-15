using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 50f;
    public ParticleSystem deathParticle;
    public SkinnedMeshRenderer SkinnedMeshRenderer;

    public AudioSource hurtSfx;
    public float soundCooldown = 1f;
    private float lastPlayTime;


    public void TakeDamage(float amount)
    {
        PlayHitSound();
        health -= amount;
        if(health <= 0)
        {
            Die();
        }
    }


        public void PlayHitSound()
        {
            if (Time.time - lastPlayTime > soundCooldown)
            {
                hurtSfx.Play();
                lastPlayTime = Time.time;
            }
        }

    void Die()
    {
        GetComponent<EnemyAI>().enabled = false;
        deathParticle.Play();
        Destroy(gameObject, 0.3f);
    }
}
