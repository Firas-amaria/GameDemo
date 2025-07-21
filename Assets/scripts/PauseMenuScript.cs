using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject inventoryButton;

    public void Pause()
    {
        //Debug.Log("Pause triggered");
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        inventoryButton.SetActive(false);

        GameManager.Instance.SetUIState(true); // Stop input
    }

    public void Resume()
    {
        //Debug.Log("Resuming");
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        inventoryButton.SetActive(true);
        GameManager.Instance.SetUIState(false); // Allow input
    }

    public void Home()
    {
        Destroy(GameManager.Instance.gameObject);
        SceneManager.LoadScene("MainMenu");
    }
}


