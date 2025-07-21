using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Reflection;
using System.Collections.Generic;
using System.IO;

public class BookReaderUI : MonoBehaviour
{
    public static BookReaderUI Instance;

    [Header("Panels & Controls")]
    public GameObject readerPanel;
    public TMP_InputField pageInputField;

    public TMP_Text titleText;

    [Header("Page Navigation Buttons")]
    public Button nextButton;
    public Button prevButton;
    public Button closeButton;

    [Header("Cipher Section")]
    public TMP_InputField cipherInputField;
    public TMP_Dropdown cipherDropdown;
    public TMP_InputField keyInputField;
    public Button cipherButton;
    public TMP_InputField cipherOutputField;

    [Header("Note Save")]
    public Button saveNoteButton;

    private BookDataModel currentBook;
    private int currentPage = 0;
    private List<string> unlockedCiphers = new List<string>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(InitializeAfterDelay());
    }


    private System.Collections.IEnumerator InitializeAfterDelay()
    {
        yield return null; // wait one frame
        yield return new WaitUntil(() => ProgressManager.Instance != null);



        readerPanel.SetActive(false);

        nextButton.onClick.AddListener(NextPage);
        prevButton.onClick.AddListener(PrevPage);
        closeButton.onClick.AddListener(CloseReader);
        cipherButton.onClick.AddListener(ApplyCipher);
        saveNoteButton.onClick.AddListener(SaveDecryptedNote);
    }

    public void OpenBook(BookDataModel book)
    {
        LoadUnlockedCiphers();
        PopulateCipherDropdown();
        currentBook = book;
        currentPage = 0;
        readerPanel.SetActive(true);
        titleText.text = book.title;
        UpdatePage();
    }

    private void UpdatePage()
    {
        if (currentBook == null || currentBook.pages.Count == 0)
        {
            pageInputField.text = "No content available.";
            return;
        }

        pageInputField.text = currentBook.pages[currentPage];
        pageInputField.textComponent.textWrappingMode = TextWrappingModes.Normal;
        pageInputField.textComponent.enableAutoSizing = false;
        pageInputField.textComponent.alignment = TextAlignmentOptions.TopLeft;

        prevButton.interactable = currentPage > 0;
        nextButton.interactable = currentPage < currentBook.pages.Count - 1;
    }

    private void NextPage()
    {
        if (currentBook == null || currentBook.pages == null)
        {
            Debug.LogWarning("No book is loaded. Cannot go to next page.");
            return;
        }

        if (currentPage < currentBook.pages.Count - 1)
        {
            currentPage++;
            UpdatePage();
        }
    }


    private void PrevPage()
    {
        if (currentBook == null || currentBook.pages == null)
        {
            Debug.LogWarning("No book is loaded. Cannot go to next page.");
            return;
        }

        if (currentPage > 0)
        {
            currentPage--;
            UpdatePage();
        }
    }

    private void CloseReader()
    {
        readerPanel.SetActive(false);
    }

    public void SendSelectionToCipher()
    {
        int start = GetPrivateInt(pageInputField, "m_StringPosition");
        int end = GetPrivateInt(pageInputField, "m_StringSelectPosition");

        int realStart = Mathf.Min(start, end);
        int realEnd = Mathf.Max(start, end);
        int length = realEnd - realStart;

        if (length > 0)
        {
            string selectedText = pageInputField.text.Substring(realStart, length);
            cipherInputField.text = selectedText;
            cipherInputField.textComponent.textWrappingMode = TextWrappingModes.Normal;
            cipherInputField.textComponent.enableAutoSizing = false;
            cipherInputField.textComponent.alignment = TextAlignmentOptions.TopLeft;
        }
    }

    private int GetPrivateInt(TMP_InputField inputField, string fieldName)
    {
        FieldInfo field = typeof(TMP_InputField).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
        return field != null ? (int)field.GetValue(inputField) : 0;
    }

    private void ApplyCipher()
    {
        if (CipherProcessor.Instance == null)
        {
            Debug.LogError("CipherProcessor.Instance is null. Did you forget to add the script to the scene?");
            cipherOutputField.text = "Cipher system not ready.";
            return;
        }

        string input = cipherInputField.text;
        string key = keyInputField.text;

        if (string.IsNullOrEmpty(input))
        {
            cipherOutputField.text = "No input.";
            return;
        }

        if (cipherDropdown.value >= unlockedCiphers.Count)
        {
            cipherOutputField.text = "No cipher selected.";
            return;
        }

        string selectedCipher = unlockedCiphers[cipherDropdown.value];
        string result = "";



        switch (selectedCipher)
        {
            case "Caesar":
                if (!int.TryParse(key, out int caesarKey))
                {
                    cipherOutputField.text = "Invalid Caesar key.";
                    return;
                }
                result = CipherProcessor.Instance.Caesar(input, caesarKey);
                break;

            case "Vigenere":
                if (string.IsNullOrWhiteSpace(key))
                {
                    cipherOutputField.text = "Vigenère key is empty.";
                    return;
                }
                result = CipherProcessor.Instance.Vigenere(input, key);
                break;

            default:
                result = "Unsupported cipher.";
                break;
        }

        cipherOutputField.text = result;
        cipherOutputField.textComponent.textWrappingMode = TextWrappingModes.Normal;
        cipherOutputField.textComponent.enableAutoSizing = false;
        cipherOutputField.textComponent.alignment = TextAlignmentOptions.TopLeft;

    }

    private void SaveDecryptedNote()
    {
        string noteText = cipherOutputField.text;

        if (string.IsNullOrWhiteSpace(noteText))
        {
            Debug.Log("Nothing to save.");
            return;
        }

        SavedNotesManager.Instance.SaveNewNote(noteText);
        //Debug.Log("Note saved: " + noteText);
    }

    private void LoadUnlockedCiphers()
    {
        if (ProgressManager.Instance == null || ProgressManager.Instance.data == null)
        {
            Debug.LogWarning("ProgressManager not initialized.");
            return;
        }

        unlockedCiphers = ProgressManager.Instance.GetUnlockedCiphers();

        if (unlockedCiphers == null || unlockedCiphers.Count == 0)
        {
            Debug.LogWarning("No ciphers unlocked or dropdown not populated.");
        }
    }


    private void PopulateCipherDropdown()
    {
        cipherDropdown.ClearOptions();

        if (unlockedCiphers.Count == 0)
        {
            cipherDropdown.options.Add(new TMP_Dropdown.OptionData("No Ciphers Found"));
            cipherDropdown.interactable = false;
        }
        else
        {
            cipherDropdown.AddOptions(unlockedCiphers);
            cipherDropdown.interactable = true;
        }

        cipherDropdown.RefreshShownValue();
    }

   
}
