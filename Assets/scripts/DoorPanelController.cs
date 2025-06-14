using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DoorPanelController : MonoBehaviour
{
    [Header("UI References")]
    public TMP_InputField passwordInput;
    public Transform notesContentParent; // The container inside the scroll view
    public GameObject noteItemPrefab;    // A prefab for showing each note

    private DoorInteractable linkedDoor;

    void OnEnable()
    {
        DisplayNotes();
    }

    public void SetLinkedDoor(DoorInteractable door)
    {
        linkedDoor = door;
    }

    public void SubmitPassword()
    {
        if (linkedDoor == null) return;

        if (passwordInput.text == linkedDoor.correctPassword)
        {
            linkedDoor.isSolved = true;
            GameManager.Instance?.SetUIState(false);
            SceneManager.LoadScene(linkedDoor.sceneToLoad);
        }
        else
        {
            Debug.Log("Incorrect password.");
        }
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
        GameManager.Instance?.SetUIState(false);
    }

    private void DisplayNotes()
    {
        // Clear previous notes
        foreach (Transform child in notesContentParent)
        {
            Destroy(child.gameObject);
        }

        var notes = SavedNotesManager.Instance.GetAllNotes();
        foreach (string note in notes)
        {
            GameObject noteGO = Instantiate(noteItemPrefab, notesContentParent);
            TMP_Text text = noteGO.GetComponentInChildren<TMP_Text>();
            if (text != null)
                text.text = note;
        }
    }
}
