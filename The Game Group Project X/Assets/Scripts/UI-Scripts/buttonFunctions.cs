using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonFunctions : MonoBehaviour
{
    [SerializeField] GameObject shotDrop;
    [SerializeField] GameObject rifleDrop;
    [SerializeField] GameObject healthDrop;
    [SerializeField] GameObject barDrop;
    
    public void resume()
    {
        gameManager.instance.stateUnpaused();
    }

    public void restart()
    {

        gameManager.instance.playerScript.spawnPlayer();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        gameManager.instance.stateUnpaused();
    }
    public void quit()
    {
        Application.Quit();
    }

    public void playerRespawn()
    {

        gameManager.instance.playerScript.spawnPlayer();
        gameManager.instance.stateUnpaused();
    }
    public void loadLevel(int level)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(level);
    }

    public void buyShot()
    {
        int price = 30;
        if (gameManager.instance.playerScript.coinAmount >= price)
        {
            Instantiate(shotDrop,gameManager.instance.currentShop.shotDrop.transform.position,transform.rotation);
            gameManager.instance.playerScript.coinAmount -= price;
        }
    }
    public void buyRifle()
    {
        int price = 50;
        if (gameManager.instance.playerScript.coinAmount >= price)
        {
            Instantiate(rifleDrop, gameManager.instance.currentShop.rifleDrop.transform.position, transform.rotation);
            gameManager.instance.playerScript.coinAmount -= price;
        }
    }
    public void buyHealth()
    {
        int price = 50;
        if (gameManager.instance.playerScript.coinAmount >= price)
        {
            Instantiate(healthDrop, gameManager.instance.currentShop.healthDrop.transform.position, transform.rotation);
            gameManager.instance.playerScript.coinAmount -= price;
        }
    }
    public void buyBar()
    {
        int price = 30;
        if (gameManager.instance.playerScript.coinAmount >= price)
        {
            Instantiate(barDrop, gameManager.instance.currentShop.barDrop.transform.position, transform.rotation);
            gameManager.instance.playerScript.coinAmount -= price;
        }
    }


}
