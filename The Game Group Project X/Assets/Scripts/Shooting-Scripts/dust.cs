using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dust : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] int speed;
    [SerializeField] int destroyTime;
    [SerializeField] int dustTime;

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.fog = false;
        Destroy(gameObject, destroyTime);
        rb.velocity = transform.forward * speed;
    }

    public void OnTriggerEnter(Collider other)
    {
        IDamage damagable = other.GetComponent<IDamage>();
        if (damagable != null)
        {
            dustInEyes();
        }
        Destroy(gameObject);
    }

    IEnumerator dustInEyes()
    {
        RenderSettings.fog = true;
        yield return new WaitForSeconds(dustTime);
        RenderSettings.fog = false;
    }
}
