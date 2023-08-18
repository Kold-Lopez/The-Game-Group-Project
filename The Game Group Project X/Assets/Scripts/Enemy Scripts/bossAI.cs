using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] float shootrate;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootpos;

    [Header("----Animation-----")]
    [SerializeField] float hitRate;
    [SerializeField] int speed;
    [SerializeField] int animSpeed;

    Vector3 playerDir;
    bool playerInRange;
    private float agentVel;
    private int startingHP;

    // Start is called before the first frame update
    void Start()
    {
        startingHP = HP;
        gameManager.instance.UpdateGameGoal(1);
    }

    // Update is called once per frame
    void Update()
    {
        playerDir = gameManager.instance.player.transform.position - transform.position;

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            facePlayer();
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
    public void phaseDecider()
    {
        if(startingHP * 0.75 <= HP)
        {

        }
    }
}
