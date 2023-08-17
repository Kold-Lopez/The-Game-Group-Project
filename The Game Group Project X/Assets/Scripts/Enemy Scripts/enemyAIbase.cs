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
    Vector3 playerPos;
    Vector3 enemyPos;
    bool isShooting;
    bool playerinRange;
    private Animator animator;

    public Spawner isspawner;

    float angle;


    void Start()
    {
        gameManager.instance.UpdateGameGoal(1);
    }

    void Update()
    {
        playerDir = gameManager.instance.player.transform.position - transform.position;
        angle = gameManager.instance.player.transform.position.y - 2;
        

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            facePlayer();
            if (!isShooting)
            {
                StartCoroutine(Shoot());
            }
        }

        agent.SetDestination(gameManager.instance.player.transform.position);

    }
    public void takeDamage(int amount)
    {
        HP -= amount;

        StartCoroutine(flashdmg());
        if (HP <= 0)
        {
            gameManager.instance.UpdateGameGoal(-1);
            isspawner.HeyIdied();
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
        //enemyPos = shootpos.position - transform.position;
        

        //Quaternion Pangle = Quaternion.LookRotation(enemyPos);
        Quaternion rot = Quaternion.LookRotation(playerDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * playerfacespeed);
        
        
    }

    IEnumerator Shoot()
    {
       enemyPos.Equals(angle);
        Quaternion ang = Quaternion.LookRotation(playerDir, enemyPos);
        isShooting = true;
        Instantiate(bullet, shootpos.position, ang);

        yield return new WaitForSeconds(shootrate);
        isShooting = false;

    }

}
