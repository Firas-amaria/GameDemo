using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class SavedNotesUI : MonoBehaviour
{
    public GameObject notePrefab;
    public Transform notesContainer;
    public GameObject panelToHide; // Main Inventory Panel

    public void ShowNotes()
    {
        gameObject.SetActive(true);
        panelToHide.SetActive(false);

        // Clear previous
        foreach (Transform child in notesContainer)
            Destroy(child.gameObject);

        List<string> notes = SavedNotesManager.Instance.GetAllNotes();

        for (int i = 0; i < notes.Count; i++)
        {
            int index = i; // Capture for lambda
            GameObject noteGO = Instantiate(notePrefab, notesContainer);
            TMP_Text text = noteGO.GetComponentInChildren<TMP_Text>();
            text.text = notes[i];

            Button deleteBtn = noteGO.GetComponentInChildren<Button>();
            deleteBtn.onClick.AddListener(() =>
            {
                SavedNotesManager.Instance.DeleteNoteAt(index);
                ShowNotes(); // Refresh list
            });
        }
    }

    public void BackToInventory()
    {
        gameObject.SetActive(false);
        panelToHide.SetActive(true);
    }
}
