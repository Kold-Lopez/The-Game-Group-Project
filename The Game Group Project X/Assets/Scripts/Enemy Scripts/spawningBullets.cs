using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawningBullets : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [Range(1, 99)][SerializeField] int damage;
    [Range(1, 99)][SerializeField] int speed;
    [Range(1, 99)][SerializeField] int destroyTime;
    private Collider collider;
    [SerializeField] meleeEnemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);
        rb.velocity = transform.forward * speed;

    }

    public void OnTriggerEnter(Collider other)
    {
        collider = other.GetComponent<Collider>();
        if (collider != null)
        {
            
           Instantiate(enemy, transform.position + new Vector3(0, 1), Quaternion.identity);
            enemy.enabled = true;
           
        }
        Destroy(gameObject);
    }
}
