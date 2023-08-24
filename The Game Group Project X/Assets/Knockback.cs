using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    //[SerializeField] Rigidbody rb;
    [SerializeField] int damage;
    //private SphereCollider ShpCollider;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter(Collider other)
    {
        Vector3 explode = transform.position;
        Rigidbody rb = other.GetComponent<Rigidbody>();
        IDamage damagable = other.GetComponent<IDamage>();
        if (damagable != null)
        {
            damagable.takeDamage(damage);
        }
        if(rb != null)
        {
            //rb.AddExplosionForce(200,explode,50,50f);
            rb.AddForce(explode,ForceMode.Impulse);
            
            
        }


    }
    public void TriggerOFF(Collider other)
    {
        other.enabled = false;
    }


}

