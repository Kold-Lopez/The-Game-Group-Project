using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phyAttack : MonoBehaviour
{

    //[SerializeField] Rigidbody rb;
    [SerializeField] int damage;

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
}
