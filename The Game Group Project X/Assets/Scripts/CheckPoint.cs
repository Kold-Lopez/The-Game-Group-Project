using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
   
    

    //Add Later: CheckPoint Logic For respawning in later levels

    public void OnTriggerEnter(Collider other)
    {
        gameManager.instance.advancePrompt.SetActive(false);
        if (other.CompareTag("Player"))
        {
           
            waveManager.instance.level2 = true;
            gameManager.instance.level2 = true;
            gameManager.instance.UpdateGameGoal(0);
            StartCoroutine(waveManager.instance.startWave());
            
        }
    }
}
