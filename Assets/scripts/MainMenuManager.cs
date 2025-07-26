using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Button continueButton;

    private string progressFilePath;
    private string savedNotesFilePath;


    private void Start()
    {
        progressFilePath = Path.Combine(Application.streamingAssetsPath, "progress.json");
        savedNotesFilePath = Path.Combine(Application.streamingAssetsPath, "savedNotes.json");

        if (File.Exists(progressFilePath))
        {
            continueButton.interactable = true;
        }
        else
        {
            continueButton.interactable = false;
        }
    }

    public void StartNewGame()
    {
        if (File.Exists(progressFilePath))
        {
            File.Delete(progressFilePath);

            string metaPath = progressFilePath + ".meta";
            if (File.Exists(metaPath))
                File.Delete(metaPath);

            //Debug.Log("[End] Deleted old progress.json and its .meta");
        }

        if (File.Exists(savedNotesFilePath))
        {
            File.Delete(savedNotesFilePath);

            string metaPath = savedNotesFilePath + ".meta";
            if (File.Exists(metaPath))
                File.Delete(metaPath);

            //Debug.Log("[End] Deleted saved notes and its .meta");
        }

        SceneManager.LoadScene("Room 1");
    }

    public void ContinueGame()
    {
        if (!File.Exists(progressFilePath))
        {
            Debug.LogWarning("[MainMenu] No saved game found.");
            return;
        }

        string json = File.ReadAllText(progressFilePath);
        ProgressData progress = JsonUtility.FromJson<ProgressData>(json);
        if (string.IsNullOrEmpty(progress.currentRoom))
        {
            Debug.LogWarning("[MainMenu] Saved file has no currentRoom — defaulting to Room 1.");
            SceneManager.LoadScene("Room 1");
        }
        else
        {
            Debug.Log($"[MainMenu] Loading saved room: {progress.currentRoom}");
            SceneManager.LoadScene(progress.currentRoom);
        }
    }

    public void ExitGame()
    {
        Debug.Log("Exiting game...");
        Application.Quit();
    }
}
