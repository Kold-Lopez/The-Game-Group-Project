using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deBuffer : MonoBehaviour
{
    [Header("-----DetrimentObject -----")]
    [SerializeField] GameObject det;

    [Header("----- Modifiers -----")]
    [Range(1, 5)][SerializeField] int detAmount;
    [SerializeField] int timeActive;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            det.SetActive(false);
        }
    }

    IEnumerator debuff()
    {
        int originalDamage = GunSystem.shootDmg;
        GunSystem.shootDmg /= detAmount;
        yield return new WaitForSeconds(timeActive);
        GunSystem.shootDmg = originalDamage;
    }
}
