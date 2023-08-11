using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class meleeEnemy : MonoBehaviour, IDamage
{

    [SerializeField] Renderer model;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animator;

    [SerializeField] int playerFaceSpeed;

    [SerializeField] int Hp;
    [SerializeField] float hitRate;
    [SerializeField] int speed;
    [SerializeField] int animSpeed;

    Vector3 playerDir;
    bool playerInRange;



    // Start is called before the first frame update
    void Start()
    {
        
        gameManager.instance.UpdateGameGoal(1);
    }

    // Update is called once per frame
    void Update()
    {
        float agentVel = agent.velocity.normalized.magnitude;
        animator.SetFloat("Speed", Mathf.Lerp(animator.GetFloat("Speed"), agentVel, Time.deltaTime * animSpeed));
        playerDir = gameManager.instance.player.transform.position - transform.position;

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            facePlayer();
            

        }
        
        



        agent.SetDestination(gameManager.instance.player.transform.position);

    }

    void facePlayer()
    {
        Quaternion rot = Quaternion.LookRotation(playerDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * playerFaceSpeed);
    }

    public void takeDamage(int amount)
    {
        Hp -= amount;
        StartCoroutine(flashDamage());
        if (Hp <= 0)
        {
            gameManager.instance.UpdateGameGoal(-1);
            Destroy(gameObject);
        }
    }

    IEnumerator flashDamage()
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = Color.white;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        playerInRange = true;
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        playerInRange = false;
    //    }
    //}
}
