using UnityEngine;
using TMPro;
using System.Collections;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 1f;
    public Transform gun;

    //public GameObject impactEffect;
    public GameObject hitImpactEffect;
    public ParticleSystem muzzleFlash;

    public Camera fpsCam;

    // Ammo and reloading variables
    public int maxAmmo = 30; // Maximum ammo in the magazine
    private int currentAmmo; // Current ammo in the magazine
    public float reloadTime = 2f; // Time it takes to reload
    public bool isReloading = false; // Check if the gun is currently reloading

    // Fire rate variables
    public float fireRate = 0.2f; // Delay between shots (in seconds)
    public bool canShoot = true; // Flag to check if the gun can shoot

    // UI Text for displaying ammo (optional)
    public TextMeshProUGUI ammoText;
    private Animator anim;

    //AUDIO

    public AudioSource shootSfx;
    public AudioSource reloadSfx;
    public AudioSource hitSfx;

    void Start()
    {
        anim = GetComponent<Animator>();
        // Initialize current ammo to max ammo at the start
        currentAmmo = maxAmmo;


        // Optional: Update the ammo text UI if it exists
        if (ammoText != null)
        {
            UpdateAmmoText();
        }
    }

    void Update()
    {
        // Prevent shooting while reloading
        if (isReloading)
        {
            return;
        }

        // Shoot when Fire1 button is pressed and there is ammo left
        if (Input.GetButtonDown("Fire1") && currentAmmo > 0 && canShoot)
        {
            StartCoroutine(ShootWithDelay());
        }

        // Reload when R key is pressed and not already reloading
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
        }

        // Optional: Update the ammo text UI if it exists
        if (ammoText != null)
        {
            UpdateAmmoText();
        }
    }

    private void OnEnable()
    {
        gun.transform.rotation = Quaternion.identity;
        canShoot = true;
        isReloading = false;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator ShootWithDelay()
    {
        canShoot = false;

        Shoot();

        yield return new WaitForSeconds(fireRate);

        canShoot = true;
    }

    void Shoot()
    {
        anim.SetTrigger("Shoot");
        muzzleFlash.Play();

        // Decrement ammo count
        currentAmmo--;
        shootSfx.Play();

        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {

            Debug.Log(hit.transform.name);

            /* Instantiate impact effect
            GameObject impactDel = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactDel, 1f);*/

            // Damage enemy if hit
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target != null)
            {
                GameObject impactDel = Instantiate(hitImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactDel, 1f); 

                    hitSfx.Play();
                target.TakeDamage(damage);
            }

            // Apply impact force to rigidbody
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
        }
    }

    public IEnumerator Reload()
    {
        reloadSfx.Play();
        anim.SetTrigger("Reload");

        // Set reloading flag to true
        isReloading = true;

        Debug.Log("Reloading...");

        // Wait for the reload time
        yield return new WaitForSeconds(reloadTime);

        // Reset ammo to max ammo after reloading
        currentAmmo = maxAmmo;

        // Set reloading flag to false
        isReloading = false;

        Debug.Log("Reloaded!");
    }

    // Update the ammo text UI
    void UpdateAmmoText()
    {
        ammoText.text = currentAmmo + " / " + maxAmmo;
    }
}