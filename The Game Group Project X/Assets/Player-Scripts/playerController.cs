using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour, IDamage
{
    [SerializeField] CharacterController characterController;

    [SerializeField] int HP;
    [SerializeField] float Stamina;
    [SerializeField] float playerSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] int jumpMax;
    [SerializeField] float jumpHeight;
    [SerializeField] float gravityValue;

    [SerializeField] float shootRate;
    [SerializeField] int shootDamage;
    [SerializeField] int shootDist;

    private int HPMax;
    private float StaminaMax;

    private float baseSpeed;
    private bool playerGrounded;
    private Vector3 move;
    private Vector3 playerVelocity;
    private int jumpCount;
    private bool isSprinting;
    private bool isShooting;
    private bool stamCooldown = true;


    void Start()
    {
        baseSpeed = playerSpeed;
        HPMax = HP;
        StaminaMax = Stamina;
        spawnPlayer();
    }

    void Update()
    {
        movement();
        Sprint();
        StartCoroutine(updateStam());
        if (Input.GetButton("Shoot") && !isShooting)
            StartCoroutine(shoot());
    }

    void movement()
    {
        playerGrounded = characterController.isGrounded;
        if (playerGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            jumpCount = 0;

        }





        move = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;
        characterController.Move(move * Time.deltaTime * playerSpeed);


        if (Input.GetButtonDown("Jump") && jumpCount < jumpMax)
        {
            playerVelocity.y = jumpHeight;
            jumpCount++;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }
    void Sprint()
    {
        if (stamCooldown)
        {
            if (Input.GetButtonDown("Sprint"))
            {
                isSprinting = true;
                playerSpeed = sprintSpeed;
            }
            else if (Input.GetButtonUp("Sprint"))
            {
                isSprinting = false;
                playerSpeed = baseSpeed;

            }
        }


    }

    IEnumerator shoot()
    {
        isShooting = true;

        //shoot something

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, shootDist))
        {
            IDamage damageable = hit.collider.GetComponent<IDamage>();

            if (damageable != null)
            {
                damageable.takeDamage(shootDamage);
            }
        }

        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }
    public void takeDamage(int amount)
    {
        HP -= amount;

        updatePlayerUI();
        if (HP <= 0)
        {
            gameManager.instance.youLose();
        }
    }

    IEnumerator updateStam()
    {
        if (Stamina > 10)
        {
            Stamina = 10;
        }
        if (Stamina < 0)
        {
            isSprinting = false;
            playerSpeed = baseSpeed;
            Stamina = 0;
            stamCooldown = false;
            yield return new WaitForSeconds(2);
            stamCooldown = true;
        }
        if (isSprinting)
        {
            yield return new WaitForSeconds(0.1f);
            Stamina = Stamina - 0.1f;
        }
        else
        {
            if (Stamina < StaminaMax && stamCooldown)
            {
                yield return new WaitForSeconds(0.1f);
                Stamina = Stamina + 0.05f;
                if (Stamina > StaminaMax) { Stamina = StaminaMax; }
            }
        }
    }
    public void takeHealth()
    {
        HP += 30;
        if(HP > HPMax)
        {
            HP = HPMax;
        }
    }

    public void spawnPlayer()
    {
        HP = HPMax;
        updatePlayerUI();
        characterController.enabled = false;
        transform.position = gameManager.instance.playerSpawnPos.transform.position;
        characterController.enabled = true;
    }

    public void updatePlayerUI()
    {
        gameManager.instance.playerHPBar.fillAmount = (float)HP / HPMax;
    }
}

