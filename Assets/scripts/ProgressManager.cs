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
        LoadProgress();
    }

    public void LoadProgress()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<ProgressData>(json);
        }
        else
        {
            data = new ProgressData();
            SaveProgress();
        }
    }

    public void SaveProgress()
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
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

   
}
