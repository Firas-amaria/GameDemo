using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BookReader : MonoBehaviour
{
    public static BookReader Instance;

    [Header("UI References")]
    public GameObject readerPanel;
    public TextMeshProUGUI titleText;
    public TMP_InputField pageInputField;
    public Button nextButton;
    public Button prevButton;
    public Button closeButton;

    private BookDataModel currentBook;
    private int currentPage = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        readerPanel.SetActive(false);

        nextButton.onClick.AddListener(NextPage);
        prevButton.onClick.AddListener(PrevPage);
        closeButton.onClick.AddListener(CloseReader);
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
            pageInputField.text = "No content";
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
}
