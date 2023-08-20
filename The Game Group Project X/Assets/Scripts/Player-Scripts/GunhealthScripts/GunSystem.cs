using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunSystem : MonoBehaviour
{
    [SerializeField] int selectedGun;

    [Header("----- Gun Stuff-----")]
    [SerializeField] List<GUNtemp> gunList = new List<GUNtemp>();
    [SerializeField] GUNtemp Pistol;
    [SerializeField] GameObject gunModel;
    [SerializeField] GameObject gunModel2;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform pistolShootPos;
    [SerializeField] Transform thompsonShootPos;

    [Header("----- Current Gun Stats -----")]
    [SerializeField] float shootRate;
    [SerializeField] int shootDmg;
    [SerializeField] float shootRange;
    [SerializeField] int ReloadTime;
    [SerializeField] int currentAmmo;
    [SerializeField] int currentReserveAmmo;
    [SerializeField] string weaponName;


    [SerializeField] bool StartActive;
    private bool isReloading;
    private int ammoToTake;
    private bool noAmmo;
    private bool isShooting;

    //// Start is called before the first frame update
    void Start()
    {
        if(StartActive == true)
        {
            gunList.Add(Pistol);

            shootDmg = Pistol.shootDamage;
            shootRange = Pistol.shootDist;
            shootRate = Pistol.shootRate;
            currentReserveAmmo = Pistol.maxAmmo; //ammo tracking convience
            ReloadTime = Pistol.ReloadTime;
            weaponName = Pistol.weaponDifferentiator;

            Pistol.currentAmmo = Pistol.weaponClipSize;
            currentAmmo = Pistol.currentAmmo; //ammo tracking convience

            isShooting = false;

            gunModel.SetActive(true);
            //replace setActive with an animation

            selectedGun = gunList.Count - 1;

        }
        
    }

    void selectGun()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && selectedGun < gunList.Count - 1)
        {
            selectedGun++;
            changeGun();
            //Update UI
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && selectedGun > 0)
        {
            selectedGun--;
            changeGun();
            //Update UI
        }
    }
    void changeGun()
    {
        shootDmg = gunList[selectedGun].shootDamage;
        shootRange = gunList[selectedGun].shootDist;
        shootRate = gunList[selectedGun].shootRate;
        ReloadTime = gunList[selectedGun].ReloadTime;
        //pulling out weapon animation

        if (gunList[selectedGun].weaponDifferentiator == "Pistol")
        {
            gunModel2.SetActive(false);
            gunModel.SetActive(true);
        }
        else if (gunList[selectedGun].weaponDifferentiator == "Thompson")
        {
            gunModel.SetActive(false);
            gunModel2.SetActive(true);
        }

    }

    //// Update is called once per frame
    void Update()
    {
          //activate weapons
        selectGun();
       if (Input.GetKeyDown(KeyCode.R) && gunList.Count > 0 && !isReloading)
       {
           StartCoroutine(Reload());
           
       }
       //when magazine drops to zero
       if(gunList[selectedGun].currentAmmo == 0)
       {
           noAmmo = true;
       }
       else
       {
           noAmmo = false;
       }

        //shooting
        if (gunList.Count > 0 && Input.GetButton("Shoot") && !isShooting)
        {
              StartCoroutine(shoot());
        }

        currentAmmo = gunList[selectedGun].currentAmmo; //ammo tracking convience
        currentReserveAmmo = gunList[selectedGun].maxAmmo; //ammo tracking convience
    }

    IEnumerator shoot()
    {
        isShooting = true;
        //shoot something
        RaycastHit hit;

        if (noAmmo == false)
        {
            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, gunList[selectedGun].shootDist))
            {
                //add muzzleflash and gun kick animations here
                if(gunList[selectedGun].weaponDifferentiator == "Pistol")
                {
                    Instantiate(bullet, pistolShootPos.position, transform.rotation);
                }
                else if(gunList[selectedGun].weaponDifferentiator == "Thompson")
                {
                    Instantiate(bullet, thompsonShootPos.position, transform.rotation);
                }
                IDamage damageable = hit.collider.GetComponent<IDamage>();
                gunList[selectedGun].currentAmmo--;
                if (damageable != null)
                {
                    damageable.takeDamage(gunList[selectedGun].shootDamage);
                }
            }
            yield return new WaitForSeconds(gunList[selectedGun].shootRate);

            isShooting = false;
        }
        //else gun no shoot sfx
    }
    IEnumerator Reload()
    {
        isReloading = true;
        if(gunList[selectedGun].maxAmmo > 0)
        {
            if(gunList[selectedGun].weaponDifferentiator == "Pistol")
            {
                gunModel.SetActive(false);
                //replace setactive with animation here

            }
            else if(gunList[selectedGun].weaponDifferentiator == "Thompson")
            {
                gunModel2.SetActive(false);
                //replace setactive with animation here
            }

            ammoToTake = gunList[selectedGun].weaponClipSize - gunList[selectedGun].currentAmmo;
            gunList[selectedGun].currentAmmo = 0;
            yield return new WaitForSeconds(gunList[selectedGun].ReloadTime);
            gunList[selectedGun].maxAmmo -= ammoToTake;
            gunList[selectedGun].currentAmmo = gunList[selectedGun].weaponClipSize;

            if (gunList[selectedGun].weaponDifferentiator == "Pistol")
            {
                gunModel.SetActive(true);
                //replace setactive with animation here
            }
            else if (gunList[selectedGun].weaponDifferentiator == "Thompson")
            {
                gunModel2.SetActive(true);
                //replace setactive with animation here
            }

        }
        isShooting = false;
        noAmmo = false;
        isReloading = false;
    }
    public void gunPickUP(GUNtemp gun)
    {
        gunList.Add(gun);

        shootDmg = gun.shootDamage;
        shootRange = gun.shootDist;
        shootRate = gun.shootRate;
        weaponName = gun.weaponDifferentiator;

        gun.currentAmmo = gun.weaponClipSize;

        ReloadTime = gun.ReloadTime;
        isShooting = false;

        gunModel.SetActive(false);
        gunModel2.SetActive(true);

        selectedGun = gunList.Count - 1;

    }

}
