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
    [SerializeField] GameObject coin;

    Vector3 playerDir;
    bool playerInRange;
    private float agentVel;
    Collider collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        animator.SetBool("IsDead", false);
        gameManager.instance.UpdateGameGoal(1);
    }

    // Update is called once per frame
    void Update()
    {
        agentVel = agent.velocity.normalized.magnitude;
        animator.SetFloat("Speed", Mathf.Lerp(animator.GetFloat("Speed"), agentVel, Time.deltaTime * animSpeed));
        playerDir = gameManager.instance.player.transform.position - transform.position;

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            facePlayer();


        }
        if(Hp > 0)
        {
            agent.SetDestination(gameManager.instance.player.transform.position);
        }
        

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
            StartCoroutine(takeDamagAnim());
            gameManager.instance.UpdateGameGoal(-1);
            Instantiate(coin, transform.position, transform.rotation);

        }
    }

    IEnumerator flashDamage()
    {
        gameManager.instance.audioManager.PlaySound(gameManager.instance.audioManager.hitClip);
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = Color.white;
    }
    IEnumerator takeDamagAnim()
    {
        animator.SetBool("IsDead", true);
        collider.enabled = false;
        //animator.SetFloat("Speed", Mathf.Lerp(animator.GetFloat("Speed"), agentVel*2, Time.deltaTime * animSpeed));
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
