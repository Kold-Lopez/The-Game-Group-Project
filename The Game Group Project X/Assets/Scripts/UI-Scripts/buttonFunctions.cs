using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonFunctions : MonoBehaviour
{
    
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

    
}
