using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject WaveCanvasUI;
    public GameObject InventoryUI;
    public GameObject BloodScreenUI;

    public GameObject PauseButtonUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    //private void Awake()
    //{
    //    if (Application.isMobilePlatform)
    //    {
    //        PauseButtonUI.SetActive(true);
    //    }
    //    else if (!Application.isMobilePlatform)
    //    {
    //        PauseButtonUI.SetActive(false);
    //    }
    //}

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        WaveCanvasUI.SetActive(true);
        InventoryUI.SetActive(true);
        BloodScreenUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        WaveCanvasUI.SetActive(false);
        InventoryUI.SetActive(false);
        BloodScreenUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
