using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;
    public GameObject playerSpawnPos;
    public GameObject player;
    public playerController playerScript;
    public GunSystem gunSystem;
    public GameObject coins;

    public GameObject pauseMenu;
    public GameObject activeMenu;
    public GameObject winMenu;
    public GameObject loseMenu;
    public GameObject loadScreen;
    public TextMeshProUGUI enemiesRemainingTxt;
    public GameObject damageFlash;
    public Image playerHPBar;
    public Image playerStamBar;

    bool isPaused;
    int enemiesRemaining;

    
    void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<playerController>();
        gunSystem = player.GetComponent<GunSystem>();
        playerSpawnPos = GameObject.FindGameObjectWithTag("Player Spawn Pos");
        coins = GameObject.FindGameObjectWithTag("Coins");
        StartCoroutine(load());
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
    public void youLose()
    {
        statePaused();
        activeMenu = loseMenu;
        activeMenu.SetActive(true);
    }
    IEnumerator load()
    {
        loadScreen.SetActive(true);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1);
        loadScreen.SetActive(false);
        Time.timeScale = 1;
    }
    public IEnumerator damaged()
    {
        damageFlash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        damageFlash.SetActive(false);
    }
   
}
