using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject objectToSpawn;
    [SerializeField] Transform[] spawnPos;
    [SerializeField] int numberToSpawn;
    [SerializeField] float timeBetweenSpawns;
    [SerializeField] List<GameObject> objectList = new List<GameObject>();


    int numberSpawned;
    int numberKilled;
    bool isSpawning;
    bool startSpawning = false;
    bool deSpawn;

    // Start is called before the first frame update
    void Start()
    {
        gameManager.instance.UpdateGameGoal(numberToSpawn);
    }

    private void Update()
    {
        if (startSpawning && !isSpawning && numberSpawned < numberToSpawn)
        {
            StartCoroutine(spawn());
        }
    }

    public IEnumerator spawn()
    {
        if (startSpawning && numberSpawned < numberToSpawn)
        {
            isSpawning = true;


            GameObject objectSpawn = Instantiate(objectToSpawn, spawnPos[Random.Range(0, spawnPos.Length)].position, objectToSpawn.transform.rotation);
            if (objectSpawn.GetComponent<meleeEnemy>())
            {
                objectSpawn.GetComponent<meleeEnemy>().whereISpawnedWave = this;
            }


            numberSpawned++;

            yield return new WaitForSeconds(timeBetweenSpawns);
            isSpawning = false;
        }
    }

    public void startWave()
    {
        startSpawning = true;
    }

    public void updateEnemyNumber()
    {
        numberKilled++;
        if (numberKilled >= numberToSpawn)
        {
            startSpawning = false;
            StartCoroutine(waveManager.instance.startWave());

        }
    }


}
