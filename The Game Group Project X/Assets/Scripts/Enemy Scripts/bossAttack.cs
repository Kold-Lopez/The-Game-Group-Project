using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAttack : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
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
    private void OnCollisionEnter(Collision collision)
    {
        IDamage damager = collision.gameObject.GetComponent<IDamage>();

        if (damager != null)
        {
            damager.takeDamage(damage);
        }
    }
    
}
