using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class HintEntry
{
    public string id;
    public bool seen;
}

[System.Serializable]
public class ProgressData
{
    public string currentRoom = "Room 1";
    public List<string> unlockedBooks = new List<string>();
    public List<string> unlockedCiphers = new List<string>();
    public List<HintEntry> interactionHintsShown = new List<HintEntry>();
}

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager Instance;

    public ProgressData data;
    private string filePath;

    private void Awake()
    {

        Instance = this;


        filePath = Path.Combine(Application.streamingAssetsPath, "progress.json");
        //Debug.Log("ProgressManager: Using file path: " + filePath);
        LoadProgress();
    }

    public void LoadProgress()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<ProgressData>(json);

            if (data.interactionHintsShown == null)
            {
                //Debug.LogWarning("[ProgressManager] interactionHintsShown list is NULL — initializing.");
                data.interactionHintsShown = new List<HintEntry>();
            }

            //Debug.Log("[ProgressManager] Progress loaded successfully.");
        }
        else
        {
            Debug.LogWarning("[ProgressManager] No progress file found — creating new.");
            data = new ProgressData();
            SaveProgress();
        }
    }



    public void SaveProgress()
{
    string json = JsonUtility.ToJson(data, true);
    File.WriteAllText(filePath, json);
    //Debug.Log($"[ProgressManager] Saved progress to: {filePath}");
    //Debug.Log($"[ProgressManager] JSON content:\n{json}");
}


    public void SetCurrentRoom(string room)
    {
        data.currentRoom = room;
        SaveProgress();
    }

    public bool HasSeenInteractionHint(string id)
    {
        foreach (var hint in data.interactionHintsShown)
        {
            if (hint.id == id)
                return hint.seen;
        }
        return false;
    }


    public void MarkInteractionHintSeen(string id)
    {
        foreach (var hint in data.interactionHintsShown)
        {
            if (hint.id == id)
            {
                if (!hint.seen)
                {
                    hint.seen = true;
                    //Debug.Log($"[ProgressManager] Marked existing hint '{id}' as seen.");
                    SaveProgress();
                }
                return;
            }
        }

        // If not found, add new
        data.interactionHintsShown.Add(new HintEntry { id = id, seen = true });
        //Debug.Log($"[ProgressManager] Added and marked new hint '{id}' as seen.");
        SaveProgress();
    }



    public void UnlockBook(string slotKey)
    {
        if (!data.unlockedBooks.Contains(slotKey))
        {
            data.unlockedBooks.Add(slotKey);
            SaveProgress();
        }
    }

    public void UnlockCipher(string cipherName)
    {
        if (!data.unlockedCiphers.Contains(cipherName))
        {
            data.unlockedCiphers.Add(cipherName);
            SaveProgress();
        }
    }

    public List<string> GetUnlockedCiphers()
    {
        if (data == null)
        {
            //Debug.LogWarning("ProgressManager: data object is NULL when GetUnlockedCiphers called.");
            return new List<string>();
        }

        if (data.unlockedCiphers == null)
        {
            //Debug.LogWarning("ProgressManager: unlockedCiphers is NULL in GetUnlockedCiphers.");
            return new List<string>();
        }

        return data.unlockedCiphers;
    }
}
