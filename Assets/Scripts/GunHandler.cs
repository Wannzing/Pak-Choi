using UnityEngine;

public class GunHandler : MonoBehaviour
{
    public GameObject smg;
    public GameObject revolver;

    private bool isReloading = false;
    private bool canShoot = true;
    private GameObject currentWeapon;

    private void Awake()
    {
        revolver.SetActive(false);
        smg.SetActive(true);
        currentWeapon = smg;
    }

    private void Update()
    {

        bool smgReloading = smg.GetComponent<SMG>()?.isReloading ?? false;
        bool revolverReloading = revolver.GetComponent<Gun>()?.isReloading ?? false;

        isReloading = smgReloading || revolverReloading;

        if (!isReloading)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && currentWeapon != smg)
            {
                canShoot = true;
                isReloading = false;
                SwitchWeapon(smg);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && currentWeapon != revolver)
            {
                canShoot = true;
                isReloading = false;
                SwitchWeapon(revolver);
            }
        }
    }
    private void SwitchWeapon(GameObject newWeapon)
    {
        if (currentWeapon != null)
        {
            currentWeapon.SetActive(false);

            // Optional: Save the state explicitly
            var stateSaver = currentWeapon.GetComponent<WeaponStateSaver>();
            if (stateSaver != null)
                stateSaver.SaveState();
        }

        // Enable new weapon
        newWeapon.SetActive(true);

        // Restore its original transform values
        var newStateSaver = newWeapon.GetComponent<WeaponStateSaver>();
        if (newStateSaver != null)
            newStateSaver.RestoreState();

        currentWeapon = newWeapon;
    }

    
}
