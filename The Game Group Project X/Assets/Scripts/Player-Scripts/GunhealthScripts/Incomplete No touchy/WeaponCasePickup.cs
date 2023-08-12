using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCasePickup : MonoBehaviour
{
    [SerializeField] GameObject weaponRack;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject tempcurrentWeapon;
    [SerializeField] BoxCollider box;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //UI code go here

        if(Input.GetKeyDown(KeyCode.E))
        {
            //pickupweapon
            //gameManager.instance.playerScript.pistolActive = false;
            tempcurrentWeapon.SetActive(false);
            weapon.SetActive(true);
            box.gameObject.SetActive(false);
        }
    }
}
