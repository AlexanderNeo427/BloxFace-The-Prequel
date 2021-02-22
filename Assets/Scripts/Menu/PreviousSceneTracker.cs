using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreviousSceneTracker : Singleton<PreviousSceneTracker>
{
    [HideInInspector]
    public string prevScene;

    private static string lastLevel;

    public static void SetLastLevel(string level)
    {
        lastLevel = level;
    }

    public static string GetLastLevel()
    {
        return lastLevel;
    }

    public static void ChangeToPrevLvl()
    {
        SceneManager.LoadScene(lastLevel);
    }

    private void Awake()
    {
        DontDestroyOnLoad( this.gameObject );
    }
}
