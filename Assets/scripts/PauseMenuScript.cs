using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public Boolean gameIsPaused =false;
    public void Pause()
    {
        Debug.Log("pause?0");
        gameIsPaused = true;
        pauseMenu.SetActive(true);
        playerMovement.isPaused = true; // <--- Stop player
    }

    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        gameIsPaused = false;
        playerMovement.isPaused = false; // <--- Resume player

    }

    public void Options()
    {
        Debug.Log("wops");
    }
}
