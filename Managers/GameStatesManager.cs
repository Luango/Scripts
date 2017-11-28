using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatesManager : MonoBehaviour {
    public static bool isGaming;
    private void Awake()
    {
        isGaming = true;
    }

    private void Update()
    {
        if(Input.GetKey("q") || Input.GetKey("e"))
        {
            isGaming = false;
            Time.timeScale = 0.1f;
            // Show switch arrow or secondary weapon bar.
            // Activate the weapon UI
        }
        else if(isGaming == false)
        {
            isGaming = true;
            Time.timeScale = 1f;
        }
    }

    static public void ChangeGameStates()
    {
        isGaming = !isGaming;
        if (isGaming)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;
        }
    }

    // Show weapon selection.

}
