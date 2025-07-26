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
    public bool disableFileIO = false; // <-- Add this

    private void Awake()
    {
        Instance = this;
        filePath = Path.Combine(Application.streamingAssetsPath, "progress.json");
        LoadProgress();
    }

    public void LoadProgress()
    {
        if (disableFileIO)
        {
            data = new ProgressData();
            return;
        }

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<ProgressData>(json);

            if (data.interactionHintsShown == null)
            {
                data.interactionHintsShown = new List<HintEntry>();
            }
        }
        else
        {
            data = new ProgressData();
            SaveProgress();
        }
    }



    public void SaveProgress()
    {
        if (disableFileIO) return;
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
                    SaveProgress();
                }
                return;
            }
        }

        data.interactionHintsShown.Add(new HintEntry { id = id, seen = true });
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
            return new List<string>();
        }

        if (data.unlockedCiphers == null)
        {
            return new List<string>();
        }

        return data.unlockedCiphers;
    }
}
