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

    public waveSpawner whereISpawnedWave;
    Vector3 playerDir;
    bool playerInRange;
    private float agentVel;
    //SphereCollider collider;
    //SphereCollider colliderSph;

    // Start is called before the first frame update
    void Start()
    {
        //collider = GetComponent<SphereCollider>();
        //colliderSph = GetComponent<SphereCollider>();
        //animator.SetBool("IsDead", false);


        //gameManager.instance.UpdateGameGoal(1);

    }

    // Update is called once per frame
    void Update()
    {
        if (agent.isActiveAndEnabled)
        {
            agentVel = agent.velocity.normalized.magnitude;
            animator.SetFloat("Speed", Mathf.Lerp(animator.GetFloat("Speed"), agentVel, Time.deltaTime * animSpeed));
            playerDir = gameManager.instance.player.transform.position - transform.position;

            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                facePlayer();


            }
            if (Hp > 0)
            {
                agent.SetDestination(gameManager.instance.player.transform.position);
            }

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
        
        if (Hp <= 0)
        {
            animator.SetBool("IsDead", true);
            //StartCoroutine(takeDamagAnim());
            gameManager.instance.UpdateGameGoal(-1);
            GetComponent<CapsuleCollider>().enabled = false;

            whereISpawnedWave.updateEnemyNumber();
            //Instantiate(coin, transform.position, transform.rotation);

        }
        else
        {
            StartCoroutine(flashDamage());
        }
    }

    IEnumerator flashDamage()
    {
        // gameManager.instance.audioManager.PlaySound(gameManager.instance.audioManager.hitClip);
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = Color.white;
    }
    public void takeDamagAnim()
    {
        
        agent.enabled = false;
        Instantiate(coin, transform.position + new Vector3(0, 1), Quaternion.identity);
        StopAllCoroutines();
        //Instantiate(coin, transform);
        //colliderSph.enabled = false;
        //animator.SetFloat("Speed", Mathf.Lerp(animator.GetFloat("Speed"), agentVel*2, Time.deltaTime * animSpeed));
        Destroy(gameObject);
    }

}
