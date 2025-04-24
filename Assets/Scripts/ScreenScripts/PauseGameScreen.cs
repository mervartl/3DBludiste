using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGameScreen : MonoBehaviour
{
    public GameObject pausePanel;
    private bool isPaused;

    void Start()
    {
        isPaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Application.Quit();
            }
            else
            {
                Time.timeScale = 0;
                pausePanel.SetActive(true);
                isPaused = true;
            }
        }

        if (isPaused && Input.GetKeyDown(KeyCode.Return))
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
            isPaused = false;
        }
    }
}
