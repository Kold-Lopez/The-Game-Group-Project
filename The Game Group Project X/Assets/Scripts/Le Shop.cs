using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeShop : MonoBehaviour
{
    public GameObject shotDrop;
    public GameObject rifleDrop;
    public GameObject healthDrop;
    public GameObject barDrop;
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
        gameManager.instance.currentShop = this;
        gameManager.instance.enterShop();
    }
}
