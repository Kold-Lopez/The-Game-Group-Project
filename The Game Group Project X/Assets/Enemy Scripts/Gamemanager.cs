using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    public GameObject Player;
    public playerController Playerscript;

    public GameObject Pausemenu;
    public GameObject activemenu;
    public GameObject Winmenu;
    public TextMeshProUGUI enemiesremainingtxt;

    bool Ispaused;
    int enemiesremaining;

    void Awake()
    {
        instance = this;
        Player = GameObject.FindGameObjectWithTag("Player");
        Playerscript = Player.GetComponent<playerController>();
    }


    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            statePaused();
            activemenu = Pausemenu;
            Pausemenu.SetActive(Ispaused);
        }
    }

    public void statePaused()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Ispaused = !Ispaused;
    }
    public void stateUnpaused()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Ispaused = !Ispaused;
        activemenu.SetActive(false);
        activemenu = null;
    }

    public void UpdateGameGoal(int amount)
    {
        enemiesremaining += amount;
        enemiesremainingtxt.text = enemiesremaining.ToString("F0");

        if (enemiesremaining <= 0)
        {
            YouWin();
        }

    }

    public void YouWin()
    {
        statePaused();
        activemenu = Winmenu;
        activemenu.SetActive(true);
    }
}
