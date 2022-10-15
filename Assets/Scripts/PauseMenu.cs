using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : Menu
{
    private static bool GameIsPaused = false;

    protected override void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                GameIsPaused = false;
                Resume();
            }
            else
            {
                GameIsPaused = true;
                Pause();
            }
        }
    }
}
