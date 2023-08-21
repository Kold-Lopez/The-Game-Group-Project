using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDropPickup : MonoBehaviour
{
    [SerializeField] GameObject WeaponPickup;
    [SerializeField] GUNtemp theThompson;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.gunSystem.gunPickUP(theThompson);
            WeaponPickup.SetActive(false);
        }
    }
}
