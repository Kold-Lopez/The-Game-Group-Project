using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class phyAttack : MonoBehaviour
{


    [SerializeField] int damage;
    private SphereCollider ShpCollider;

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
        Debug.Log("Collide",other);
        other.enabled = true;
        IDamage damagable = other.GetComponent<IDamage>();
        
        if (damagable != null)
        {
            damagable.takeDamage(damage);
            Debug.Log(damage);
        }
        

    }
    public void TriggerOFF(Collider other)
    {
        other.enabled = false;
    }



}
