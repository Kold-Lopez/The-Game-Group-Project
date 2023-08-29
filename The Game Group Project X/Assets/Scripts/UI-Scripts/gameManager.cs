using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;
    public GameObject playerSpawnPos;
    public GameObject level2Spawn;
    public GameObject player;
    public playerController playerScript;
    public GunSystem gunSystem;
    public GameObject coins;

    public GameObject pauseMenu;
    public GameObject activeMenu;
    public GameObject winMenu;
    public GameObject loseMenu;
    public TextMeshProUGUI loseText;
    public GameObject loseRest;
    public GameObject loseRspn;
    public GameObject loseQuit;
    public GameObject shopMenu;
    public LeShop currentShop;
    public TextMeshProUGUI enemiesRemainingTxt;
    public TextMeshProUGUI waveTxt;
    public TextMeshProUGUI ammoCur;
    public TextMeshProUGUI ammoMax;
    public TextMeshProUGUI coinCount;
    public GameObject damageFlash;
    public Image playerHPBar;
    public Image playerStamBar;
    //public audioManager audioManager;

    public bool level2;
    bool isPaused;
    int totalWaves = 10;
    int enemiesRemaining;


    void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<playerController>();
        gunSystem = player.GetComponent<GunSystem>();
        playerSpawnPos = GameObject.FindGameObjectWithTag("Player Spawn Pos");
        level2Spawn = GameObject.FindGameObjectWithTag("Level2 Spawn Pos");
        coins = GameObject.FindGameObjectWithTag("Coins");



    }


    void Update()
    {

        if (Input.GetButtonDown("Cancel") && activeMenu == null)
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
        waveTxt.text = waveManager.instance.waveCurrent.ToString("F0");
        enemiesRemaining += amount;
        enemiesRemainingTxt.text = enemiesRemaining.ToString("F0");

        if (level2)
        {

        }

        //if (enemiesRemaining <= 0 && waveManager.instance.waveCurrent == totalWaves)
        //{
        //    //YouWin();
        //}

    }

    public void YouWin()
    {
        activeMenu = winMenu;
        activeMenu.SetActive(true);
    }
    public void youLose()
    {
        statePaused();
        activeMenu = loseMenu;
        activeMenu.SetActive(true);
        Color deathcolor = loseMenu.GetComponent<Image>().color;
        
        StartCoroutine(deathcolorfade(0));

    }
    public IEnumerator damaged()
    {
        damageFlash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        damageFlash.SetActive(false);
    }
    public void enterShop()
    {
        statePaused();
        activeMenu = shopMenu;
        shopMenu.SetActive(isPaused);
    }
    public Color LerpColor(Color color1, Color color2, float lerpnum)
    {

        color1.r = Mathf.Lerp(color1.r, color2.r, lerpnum);
        color1.g = Mathf.Lerp(color1.g, color2.g, lerpnum);
        color1.b = Mathf.Lerp(color1.b, color2.b, lerpnum);
        color1.a = Mathf.Lerp(color1.a, color2.a, lerpnum);
        

        return color1;
    }
    IEnumerator deathcolorfade( float lerpnum)
    {
        Color color1 = loseMenu.GetComponent<Image>().color;
        Color color2 = loseText.color;
        while (loseMenu.GetComponent<Image>().color.r != 0)
        {
            loseMenu.GetComponent<Image>().color= LerpColor(color1,Color.black,lerpnum);
            loseText.color = LerpColor(color2, Color.white, lerpnum);

            lerpnum = lerpnum + (float)0.01;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        loseQuit.SetActive(true);
        loseRest.SetActive(true);
        loseRspn.SetActive(true);
    }
}
