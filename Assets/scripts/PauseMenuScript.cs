using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public void Pause()
    {
        Debug.Log("pause?0");
        pauseMenu.SetActive(true);
    }

    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);

    }

    public void Options()
    {
        Debug.Log("wops");
    }
}
