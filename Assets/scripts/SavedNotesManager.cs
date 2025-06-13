using UnityEngine;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public class SavedNoteDatabase
{
    public List<string> notes = new List<string>();
}

public class SavedNotesManager : MonoBehaviour
{
    public static SavedNotesManager Instance;

    private string filePath;
    public SavedNoteDatabase noteDatabase;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //Debug.Log("SavedNotesManager ready");
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        filePath = Path.Combine(Application.streamingAssetsPath, "saved_notes.json");
        LoadNotes();
    }

    private void LoadNotes()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            noteDatabase = JsonUtility.FromJson<SavedNoteDatabase>(json);
        }
        else
        {
            noteDatabase = new SavedNoteDatabase();
        }
    }

    public void SaveNewNote(string newNote)
    {

        if (string.IsNullOrWhiteSpace(newNote)) return;

        if (noteDatabase == null)
        {
            Debug.LogError("noteDatabase is null!");
            return;
        }


        noteDatabase.notes.Add(newNote);
        WriteToFile();
    }

    public void DeleteNoteAt(int index)
    {
        if (index >= 0 && index < noteDatabase.notes.Count)
        {
            noteDatabase.notes.RemoveAt(index);
            WriteToFile();
        }
    }

    public List<string> GetAllNotes()
    {
        return noteDatabase.notes;
    }

    private void WriteToFile()
    {
        string json = JsonUtility.ToJson(noteDatabase, true);
        File.WriteAllText(filePath, json);
    }
}
