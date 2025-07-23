using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class EndSceneManager : MonoBehaviour
{

    private string progressFilePath;
    private string savedNotesFilePath;


    private void Start()
    {
        progressFilePath = Path.Combine(Application.streamingAssetsPath, "progress.json");
        savedNotesFilePath = Path.Combine(Application.streamingAssetsPath, "savedNotes.json");

    }

    public void GoToMainMenu()
    {
        if (File.Exists(progressFilePath))
        {
            File.Delete(progressFilePath);
            File.Delete(savedNotesFilePath);
            Debug.Log("[End] Deleted old progress.json");
        }

        SceneManager.LoadScene("MainMenu");
    }


}
