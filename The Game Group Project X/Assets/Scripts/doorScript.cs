using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScript : MonoBehaviour
{
    [SerializeField] BoxCollider model;
    [SerializeField] SphereCollider target;
    public waveManager waveCount;

    private Vector3 start;

    // Start is called before the first frame update
    void Start()
    {
        start = model.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (waveCount.waveCurrent == 4 || waveCount.waveCurrent == 9)
        {
            target = other.GetComponent<SphereCollider>();

            model.transform.position = new Vector3(model.transform.position.x, model.transform.position.y + 10, model.transform.position.z);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        target = other.GetComponent<SphereCollider>();

        model.transform.position = start;
    }
}
