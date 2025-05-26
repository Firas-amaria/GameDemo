using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    public static InventoryUIManager Instance;

    [Header("UI References")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI genreText;
    public TextMeshProUGUI authorText;
    public TextMeshProUGUI descriptionText;
    public Button readBookButton;

    //private BookDataModel currentBook;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        // Hide the read button initially
        readBookButton.gameObject.SetActive(false);

        // Register button click
        readBookButton.onClick.AddListener(OpenBookReader);
    }

    //public void ShowBookInfo(BookDataModel book)
    //{
    //    currentBook = book;

    //    titleText.text = "Title: " + book.title;
    //    genreText.text = "Genre: " + book.genre;
    //    authorText.text = "Author: " + book.author;
    //    descriptionText.text = book.description;

    //    readBookButton.gameObject.SetActive(true);
    //}

    private void OpenBookReader()
    {
       // Debug.Log("Opening book: " + currentBook.title);
        // TODO: Load BookReaderPanel and display currentBook.pages[]
    }
}
