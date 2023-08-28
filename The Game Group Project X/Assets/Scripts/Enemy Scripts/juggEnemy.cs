using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class juggEnemy : MonoBehaviour, IDamage//, IKnockback
{


    [SerializeField] Renderer model;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animator;

    [SerializeField] int playerFaceSpeed;

    [SerializeField] int Hp;
    [SerializeField] float hitRate;
    [SerializeField] int speed;
    [SerializeField] int knockbackDist;
    private Animator runAnimation;

    public waveSpawner whereISpawnedWave;
    Vector3 playerDir;
    bool playerInRange;



    // Start is called before the first frame update
    void Start()
    {
        runAnimation = GetComponent<Animator>();


        // gameManager.instance.UpdateGameGoal(1);

    }

    // Update is called once per frame
    void Update()
    {
        playerDir = gameManager.instance.player.transform.position - transform.position;

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            facePlayer();


            runAnimation.SetBool("attackRange", true);
        }
        else
            runAnimation.SetBool("attackRange", false);



        agent.SetDestination(gameManager.instance.player.transform.position);
        if (playerInRange)
        {
            knockback();
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
            gameManager.instance.UpdateGameGoal(-1);
            GetComponent<CapsuleCollider>().enabled = false;
            whereISpawnedWave.updateEnemyNumber();
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(flashDamage());
        }
    }

    IEnumerator flashDamage()
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = Color.cyan;
    }
    public void takeDamagAnim()
    {

        agent.enabled = false;
        StopAllCoroutines();
        //colliderSph.enabled = false;
        //animator.SetFloat("Speed", Mathf.Lerp(animator.GetFloat("Speed"), agentVel*2, Time.deltaTime * animSpeed));
        Destroy(gameObject);
    }

    void knockback()
    {

        gameManager.instance.player.transform.position += transform.forward * Time.deltaTime * knockbackDist;


    }

}
