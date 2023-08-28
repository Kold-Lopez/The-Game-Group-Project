using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class enemyAIbase : MonoBehaviour, IDamage
{
    [SerializeField] Renderer model;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animator;
    [SerializeField] GameObject gun;
    [SerializeField] GameObject coin;

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
    //private Animator animator;
    public Spawner isspawning;
    public waveSpawner whereISpawnedWave;
    Vector3 angleNew;
    float angle;
    float sub = 30;

    void Start()
    {
        //gameManager.instance.UpdateGameGoal(1);
    }

    void Update()
    {

        if (agent.isActiveAndEnabled)
        {
            if (shootpos != null)
            {


                playerDir = gameManager.instance.player.transform.position - shootpos.position;

                if (gameManager.instance.player.transform.position - shootpos.position != null)
                {
                    angleNew = gameManager.instance.player.transform.position - shootpos.position;
                }


                angle = gameManager.instance.player.transform.position.y - shootpos.position.y;

            }

            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                facePlayer();
                if (!isShooting)
                {
                    animator.SetTrigger("InRange");
                    //StartCoroutine(Shoot());
                }

                animator.SetBool("Moving", false);
            }
            else
            {
                animator.SetBool("Moving", true);
            }

            if (HP > 0)
            {
                agent.SetDestination(gameManager.instance.player.transform.position);
                


            }
        }





    }
    public void takeDamage(int amount)
    {
        HP -= amount;

        StartCoroutine(flashdmg());
        if (HP <= 0)
        {
            //gameManager.instance.UpdateGameGoal(-1);
            //isspawner.HeyIdied();
            animator.SetBool("Move", false);
            animator.SetBool("Moving", false);
            animator.SetBool("IsDead", true);
            
            //Instantiate(coin, angleNew, Quaternion.identity);
            
            GetComponent<CapsuleCollider>().enabled = false;

            gameManager.instance.UpdateGameGoal(-1);
            

            //agent.enabled = false;

            whereISpawnedWave.updateEnemyNumber();

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
        Quaternion ang = Quaternion.LookRotation(playerDir,enemyPos);
        
        isShooting = true;
        Instantiate(bullet, shootpos.position, ang);

        yield return new WaitForSeconds(shootrate);
        isShooting = false;

    }
    /*
    public void createBullet()
    {
        Instantiate(bullet, shootpos.position, transform.rotation);

    }
     */
    public void takeDamagAnim()
    {

        //animator.SetTrigger("InRange");
        shootpos = null;
        Instantiate(coin, transform.position + new Vector3(0, 1), Quaternion.identity);
        agent.enabled = false;

        
        StopAllCoroutines();
        
        Destroy(gameObject);

    }
    public void DestroyGun()
    {
        Destroy(gun);
    }

}
