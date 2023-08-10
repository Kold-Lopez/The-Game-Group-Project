using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerModelController : MonoBehaviour
{
    [SerializeField] CharacterController controller;

    [SerializeField] int HP;
    [SerializeField] float playerSpeed;
    [SerializeField] float sprintMod;
    [SerializeField] int jumpMax;
    [SerializeField] float jumpHeight;
    [SerializeField] float gravityValue;

    [SerializeField] float shootRate;
    [SerializeField] int shootDamage;
    [SerializeField] int shootDist;




    private bool groundedPlayer;
    private Vector3 move;
    private Vector3 playerVelocity;
    private int jumpCount;
    private bool isSprinting;
    private bool isShooting;
    private Animator runAnimation;
    private Animator shootAnimation;


    // Start is called before the first frame update
    void Start()
    {
        runAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        Sprint();
        //if (Input.GetButton("Shoot") && !isShooting)
        //    StartCoroutine(shoot());

    }

    void movement()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {

            playerVelocity.y = 0f;
            jumpCount = 0;
        }


        move = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;
        controller.Move(move * Time.deltaTime * playerSpeed);



        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && jumpCount < jumpMax)
        {
            playerVelocity.y = jumpHeight;
            jumpCount++;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (move != Vector3.zero)
        {
            runAnimation.SetBool("Run", true);
        }
        else
        {
            runAnimation.SetBool("Run", false);
        }
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
        // shootAnimation.SetBool("Shoot", true);

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
        //shootAnimation.SetBool("Shoot", false);
    }
    public void takeDamage(int amount)
    {
        HP -= amount;
    }
}
