using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    public GameObject player;
    public playerController playerScript;

    public GameObject pauseMenu;
    public GameObject activeMenu;
    public GameObject winMenu;
    public TextMeshProUGUI enemiesRemainingTxt;

    bool isPaused;
    int enemiesRemaining;

    private void Start()
    {
        
    }
    void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<playerController>();
    }


    void Update()
    {
        if (Input.GetButtonDown("Cancel")&&activeMenu==null)
        {
            statePaused();
            activeMenu = pauseMenu;
            pauseMenu.SetActive(isPaused);
        }
    }

    public void statePaused()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        isPaused = !isPaused;
    }
    public void stateUnpaused()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = !isPaused;
        activeMenu.SetActive(false);
        activeMenu = null;
    }

    public void UpdateGameGoal(int amount)
    {
        enemiesRemaining += amount;
        enemiesRemainingTxt.text = enemiesRemaining.ToString("F0");

        if (enemiesRemaining <= 0)
        {
            YouWin();
        }

    }

    public void YouWin()
    {
        statePaused();
        activeMenu = winMenu;
        activeMenu.SetActive(true);
    }
}
