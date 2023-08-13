using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSystem : MonoBehaviour
{
    [Header("----- GunObjects -----")]
    [SerializeField] GameObject GUN1;
    [SerializeField] GameObject GUN2;
    [SerializeField] GameObject activeGun;
    [SerializeField] int ReloadTime;
    [SerializeField] bool HasSmg;


    [Header("----- Gun1Stats -----")]
    [SerializeField] float shootRate;
    [SerializeField] int shootDmg;
    [SerializeField] float shootRange;
    [SerializeField] int GUN1MagazineMAX;
    [SerializeField] int GUN1Magazine;
    [SerializeField] bool StartActive;

    [Header("----- Gun2Stats -----")]
    [SerializeField] float shootRate2;
    [SerializeField] int shootDmg2;
    [SerializeField] float shootRange2;
    [SerializeField] int GUN2MagazineMAX;
    [SerializeField] int GUN2Magazine;
    [SerializeField] int GUN2AmmoTotal;

    private bool gunActive;
    private bool isShooting;
    private bool noAmmo1;
    private bool noAmmo2;
    // Start is called before the first frame update
    void Start()
    {
        if(StartActive == true)
        {
            GUN1.SetActive(StartActive);
            gunActive = true;
        }
        GUN1Magazine = GUN1MagazineMAX;
      
        GUN2.SetActive(false);
        GUN2Magazine = GUN2MagazineMAX;
    }

    // Update is called once per frame
    void Update()
    {
        //activate weapons
        if (Input.GetKeyDown(KeyCode.Q))
        {
            activateOrDeactivateGun(GUN1);
        }
        if (Input.GetKeyDown(KeyCode.Z) && HasSmg == true)
        {
            activateOrDeactivateGun(GUN2);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            activateOrDeactivateGun(null);
        }
        if(GUN2.active == true)
        {
            HasSmg = true;
        }
        if (Input.GetKeyDown(KeyCode.R) && gunActive)
        {
            StartCoroutine(reload());
            if(activeGun == GUN1)
            {
                noAmmo1 = false;
            }
            else if(activeGun == GUN2)
            {
                noAmmo2 = false;
            }
        }
        //when magazine drops to zero
        if(GUN1Magazine <= 0)
        {
            noAmmo1 = true;
        }
        if (GUN2Magazine <= 0)
        {
            noAmmo2 = true;
        }
        //shooting
        if (Input.GetButton("Shoot") && !isShooting && gunActive)
        {
            if (GUN1.active == true && noAmmo1 == true)
            {
                //start animation here
                StartCoroutine(reload());
                noAmmo1 = false;
            }
            else if (GUN2.active == true && noAmmo2 == true)
            {   
                StartCoroutine(reload());
                noAmmo2 = false;
            }
            else if(GUN1.active == true && noAmmo1 == false || GUN2.active == true && noAmmo2 == false)
            {
                StartCoroutine(shoot());
            }
        }
    }
    void activateOrDeactivateGun(GameObject gunToActivate)
    {
        if(gunToActivate != null) 
        { 
            GUN1.SetActive(false);
            GUN2.SetActive(false);
            gunToActivate.SetActive(true);
            gunActive = true;
        }
        else if(gunToActivate == null)
        {
            GUN1.SetActive(false);
            GUN2.SetActive(false);
            gunActive = false;
        }
    }
    IEnumerator shoot()
    {
        isShooting = true;

        //shoot something
        RaycastHit hit;
        if(GUN1.active == true)
        {
            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, shootRange))
            {
                IDamage damageable = hit.collider.GetComponent<IDamage>();
                GUN1Magazine -= 1;

                if (damageable != null)
                {
                    damageable.takeDamage(shootDmg);
                }
            }

            yield return new WaitForSeconds(shootRate);
        }
        if (GUN2.active == true)
        {
            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, shootRange2))
            {
                IDamage damageable = hit.collider.GetComponent<IDamage>();
                GUN2Magazine -= 1;

                if (damageable != null)
                {
                    damageable.takeDamage(shootDmg2);
                }
            }

            yield return new WaitForSeconds(shootRate2);
        }

        isShooting = false;
    }
    IEnumerator reload()
    {
        //or start animation here
        yield return new WaitForSeconds(ReloadTime);
        if (GUN1.active == true)
        {
            GUN1Magazine = GUN1MagazineMAX;
        }

        if (GUN2.active == true)
        {
            int ammoLoss = GUN2MagazineMAX - GUN2Magazine;
            GUN2Magazine = GUN2MagazineMAX;
            GUN2AmmoTotal = GUN2AmmoTotal - ammoLoss;
        }
    }
    
}
