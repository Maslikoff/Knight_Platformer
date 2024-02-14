using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private bool pauseGame;
    [SerializeField] private GameObject pauseGameMenu;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseGame)
                Resume();
            else
                PauseMenu();
        }
    }

    public void Resume()
    {
        pauseGameMenu.SetActive(false);
        Time.timeScale = 1f;
        pauseGame = false;
    }

    public void PauseMenu()
    {
        pauseGameMenu.SetActive(true);
        Time.timeScale = 0f;
        pauseGame = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
