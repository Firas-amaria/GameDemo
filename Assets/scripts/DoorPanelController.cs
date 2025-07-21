using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 
using System.Collections; 

public class DoorPanelController : MonoBehaviour
{
    [Header("UI References")]
    public TMP_InputField passwordInput;
    public Transform notesContentParent; // The container inside the scroll view
    public GameObject noteItemPrefab;    // A prefab for showing each note

    private DoorInteractable linkedDoor;
    private Coroutine flashCoroutine;

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

            // ✅ Save the scene that will be loaded as the new currentRoom
            if (ProgressManager.Instance != null)
            {
                ProgressManager.Instance.SetCurrentRoom(linkedDoor.sceneToLoad);
            }

            SceneManager.LoadScene(linkedDoor.sceneToLoad);
        }
        else
        {
            passwordInput.text = "";
            if (flashCoroutine != null)
                StopCoroutine(flashCoroutine);
            flashCoroutine = StartCoroutine(FlashInputFieldRed());
        }
    }

    private IEnumerator FlashInputFieldRed()
    {
        Image bg = passwordInput.GetComponent<Image>();
        if (bg != null)
        {
            Color originalColor = bg.color;
            bg.color = Color.red;
            yield return new WaitForSeconds(0.4f);
            bg.color = originalColor;
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
