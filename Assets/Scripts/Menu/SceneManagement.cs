using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
//#if UNITY_ANDROID
//        Screen.SetResolution(1080, 2400, true);
//#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToMainMenu()
    {
        PreviousSceneTracker.Instance.prevScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Main Menu");

        if (PreviousSceneTracker.Instance.prevScene != "Options")
        {
            AudioManager.instance.Play("MainMenuTheme");
            AudioManager.instance.StopPlaying("Theme");
        }

        //FindObjectOfType<AudioManager>().StopPlaying("Theme");
        //FindObjectOfType<AudioManager>().Play("MainMenuTheme");
    }
    public void ToInGame()
    {
        PreviousSceneTracker.Instance.prevScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Game");
        AudioManager.instance.Play("Theme");
        AudioManager.instance.StopPlaying("MainMenuTheme");

        Time.timeScale = 1f;

        //FindObjectOfType<AudioManager>().Play("Theme");
        //FindObjectOfType<AudioManager>().StopPlaying("MainMenuTheme");
    }

    public void ToOptions()
    {
        PreviousSceneTracker.Instance.prevScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Options");
    }

    //public void ToCredits()
    //{
    //    SceneManager.LoadScene("Credits");
    //}

    public void ToPrevScene()
    {
        if(SceneManager.GetActiveScene().name == "Options")
        {
            if(SceneManager.GetActiveScene().buildIndex == 0)
            {
                SceneManager.LoadScene("Main Menu");
            }
            else
            {
                SceneManager.LoadScene("Game");
            }
        }

        PreviousSceneTracker.Instance.prevScene = SceneManager.GetActiveScene().name;
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
