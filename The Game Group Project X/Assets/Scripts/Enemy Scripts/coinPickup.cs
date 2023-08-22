using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinPickup : MonoBehaviour
{

    public int coinCurr;
    public int coinMax;
    public int coinMin;

    // Start is called before the first frame update
    void Start()
    {
        coinCurr = coinMin;

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            coinDrop(1, 10);
            Destroy(gameObject);
        }
    }
    public void coinDrop(int min, int max)
    {
        int total = Random.Range(min, max);

        gameManager.instance.playerScript.coinPickUp(total);

        coinCurr += total;

        Debug.Log(coinCurr);
    }
}
