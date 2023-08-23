using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class waveManager : MonoBehaviour
{
    public static waveManager instance;
    [SerializeField] waveSpawner[] spawners;
    [SerializeField] int timeBetweenWaves;

    public int waveCurrent;
    public bool level2 = false;


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

        if (waveCurrent < 5)
        {

            yield return new WaitForSeconds(timeBetweenWaves);

            gameManager.instance.UpdateGameGoal(spawners[waveCurrent - 1].numberToSpawn); //Updates wave Counter
            spawners[waveCurrent - 1].startWave();

        }

        if (level2)
        {
            waveCurrent++;

            yield return new WaitForSeconds(timeBetweenWaves);

            gameManager.instance.UpdateGameGoal(0);

            if (waveCurrent <= 10)
            {
                spawners[waveCurrent - 1].startWave();

            }
        }



    }
}
