using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class ProgressData
{
    public string currentRoom = "room1";
    public List<string> unlockedBooks = new List<string>();
    public List<string> unlockedCiphers = new List<string>();
    public Dictionary<string, bool> interactionHintsShown = new Dictionary<string, bool>();
}

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager Instance;

    public ProgressData data;
    private string filePath;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        filePath = Path.Combine(Application.streamingAssetsPath, "progress.json");
        //Debug.Log("ProgressManager: Using file path: " + filePath);
        LoadProgress();
    }

    public void LoadProgress()
    {
        if (File.Exists(filePath))
        {
            //Debug.Log("ProgressManager: File found. Loading progress.");
            string json = File.ReadAllText(filePath);
            //Debug.Log("ProgressManager: JSON content:\n" + json);
            data = JsonUtility.FromJson<ProgressData>(json);

            if (data.unlockedCiphers == null)
                Debug.LogWarning("ProgressManager: unlockedCiphers list is NULL after load!");
            else if (data.unlockedCiphers.Count == 0)
                Debug.LogWarning("ProgressManager: unlockedCiphers list is EMPTY.");
            //else
            //    Debug.Log("ProgressManager: unlockedCiphers contains: " + string.Join(", ", data.unlockedCiphers));
        }
        else
        {
            //Debug.LogWarning("ProgressManager: File NOT FOUND! Creating new one.");
            data = new ProgressData();
            SaveProgress();
        }
    }

    public void SaveProgress()
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
        //Debug.Log("ProgressManager: Saved progress to file.");
    }

    public void SetCurrentRoom(string room)
    {
        data.currentRoom = room;
        SaveProgress();
    }

    public bool HasSeenInteractionHint(string id)
    {
        return data.interactionHintsShown.ContainsKey(id) && data.interactionHintsShown[id];
    }

    public void MarkInteractionHintSeen(string id)
    {
        if (!data.interactionHintsShown.ContainsKey(id))
        {
            data.interactionHintsShown[id] = true;
            SaveProgress();
        }
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
