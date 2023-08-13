using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCasePickup : MonoBehaviour, IInteractable
{
    [Header("----- WeaponCaseObjects -----")]
    //[SerializeField] GameObject weaponRack;
    [SerializeField] BoxCollider box;
    [Header("----- WeaponObjects -----")]
    [SerializeField] GameObject newWeapon;
    [SerializeField] GameObject currentHeldWeapon;
    

    public void Interact()
    {
        currentHeldWeapon.SetActive(false);
        newWeapon.SetActive(true);
        box.enabled = false;
    }

}
