using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerController : MonoBehaviour, IDamage
{
    [SerializeField] CharacterController characterController;

    [Header("----- PlayerStats -----")]
    

    [SerializeField] int HP;
    [SerializeField] float Stamina;
    [SerializeField] float playerSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] int jumpMax;
    [SerializeField] float jumpHeight;
    [SerializeField] float gravityValue;
    public int coinAmount = 0;

    //[SerializeField] GameObject coins;


    private int HPMax;
    private float StaminaMax;

    private float baseSpeed;
    private bool playerGrounded;
    private Vector3 move;
    private Vector3 playerVelocity;
    private int jumpCount;
    private bool isSprinting;
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
        updatePlayerUI();
        movement();
        Sprint();
        StartCoroutine(updateStam());
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
    public void takeDamage(int amount)
    {
        HP -= amount;
        StartCoroutine(gameManager.instance.damaged());
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
            Stamina = Stamina - 0.03f;
        }
        else
        {
            if (Stamina < StaminaMax && stamCooldown)
            {
                yield return new WaitForSeconds(0.1f);
                Stamina = Stamina + 0.02f;
                if (Stamina > StaminaMax) { Stamina = StaminaMax; }
            }
        }
    }
    public void takeHealth(int amount)
    {
        HP += amount;
        if (HP > HPMax)
        {
            HP = HPMax;
        }
    }

    public void spawnPlayer()
    {
        HP = HPMax;
        updatePlayerUI();
        characterController.enabled = false;
        if (gameManager.instance.level2)
        {
            transform.position = gameManager.instance.level2Spawn.transform.position;
        }
        else
        {
        transform.position = gameManager.instance.playerSpawnPos.transform.position;

        }
        characterController.enabled = true;
    }

    public void updatePlayerUI()
    {
        gameManager.instance.playerHPBar.fillAmount = (float)HP / HPMax;
        gameManager.instance.playerStamBar.fillAmount = (float)Stamina / StaminaMax;
        gameManager.instance.ammoCur.text = GunSystem.currentAmmo.ToString("F0");
        if (GunSystem.currentReserveAmmo > 1000)
        {
            gameManager.instance.ammoMax.text = "INF";
        }
        else
        {
            gameManager.instance.ammoMax.text = GunSystem.currentReserveAmmo.ToString("F0");
        }
        gameManager.instance.coinCount.text = coinAmount.ToString("F0");
    }
    public void coinPickUp(int amount)
    {
        coinAmount += amount;
    }
    public void makeManSpeedy(float speedIncrease, float sprintIncrease, int jumpIncrease)
    {
        playerSpeed += speedIncrease;
        sprintSpeed += sprintIncrease;
        jumpMax += jumpIncrease;
    }


}

