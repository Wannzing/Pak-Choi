using UnityEngine;

public class Trap : MonoBehaviour
{
    public GameObject player;
    public int trapDamage;
    float damageCooldown = 1f;
    float nextDamageCooldown;

    void Start()
    {
       
    }

    void OnTriggerEnter(Collider other)
    {
        {
        if(other.gameObject.CompareTag("Player"))
        {
            float currentTime = Time.time;
            if(currentTime > nextDamageCooldown)
            {
                player.GetComponent<PlayerHealth>().currentHealth -= trapDamage;

                nextDamageCooldown = currentTime + damageCooldown;
            }
        }
        }
    }

}
