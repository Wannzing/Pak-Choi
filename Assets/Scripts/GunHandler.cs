using UnityEngine;

public class GunHandler : MonoBehaviour
{
    public GameObject smg;
    public GameObject revolver;

    private bool isReloading = false;
    private bool canShoot = true;
    private GameObject currentWeapon;

    public GameObject smg_ui;
    public GameObject revolver_ui;

    private void Awake()
    {
        revolver.SetActive(false);
        smg.SetActive(true);
        currentWeapon = smg;
        revolver_ui.SetActive(false);
        smg_ui.SetActive(true);
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
                revolver_ui.SetActive(false);
                smg_ui.SetActive(true);
                SwitchWeapon(smg);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && currentWeapon != revolver)
            {
                canShoot = true;
                isReloading = false;
                SwitchWeapon(revolver);
                revolver_ui.SetActive(true);
                smg_ui.SetActive(false);
            }
        }
    }
    private void SwitchWeapon(GameObject newWeapon)
    {
        if (currentWeapon != null)
        {
            currentWeapon.SetActive(false);

            
        }

        // Enable new weapon
        newWeapon.SetActive(true);

        currentWeapon = newWeapon;
    }

    
}
