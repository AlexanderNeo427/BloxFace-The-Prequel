using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject WaveCanvasUI;
    public GameObject InventoryUI;
    public GameObject BloodScreenUI;
    public GameObject SettingsUI;
    public GameObject PopUpTextUI;
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

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        WaveCanvasUI.SetActive(true);
        BloodScreenUI.SetActive(true);
        SettingsUI.SetActive(false);
        PauseButtonUI.SetActive(true);
        InventoryUI.SetActive(true);
        PopUpTextUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        WaveCanvasUI.SetActive(false);
        BloodScreenUI.SetActive(false);
        SettingsUI.SetActive(false);
        InventoryUI.SetActive(false);
        PauseButtonUI.SetActive(false);
        PopUpTextUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
