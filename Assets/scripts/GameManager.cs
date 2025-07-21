using UnityEngine;
using UnityEngine.SceneManagement;

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
            //DontDestroyOnLoad(gameObject); // Optional
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleInventory()
    {
        if (inventoryPanel == null || pauseButton == null || inventoryButton == null)
        {
            Debug.LogWarning("[GameManager] UI references are not set. Cannot toggle inventory.");
            return;
        }

        bool isOpening = !inventoryPanel.activeSelf;

        inventoryPanel.SetActive(isOpening);
        pauseButton.SetActive(!isOpening);
        inventoryButton.SetActive(!isOpening);

        SetUIState(isOpening); // Block/unblock input
        if (isOpening)
        {
            InventoryUIManager.Instance?.RefreshInventoryUI();
        }
    }


    public bool IsInputBlocked => isInputBlocked;

    public void ReconnectSceneUI()
    {
       
        inventoryPanel = GameObject.FindWithTag("InventoryPanel");
        pauseButton = GameObject.FindWithTag("PauseButton");
        inventoryButton = GameObject.FindWithTag("InventoryButton");

        if (inventoryPanel == null)
            Debug.LogWarning("[GameManager] Inventory Panel not found.");
        if (pauseButton == null)
            Debug.LogWarning("[GameManager] Pause Button not found.");
        if (inventoryButton == null)
            Debug.LogWarning("[GameManager] Inventory Button not found.");


        if (inventoryPanel != null && inventoryPanel.activeSelf)
        {
            inventoryPanel.SetActive(false);
        }
    }



    public void SetUIState(bool isUIOpen)
    {
        isInputBlocked = isUIOpen;
        //Debug.Log("Input blocked: " + isInputBlocked);
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ReconnectSceneUI();
    }

}
