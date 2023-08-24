using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
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

    UnityEngine.Vector3 playerDir;
    bool playerInRange;
    private float agentVel;
    private int startingHP;
    private bool isShooting;
    private float aim1;
    private float aim2;
    UnityEngine.Vector3 eyebeam;
    UnityEngine.Vector3 eyebeam2;
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
        agentVel = 0;
        playerDir = gameManager.instance.player.transform.position - transform.position;
        animator.SetFloat("Speed", Mathf.Lerp(animator.GetFloat("Speed"), agentVel, Time.deltaTime * animSpeed));
        aim1 = gameManager.instance.player.transform.position.y - shootPosLE.position.y;
        aim2 = gameManager.instance.player.transform.position.y - shootPosRE.position.y;

        if (agent.isActiveAndEnabled)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                facePlayer();
                if (HP <= 999 && HP > 701)
                {
                    StartCoroutine(phase1());
                }
                else if (HP <= 700 && HP > 501)
                {
                    if (!isShooting)
                    {
                        StartCoroutine(phase2());
                    }
                }
                else if (HP <= 500 && HP > 1)
                {
                    phase3();
                }

                //DO NOT UNCOMMENT IT WILL CRASH UNITY
                //else if (HP <= 250 && HP > 1)
                //{
                //    StartCoroutine(phase2());
                //    phase3();
                //}
            }
        }
        
        



    }
    void facePlayer()
    {
        UnityEngine.Quaternion rot = UnityEngine.Quaternion.LookRotation(playerDir);
        transform.rotation = UnityEngine.Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * playerfacespeed);
    }
    public void takeDamage(int amount)
    {
        HP -= amount;
        StartCoroutine(flashDamage());
        if (HP <= 0)
        {
            //gameManager.instance.UpdateGameGoal(-1);
            animator.SetBool("Phase3", false);
            animator.SetBool("IsDead", true);  
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

        eyebeam.Equals(aim1);
        eyebeam2.Equals(aim2);

        UnityEngine.Quaternion ang = UnityEngine.Quaternion.LookRotation(playerDir, eyebeam);
        UnityEngine.Quaternion ang2 = UnityEngine.Quaternion.LookRotation(playerDir, eyebeam2);
        Instantiate(bullet, shootPosLE.position, ang);
        Instantiate(bullet, shootPosRE.position, ang2);
        Instantiate(bullet, shootPosLA.position, transform.rotation);
        Instantiate(bullet, shootPosRA.position, transform.rotation);
        Instantiate(bullet, shootPosGut.position, transform.rotation);
        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }
    private void phase3()
    {
        animator.SetBool("Punch", false);
        animator.SetBool("Phase3", true);

    }
    public void Death()
    {
        agent.enabled = false;
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
