using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class waveManager : MonoBehaviour
{
    public static waveManager instance;

    [SerializeField] waveSpawner[] spawners;

    [SerializeField] int timeBetweenWaves;


    public int waveCurrent;


    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {

            instance = this;
            StartCoroutine(startWave());
        }
    }

  
    public IEnumerator startWave()
    {
        waveCurrent++;
        if (waveCurrent <= instance.spawners.Length)
        {
            // spawners[waveCurrent -1].enabled = true;
            yield return new WaitForSeconds(timeBetweenWaves);
            spawners[waveCurrent - 1].startWave();

        }

    }
}
