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

        SceneManager.LoadScene("MainMenu");
    }


}
