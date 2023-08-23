using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunSystem : MonoBehaviour
{
    [SerializeField] int selectedGun;

    [Header("----- Gun Stuff-----")]
    [SerializeField] List<GUNtemp> gunList = new List<GUNtemp>();
    [SerializeField] GUNtemp Pistol;
    [SerializeField] GUNtemp Thompson;
    [SerializeField] GUNtemp Shotgun;
    [SerializeField] GameObject gunModel;
    [SerializeField] GameObject gunModel2;
    [SerializeField] GameObject gunModel3;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform pistolShootPos;
    [SerializeField] Transform thompsonShootPos;
    [SerializeField] Transform shotgunShootPos;

    [Header("----- Current Gun Stats -----")]
    [SerializeField] float shootRate;
    [SerializeField] int shootDmg;
    [SerializeField] float shootRange;
    [SerializeField] int ReloadTime;
    public static int currentAmmo;
    public static int currentReserveAmmo;
    [SerializeField] string weaponName;


    [SerializeField] bool StartActive;
    [SerializeField] bool gameStartStats;
    private bool isReloading;
    private bool gunAmmoAdded = false;
    private int previousClipAmmo;
    private int ammoToTake;
    private bool noAmmoPistol;
    private bool noAmmoThompson;
    private bool noAmmoShotgun;
    private bool isShooting;

    //// Start is called before the first frame update
    void Start()
    {
        if(gameStartStats == true)
        {
            Pistol.maxAmmo = Pistol.gameStartAmmo;
            Thompson.maxAmmo = Thompson.gameStartAmmo;
            Shotgun.maxAmmo = Shotgun.gameStartAmmo;
        }
        if(StartActive == true)
        {
            gunList.Add(Pistol);

            shootDmg = Pistol.shootDamage;
            shootRange = Pistol.shootDist;
            shootRate = Pistol.shootRate;
            currentReserveAmmo = Pistol.maxAmmo; //ammo tracking convience
            ReloadTime = (int)Pistol.ReloadTime;
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
        ReloadTime = (int)gunList[selectedGun].ReloadTime;
        //pulling out weapon animation

        if (gunList[selectedGun].weaponDifferentiator == "Pistol")
        {
            gunModel.SetActive(true);
            gunModel2.SetActive(false);
            gunModel3.SetActive(false);
        }
        else if (gunList[selectedGun].weaponDifferentiator == "Thompson")
        {
            gunModel.SetActive(false);
            gunModel2.SetActive(true);
            gunModel3.SetActive(false);
        }
        else if (gunList[selectedGun].weaponDifferentiator == "Shotgun")
        {
            gunModel2.SetActive(false);
            gunModel.SetActive(false);
            gunModel3.SetActive(true);
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
           gunList[selectedGun].noAmmo = true;
       }
       else
       {
           gunList[selectedGun].noAmmo = false;
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

        if (gunList[selectedGun].noAmmo == false)
        {
            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, gunList[selectedGun].shootDist))
            {
                //add muzzleflash and gun kick animations here
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
            else if (gunList[selectedGun].weaponDifferentiator == "Shotgun")
            {
                gunModel3.SetActive(false);
                //replace setactive with animation here
            }

            ammoToTake = gunList[selectedGun].weaponClipSize - gunList[selectedGun].currentAmmo;
            previousClipAmmo = gunList[selectedGun].currentAmmo;
            gunList[selectedGun].currentAmmo = 0;

            yield return new WaitForSeconds(gunList[selectedGun].ReloadTime);

            if (gunList[selectedGun].maxAmmo <= ammoToTake)
            {
                ammoToTake = gunList[selectedGun].maxAmmo;
                gunList[selectedGun].maxAmmo -= ammoToTake;
                gunList[selectedGun].currentAmmo += ammoToTake + previousClipAmmo;

            }
            else if(gunList[selectedGun].maxAmmo > ammoToTake)
            {
                gunList[selectedGun].maxAmmo -= ammoToTake;
                gunList[selectedGun].currentAmmo = gunList[selectedGun].weaponClipSize;
            }


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
            else if (gunList[selectedGun].weaponDifferentiator == "Shotgun")
            {
                gunModel3.SetActive(true);
                //replace setactive with animation here
            }

        }
        isShooting = false;
        gunList[selectedGun].noAmmo = false;
        isReloading = false;
    }
    public void gunPickUP(GUNtemp gun)
    {
        for(int i = 0; i < gunList.Count; i++)
        {
            if(gun.weaponDifferentiator == gunList[i].weaponDifferentiator)
            {
                if(gun.weaponDifferentiator == "Thompson")
                {
                    gunList[i].maxAmmo += 100;
                    gunAmmoAdded = true;
                }
                else if(gun.weaponDifferentiator == "Shotgun")
                {
                    gunList[i].maxAmmo += 24;
                    gunAmmoAdded = true;
                }
            }
           
        }
        if(gunAmmoAdded == false)
        {
            gunList.Add(gun);

            shootDmg = gun.shootDamage;
            shootRange = gun.shootDist;
            shootRate = gun.shootRate;
            gun.maxAmmo = gun.gameStartAmmo;
            weaponName = gun.weaponDifferentiator;

            gun.currentAmmo = gun.weaponClipSize;

            ReloadTime = (int)gun.ReloadTime;
            isShooting = false;

            if (gun.weaponDifferentiator == "Thompson")
            {
                gunModel.SetActive(false);
                gunModel2.SetActive(true);
                gunModel3.SetActive(false);
            }
            else if(gun.weaponDifferentiator == "Shotgun")
            {
                gunModel.SetActive(false);
                gunModel2.SetActive(false);
                gunModel3.SetActive(true);
            }

            selectedGun = gunList.Count - 1;
        }
        

    }

}
