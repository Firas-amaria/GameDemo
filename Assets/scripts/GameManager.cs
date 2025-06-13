using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool isInputBlocked = false;

    // Inventory UI references
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject inventoryButton;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleInventory()
    {
        bool isOpening = !inventoryPanel.activeSelf;

        inventoryPanel.SetActive(isOpening);
        pauseButton.SetActive(!isOpening);
        inventoryButton.SetActive(!isOpening);

        SetUIState(isOpening); // Block/unblock input
        if (isOpening)
        {
            InventoryUIManager.Instance.RefreshInventoryUI();
        }
    }

    public bool IsInputBlocked => isInputBlocked;

    public void SetUIState(bool isUIOpen)
    {
        isInputBlocked = isUIOpen;
        //Debug.Log("Input blocked: " + isInputBlocked);
    }
}
