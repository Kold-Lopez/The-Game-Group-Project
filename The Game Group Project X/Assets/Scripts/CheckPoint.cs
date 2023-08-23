using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
   
    

    //Add Later: CheckPoint Logic For respawning in later levels

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            waveManager.instance.level2 = true;
            gameManager.instance.level2 = true;
            gameManager.instance.UpdateGameGoal(0);
            waveManager.instance.startWave();
        }
    }
}
