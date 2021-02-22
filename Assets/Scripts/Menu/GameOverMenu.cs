using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    bool gameEnded = false;

    public float restartDelay = 1f;

    public GameObject GameOverMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if(gameEnded)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void EndGame()
    {
        if (gameEnded == false)
        {
            GameOverMenuUI.SetActive(true);
            gameEnded = true;
            Debug.Log("GAME OVER");
            Invoke("Restart", restartDelay);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Resume()
    {
        GameOverMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameEnded = false;
    }

    void Pause()
    {
        GameOverMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameEnded = true;
    }
}
