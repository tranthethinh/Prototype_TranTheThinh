using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public GameObject pausedPanel; 

    private bool isPaused = false;

    public void TogglePause()
    {
        isPaused = !isPaused;
        
        pausedPanel.SetActive(isPaused);

        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
