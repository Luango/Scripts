using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicGameManager : MonoBehaviour {
    private static MusicGameManager instance = null;

    public static MusicGameManager Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;
        AndroidNativeAudio.makePool(150);
        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start () {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
}
