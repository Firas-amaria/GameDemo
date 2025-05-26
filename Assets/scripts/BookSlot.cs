using UnityEngine;
using UnityEngine.UI;

public class BookSlot : MonoBehaviour
{
    [Header("Assigned manually in Inspector")]
    public string slotKey;           // Unique slot ID (e.g., "room1_slot1")
    public Image iconImage;          // Image used to show the book icon

    // Called when player clicks the book slot
    public void OnClickSlot()
    {
        BookDataModel book = BookDatabaseLoader.Instance.GetBookBySlot(slotKey);

        if (book != null)
        {
            InventoryUIManager.Instance.ShowBookInfo(book);
        }
        else
        {
            Debug.LogWarning("No book found for slot key: " + slotKey);
        }
    }


    // Optional: Used to dim or reset icons visually (e.g., for found vs. not found)
    public void SetFound(bool isFound)
    {
        iconImage.color = isFound ? Color.white : new Color(1, 1, 1, 0.3f); // Transparent for missing
    }
}
