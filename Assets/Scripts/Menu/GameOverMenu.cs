using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    bool gameEnded = false;

    //public float restartDelay = 1f;

    public GameObject GameOverMenuUI;
    public GameObject WaveCanvasUI;
    public GameObject PauseButtonUI;
    public GameObject InventoryUI;
    public GameObject BloodScreenUI;

    private void OnEnable()
    {
        PlayerInfo.OnDeath += EndGame;
    }

    private void OnDisable()
    {
        PlayerInfo.OnDeath -= EndGame;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    if(gameEnded)
        //    {
        //        Resume();
        //    }
        //    else
        //    {
        //        Pause();
        //    }
        //}
    }

    public void EndGame()
    {
        AudioManager.instance.Play("Dying");

        if (gameEnded == false)
        {
            GameOverMenuUI.SetActive(true);
            WaveCanvasUI.SetActive(false);
            PauseButtonUI.SetActive(false);
            InventoryUI.SetActive(false);
            BloodScreenUI.SetActive(false);
            Time.timeScale = 0f;
            gameEnded = true;
            Debug.Log("GAME OVER");
            //Invoke("Restart", restartDelay);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
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
