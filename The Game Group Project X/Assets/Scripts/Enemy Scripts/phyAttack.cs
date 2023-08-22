using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class phyAttack : MonoBehaviour
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
        IDamage damagable = other.GetComponent<IDamage>();
        if (damagable != null)
        {
            damagable.takeDamage(damage);
        }
        

    }
    public void TriggerOFF(Collider other)
    {
        other.enabled = false;
    }
    

}
