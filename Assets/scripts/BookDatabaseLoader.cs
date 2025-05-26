using UnityEngine;
using System.IO;

public class BookDatabaseLoader : MonoBehaviour
{
    public static BookDatabaseLoader Instance;
    public BookDatabase bookDatabase;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        LoadBookDatabase();
    }

    private void LoadBookDatabase()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "books.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            bookDatabase = JsonUtility.FromJson<BookDatabase>(json);
        }
        else
        {
            Debug.LogError("books.json not found at: " + path);
        }
    }

    public BookDataModel GetBookBySlot(string slotKey)
    {
        return bookDatabase?.books.Find(book => book.slotKey == slotKey);
    }
}
