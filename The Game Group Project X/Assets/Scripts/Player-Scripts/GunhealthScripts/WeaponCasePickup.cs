using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCasePickup : MonoBehaviour, IInteractable
{
    [Header("----- WeaponCaseObjects -----")]
    //[SerializeField] GameObject weaponRack;
    [SerializeField] BoxCollider box;
    [Header("----- WeaponObjects -----")]
    [SerializeField] GUNtemp theThompson;
    

    public void Interact()
    {
        gameManager.instance.gunSystem.gunPickUP(theThompson);
        box.enabled = false;
    }

}
