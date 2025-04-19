using UnityEngine;
using UnityEngine.SceneManagement;

public class main_menu_script : MonoBehaviour
{
    public GameObject settings_panel;
    public void start_game()
    {
        SceneManager.LoadScene("Room 1");
    }

    public void exit_game()
    {
        Debug.Log("Exiting game");
        Application.Quit();
    }

    public void open_settings()
    {
        settings_panel.SetActive(true);
    }

    public void close_settings()
    {
        settings_panel.SetActive(false);
    }


}
