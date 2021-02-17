using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");

        AudioManager.instance.Play("MainMenuTheme");
        AudioManager.instance.StopPlaying("Theme");

        //FindObjectOfType<AudioManager>().StopPlaying("Theme");
        //FindObjectOfType<AudioManager>().Play("MainMenuTheme");
    }
    public void ToInGame()
    {
        SceneManager.LoadScene("Game");
        AudioManager.instance.Play("Theme");
        AudioManager.instance.StopPlaying("MainMenuTheme");

        //FindObjectOfType<AudioManager>().Play("Theme");
        //FindObjectOfType<AudioManager>().StopPlaying("MainMenuTheme");
    }

    public void ToOptions()
    {
        SceneManager.LoadScene("Options");
    }

    public void ToCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void ToExitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
