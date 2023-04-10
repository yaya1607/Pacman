using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    public GameObject pauseMenu;
    public GameManager gameManager;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        isGamePaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        isGamePaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        isGamePaused = false;
        File.Delete(Application.dataPath + "/SaveGame/Save.txt");
        SceneManager.LoadScene("Pacman");
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        isGamePaused = false;
        SceneManager.LoadScene("MainMenu");
        gameManager.SaveGame();
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
    }

}
