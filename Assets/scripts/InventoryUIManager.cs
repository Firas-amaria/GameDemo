using System.Collections.Generic;
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
    [Header("Slot Containers")]
    public Transform room1Container;
    public Transform room2Container;
    public Transform room3Container;

    [Header("Prefabs")]
    public GameObject bookSlotPrefab;
    public GameObject emptySlotPrefab; // a dimmed visual with no button



    private BookDataModel currentBook;

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

    public void PopulateBookSlots()
    {
        ClearContainers();

        List<BookDataModel> allBooks = BookDatabaseLoader.Instance.bookDatabase.books;

        foreach (var book in allBooks)
        {
            Transform targetContainer = GetContainerForRoom(book.slotKey);
            bool isUnlocked = ProgressManager.Instance.data.unlockedBooks.Contains(book.slotKey);

            GameObject slot = Instantiate(
                isUnlocked ? bookSlotPrefab : emptySlotPrefab,
                targetContainer
            );

            if (isUnlocked)
            {
                BookSlot bookSlot = slot.GetComponent<BookSlot>();
                bookSlot.slotKey = book.slotKey;
                bookSlot.SetFound(true);
            }
        }
    }

    private void ClearContainers()
    {
        foreach (Transform child in room1Container) Destroy(child.gameObject);
        foreach (Transform child in room2Container) Destroy(child.gameObject);
        foreach (Transform child in room3Container) Destroy(child.gameObject);
    }

    private Transform GetContainerForRoom(string slotKey)
    {
        if (slotKey.StartsWith("room1")) return room1Container;
        if (slotKey.StartsWith("room2")) return room2Container;
        if (slotKey.StartsWith("room3")) return room3Container;
        Debug.LogWarning("No matching container for " + slotKey);
        return room1Container;
    }


    public void ShowBookInfo(BookDataModel book)
    {
        currentBook = book;

        titleText.text = "Title: " + book.title;
        genreText.text = "Genre: " + book.genre;
        authorText.text = "Author: " + book.author;
        descriptionText.text = book.description;

        readBookButton.gameObject.SetActive(true);
    }

    public void OpenBookReader()
    {
        Debug.Log("Opening book: " + currentBook.title);
        BookReader.Instance.OpenBook(currentBook);
    }
}
