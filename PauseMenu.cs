using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject gameOver;
    public GameObject gameWon;
    public static bool isPaused;//ha valami meg mozog pause utan ez kell !!!!!!
    public static bool isDead;
    public static bool isWon;
    void Start()
    {
        pauseMenu.SetActive(false);
        gameOver.SetActive(false);
        gameWon.SetActive(false);

        isDead = false;
        isPaused = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            Dead();
        }
        if (isWon)
        {
            Game_Won();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        
    }
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true; 
    }
    public void Dead()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused=false;
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    public void Game_Won()
    {
        gameWon.SetActive(true);
        Time.timeScale = 0f;
    }
}
