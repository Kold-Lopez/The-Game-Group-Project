using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAIbase : MonoBehaviour, IDamage
{
    [SerializeField] Renderer model;
    [SerializeField] NavMeshAgent agent;


    [SerializeField] int HP;
    [SerializeField] int speed;
    [SerializeField] int playerfacespeed;

    [SerializeField] float shootrate;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootpos;

    Vector3 playerDir;
    bool isShooting;
    bool playerinRange;
    private Animator animator;

    void Start()
    {
        gameManager.instance.UpdateGameGoal(1);
    }

    void Update()
    {
        //if(playerinRange)
        //{
          playerDir = gameManager.instance.player.transform.position - transform.position;

          if (agent.remainingDistance <= agent.stoppingDistance)
          {
            facePlayer();
            if (!isShooting)
            {
                //animator.SetBool("startShooting", true);
                StartCoroutine(Shoot());
            }
            //else
            //    animator.SetBool("startShooting", true);
            }

       // }
          agent.SetDestination(gameManager.instance.player.transform.position);

    }
    public void takeDamage(int amount)
    {
        HP -= amount;

        StartCoroutine(flashdmg());
        if (HP <= 0)
        {
            gameManager.instance.UpdateGameGoal(-1);
            Destroy(gameObject);
        }
    }

    IEnumerator flashdmg()
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = Color.white;

    }

    void facePlayer()
    {
        Quaternion rot = Quaternion.LookRotation(playerDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * playerfacespeed);

    }

    IEnumerator Shoot()
    {
        isShooting = true;
        Instantiate(bullet, shootpos.position, transform.rotation);

        yield return new WaitForSeconds(shootrate);
        isShooting = false;

    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.CompareTag("Player"))
    //    {
    //        playerinRange = true;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        playerinRange = false;
    //    }
    //}

}