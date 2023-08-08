using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUNtemp : MonoBehaviour
{
    [SerializeField] GameObject GUN;

    [SerializeField] float shootRate;
    [SerializeField] int shootDmg;
    [SerializeField] float shootRange;

    private bool gunActive;
    private bool isShooting;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            gunActive = !gunActive;
            GUN.SetActive(gunActive);
        }
        if (Input.GetButton("Shoot") && !isShooting && gunActive)
        {
            StartCoroutine(shoot());
        }
    }
    IEnumerator shoot()
    {
        isShooting = true;

        //shoot something
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, shootRange))
        {
            //wall example
            //Instantiate(wall, hit.point, transform.rotation);
            IDamage damageable = hit.collider.GetComponent<IDamage>();

            if (damageable != null)
            {
                damageable.takeDamage(shootDmg);
            }
        }

        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }
}
