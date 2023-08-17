using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;
    [SerializeField] Transform[] spawnPos;
    [SerializeField] int numberToSpawn;
    [SerializeField] float timeBetweenSpawns;
    [SerializeField] List<GameObject> objectList = new List<GameObject>();

    int NumberSpawned;
    bool isSpawning;
    bool startSpawning;
    bool deSpawn;


    void Start()
    {
        //gameManager.instance.UpdateGameGoal(numberToSpawn);
    }

    private void Update()
    {
        if (startSpawning && !isSpawning && NumberSpawned < numberToSpawn)
        {
            StartCoroutine(Spawn());
        }
    }

    public IEnumerator Spawn()
    {
        isSpawning = true;

        GameObject objectSpawned = Instantiate(objectToSpawn, spawnPos[Random.Range(0, spawnPos.Length)].position, objectToSpawn.transform.rotation);
        
         if (objectSpawned.GetComponent<enemyAIbase>())
         {
             objectSpawned.GetComponent<enemyAIbase>().isspawner = this;
         }
         
        objectList.Add(objectSpawned);
        NumberSpawned++;
        yield return new WaitForSeconds(timeBetweenSpawns);
        isSpawning = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            startSpawning = true;

        }
    }

    //activates despawning enemys
    /*
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            deSpawn = true;
            for (int i = 0; i < objectList.Count; i++)
            {
                Destroy(objectList[i]);
            }
            objectList.Clear();

            NumberSpawned = 0;
            startSpawning = false;
        }
    }
     */
    public void HeyIdied()
    {
        NumberSpawned--;
    }

    //KL
}
