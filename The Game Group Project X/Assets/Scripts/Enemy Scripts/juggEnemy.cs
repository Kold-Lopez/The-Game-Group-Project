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
            whereISpawnedWave.updateEnemyNumber();
            Destroy(gameObject);
        }
    }

    IEnumerator flashDamage()
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = Color.cyan;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
    //void knockback(Vector3 dist)
    //{
    //    if (playerInRange)
    //    {
    //        gameManager.instance.player.transform.position -= dist;
    //    }

    //}

}
