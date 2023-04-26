using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject otherButtons;
    [SerializeField] private GameObject pauseMenuButtons;

    private bool paused = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
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
        paused = false;
        otherButtons.SetActive(true);
        pauseMenuButtons.SetActive(false);
        Time.timeScale = 1;
    }

    private void Pause()
    {
        paused = true;
        otherButtons.SetActive(false);
        pauseMenuButtons.SetActive(true);
        Time.timeScale = 0;
    }
}