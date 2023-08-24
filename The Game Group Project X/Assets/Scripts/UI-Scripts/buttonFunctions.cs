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
    [SerializeField] GameObject shopCart;
    
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
        int price = 0;
        if (gameManager.instance.playerScript.coinAmount >= price)
        {
            Instantiate(shotDrop,shopCart.transform.position,transform.rotation);
            gameManager.instance.playerScript.coinAmount -= price;
        }
    }
    public void buyRifle()
    {
        int price = 0;
        if (gameManager.instance.playerScript.coinAmount >= price)
        {
            Instantiate(rifleDrop, shopCart.transform.position, transform.rotation);
            gameManager.instance.playerScript.coinAmount -= price;
        }
    }
    public void buyHealth()
    {
        int price = 0;
        if (gameManager.instance.playerScript.coinAmount >= price)
        {
            Instantiate(healthDrop, shopCart.transform.position, transform.rotation);
            gameManager.instance.playerScript.coinAmount -= price;
        }
    }
    public void buyBar()
    {
        int price = 0;
        if (gameManager.instance.playerScript.coinAmount >= price)
        {
            Instantiate(barDrop, shopCart.transform.position, transform.rotation);
            gameManager.instance.playerScript.coinAmount -= price;
        }
    }


}
