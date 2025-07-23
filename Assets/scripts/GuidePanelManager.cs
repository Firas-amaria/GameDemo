using UnityEngine;

public class GuidePanelManager : MonoBehaviour
{
    public static GuidePanelManager Instance; 

    [Header("Section Panels")]
    public GameObject movementPanel;
    public GameObject caesarPanel;
    public GameObject vigenerePanel;

    [Header("Section Buttons")]
    public GameObject caesarButton;
    public GameObject vigenereButton;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        // Hide all subpanels by default
        movementPanel.SetActive(true);
        caesarPanel.SetActive(false);
        vigenerePanel.SetActive(false);

        RefreshGuidePanel(); // instead of Update()
    }

    public void RefreshGuidePanel()
    {
        bool caesarUnlocked = ProgressManager.Instance.GetUnlockedCiphers().Contains("Caesar");
        bool vigenereUnlocked = ProgressManager.Instance.GetUnlockedCiphers().Contains("Vigenere");

        caesarButton.SetActive(caesarUnlocked);
        vigenereButton.SetActive(vigenereUnlocked);

        // Don't auto-open anything here
        caesarPanel.SetActive(false);
        vigenerePanel.SetActive(false);
    }

    public void ShowMovementInfo()
    {
        movementPanel.SetActive(true);
        caesarPanel.SetActive(false);
        vigenerePanel.SetActive(false);
    }

    public void ShowCaesarInfo()
    {
        movementPanel.SetActive(false);
        caesarPanel.SetActive(true);
        vigenerePanel.SetActive(false);
    }

    public void ShowVigenereInfo()
    {
        movementPanel.SetActive(false);
        caesarPanel.SetActive(false);
        vigenerePanel.SetActive(true);
    }
}
