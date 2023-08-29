using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Supplement : MonoBehaviour
{
    [Header("----- SuppObject -----")]
    [SerializeField] GameObject supplement;

    [Header("----- Modifiers -----")]
    [Range(1, 5)][SerializeField] int buffAmount;
    [SerializeField] int timeActive;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            supplement.SetActive(false);
            buff();
        }
    }
    IEnumerator buff()
    {
        int originalDamage = GunSystem.shootDmg;
        GunSystem.shootDmg *= buffAmount;
        yield return new WaitForSeconds(timeActive);
        GunSystem.shootDmg = originalDamage;
    }
}
