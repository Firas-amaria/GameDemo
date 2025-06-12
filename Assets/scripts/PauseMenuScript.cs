using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    private bool gameIsPaused = false;

    public void Pause()
    {
        Debug.Log("Pause triggered");
        gameIsPaused = true;
        pauseMenu.SetActive(true);

        GameManager.Instance.SetUIState(true); // Stop input
    }

    public void Resume()
    {
        Debug.Log("Resuming");
        gameIsPaused = false;
        pauseMenu.SetActive(false);

        GameManager.Instance.SetUIState(false); // Allow input
    }

    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Options()
    {
        Debug.Log("Options clicked");
    }
}
