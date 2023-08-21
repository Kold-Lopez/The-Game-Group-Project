using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;

public class bossAI : MonoBehaviour, IDamage
{
    [SerializeField] Renderer model;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animator;

    [Header("-----Stats-----")]

    
    [SerializeField] int HP;
    [SerializeField] int playerfacespeed;


    [Header("-----Gun Phase-----")]
    [SerializeField] float shootRate;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootPosLE;
    [SerializeField] Transform shootPosRE;
    [SerializeField] Transform shootPosRA;
    [SerializeField] Transform shootPosLA;
    [SerializeField] Transform shootPosGut;


    [Header("----Animation-----")]
    [SerializeField] float hitRate;
    [SerializeField] int speed;
    [SerializeField] int animSpeed;

    Vector3 playerDir;
    bool playerInRange;
    private float agentVel;
    private int startingHP;
    private bool isShooting;

    // Start is called before the first frame update
    void Start()
    {
        startingHP = HP;
        gameManager.instance.UpdateGameGoal(1);
        animator.SetBool("Punch", false);
        
    }

    // Update is called once per frame
    void Update()
    {
        //agentVel = 1;
        playerDir = gameManager.instance.player.transform.position - transform.position;
        
        
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            facePlayer();
            if (HP <= 999 && HP > 501)
            {
                StartCoroutine(phase1());
            }
            if(HP <= 500)
            {
                if (!isShooting)
                {
                    StartCoroutine(phase2());
                }
            }
        }
        
        



    }
    void facePlayer()
    {
        Quaternion rot = Quaternion.LookRotation(playerDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * playerfacespeed);
    }
    public void takeDamage(int amount)
    {
        HP -= amount;
        StartCoroutine(flashDamage());
        if (HP <= 0)
        {
            gameManager.instance.UpdateGameGoal(-1);
        }
    }
    IEnumerator flashDamage()
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = Color.white;
    }
    //IEnumerator phases()
    //{
    //    yield return new WaitForSeconds(5);
    //    animator.SetFloat("Speed", Random.value);
    //    yield return new WaitForSeconds(5);
    //}
    IEnumerator phase1()
    {
        animator.SetBool("Punch", true);
        yield return new WaitForSeconds(2);
    }
    IEnumerator phase2()
    {
        //Purpose overwhelm the player
        animator.SetBool("Punch", false);
        isShooting = true;

        Instantiate(bullet, shootPosLE.position, transform.rotation);
        Instantiate(bullet, shootPosRE.position, transform.rotation);
        Instantiate(bullet, shootPosLA.position, transform.rotation);
        Instantiate(bullet, shootPosRA.position, transform.rotation);
        Instantiate(bullet, shootPosGut.position, transform.rotation);
        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }
}
