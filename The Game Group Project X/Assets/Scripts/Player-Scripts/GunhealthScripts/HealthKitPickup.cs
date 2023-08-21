using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKitPickup : MonoBehaviour
{
    [Header("----- MedkitObject -----")]
    [SerializeField] GameObject Medkit;

    [Header("----- HealAmount -----")]
    [Range(1, 50)][SerializeField] int healAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.playerScript.takeHealth(healAmount);
            Medkit.SetActive(false);   
        }
    }
}
