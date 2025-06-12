using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject messagePanel;
    public TMP_Text messageText;

    public GameObject interactionResultPanel;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void ShowMessage(string message)
    {
        if (messagePanel != null && messageText != null)
        {
            messagePanel.SetActive(true);
            messageText.text = message;

            GameManager.Instance?.SetUIState(true);  // 🟡 Block movement
        }
    }

    public void HideMessage()
    {
        if (messagePanel != null)
        {
            messagePanel.SetActive(false);
            GameManager.Instance?.SetUIState(false); // 🟢 Unblock movement
        }
    }

    public void ShowInteractionResult()
    {
        if (interactionResultPanel != null)
        {
            interactionResultPanel.SetActive(true);
            GameManager.Instance?.SetUIState(true); // Optional
        }
    }

    public void HideInteractionResult()
    {
        if (interactionResultPanel != null)
        {
            interactionResultPanel.SetActive(false);
            GameManager.Instance?.SetUIState(false); // Optional
        }
    }
}
