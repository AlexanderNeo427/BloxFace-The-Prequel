using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviousSceneTracker : Singleton<PreviousSceneTracker>
{
    [HideInInspector]
    public string prevScene;

    private void Awake()
    {
        DontDestroyOnLoad( this.gameObject );
    }
}
