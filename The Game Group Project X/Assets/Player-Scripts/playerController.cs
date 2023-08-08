using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField] CharacterController characterController;

    [SerializeField] int HP;
    [SerializeField] int Stamina;
    [SerializeField] float playerSpeed;
    [SerializeField] float sprintMod;
    [SerializeField] int jumpMax;
    [SerializeField] float jumpHeight;
    [SerializeField] float gravityValue;

    [SerializeField] float shootRate;
    [SerializeField] int shootDamage;
    [SerializeField] int shootDist;

    private int HPMax;
    private int StaminaMax;

    private bool playerGrounded;
    private Vector3 move;
    private Vector3 playerVelocity;
    private int jumpCount;
    private bool isSprinting;
    private bool isShooting;


    void Start()
    {
        HPMax = HP;
        StaminaMax = Stamina;
    }

    void Update()
    {
        movement();
        Sprint();
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
        if (Input.GetButtonDown("Sprint"))
        {
            isSprinting = true;
            playerSpeed *= sprintMod;

        }
        else if (Input.GetButtonUp("Sprint"))
        {
            isSprinting = false;
            playerSpeed /= sprintMod;

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
    }
}
