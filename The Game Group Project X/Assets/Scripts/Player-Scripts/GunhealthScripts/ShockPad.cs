using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockPad : MonoBehaviour
{
    private int shockingTimer = 2;
    private bool shocking = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //create enemy tag so we can shock them too
        if (other.CompareTag("Player") && shocking == false)
        {
            gameManager.instance.playerScript.takeDamage(30);

            StartCoroutine(shockerTimer());
        }
    }
    IEnumerator shockerTimer()
    {
        shocking = true;
        yield return new WaitForSeconds(shockingTimer);
        shocking = false;
    }
}
