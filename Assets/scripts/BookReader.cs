using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Reflection;

public class BookReader : MonoBehaviour
{
    public static BookReader Instance;

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

    private BookDataModel currentBook;
    private int currentPage = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        readerPanel.SetActive(false);

        nextButton.onClick.AddListener(NextPage);
        prevButton.onClick.AddListener(PrevPage);
        closeButton.onClick.AddListener(CloseReader);
        cipherButton.onClick.AddListener(ApplyCipher);
    }

    public void OpenBook(BookDataModel book)
    {
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

        prevButton.interactable = currentPage > 0;
        nextButton.interactable = currentPage < currentBook.pages.Count - 1;
    }

    private void NextPage()
    {
        if (currentPage < currentBook.pages.Count - 1)
        {
            currentPage++;
            UpdatePage();
        }
    }

    private void PrevPage()
    {
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
        }
    }

    private int GetPrivateInt(TMP_InputField inputField, string fieldName)
    {
        FieldInfo field = typeof(TMP_InputField).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
        return field != null ? (int)field.GetValue(inputField) : 0;
    }

    private void ApplyCipher()
    {
        string method = cipherDropdown.options[cipherDropdown.value].text;
        string input = cipherInputField.text;

        if (string.IsNullOrEmpty(input))
        {
            cipherOutputField.text = "No input.";
            return;
        }

        int key;
        if (!int.TryParse(keyInputField.text, out key))
        {
            cipherOutputField.text = "Invalid key.";
            return;
        }

        if (method == "Caesar")
        {
            cipherOutputField.text = CaesarCipher(input, key);
        }
        else
        {
            cipherOutputField.text = "Unsupported cipher.";
        }
    }

    private string CaesarCipher(string text, int key)
    {
        char Shift(char c, int shift)
        {
            if (char.IsLetter(c))
            {
                char offset = char.IsUpper(c) ? 'A' : 'a';
                return (char)(((c - offset + shift + 26) % 26) + offset);
            }
            return c;
        }

        char[] result = new char[text.Length];
        for (int i = 0; i < text.Length; i++)
        {
            result[i] = Shift(text[i], key);
        }
        return new string(result);
    }
}
