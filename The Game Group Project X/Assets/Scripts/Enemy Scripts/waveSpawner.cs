using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject objectToSpawn;
    [SerializeField] GameObject objectToSpawn2;
    [SerializeField] GameObject objectToSpawn3;
    [SerializeField] Transform[] spawnPos;
    [SerializeField] public int numberToSpawn;
    [SerializeField] float timeBetweenSpawns;
    [SerializeField] List<GameObject> objectList = new List<GameObject>();



    int numberSpawned;
    int numberKilled;
    bool isSpawning;
    bool startSpawning = false;
    bool deSpawn;
    int increment = 0;

    // Start is called before the first frame update
    void Start()
    {
        //gameManager.instance.UpdateGameGoal(numberToSpawn);
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
            else if (objectSpawn.GetComponent<juggEnemy>())
            {
                objectSpawn.GetComponent<juggEnemy>().whereISpawnedWave = this;
            }
            else if (objectSpawn.GetComponent<enemyAIbase>())
            {
                objectSpawn.GetComponent<enemyAIbase>().whereISpawnedWave = this;
            }
            numberSpawned++;


            if (objectToSpawn2 != null)
            {
                GameObject objectSpawn2 = Instantiate(objectToSpawn2, spawnPos[Random.Range(0, spawnPos.Length)].position, objectToSpawn.transform.rotation);
                numberSpawned++;
                numberToSpawn++;
                if (objectSpawn.GetComponent<meleeEnemy>())
                {
                    objectSpawn.GetComponent<meleeEnemy>().whereISpawnedWave = this;
                }
                else if (objectSpawn.GetComponent<juggEnemy>())
                {
                    objectSpawn.GetComponent<juggEnemy>().whereISpawnedWave = this;
                }
                else if (objectSpawn.GetComponent<enemyAIbase>())
                {
                    objectSpawn.GetComponent<enemyAIbase>().whereISpawnedWave = this;
                }
            }

            else if (objectToSpawn3 != null)
            {
                GameObject objectSpawn3 = Instantiate(objectToSpawn3, spawnPos[Random.Range(0, spawnPos.Length)].position, objectToSpawn.transform.rotation);
                numberSpawned++;
                numberToSpawn++;
                if (objectSpawn.GetComponent<meleeEnemy>())
                {
                    objectSpawn.GetComponent<meleeEnemy>().whereISpawnedWave = this;
                }
                else if (objectSpawn.GetComponent<juggEnemy>())
                {
                    objectSpawn.GetComponent<juggEnemy>().whereISpawnedWave = this;
                }
                else if (objectSpawn.GetComponent<enemyAIbase>())
                {
                    objectSpawn.GetComponent<enemyAIbase>().whereISpawnedWave = this;
                }
            }


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
