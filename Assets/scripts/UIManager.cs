using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject messagePanel;
    public TMP_Text messageText;

    public GameObject interactionResultPanel; // <- new reference!

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowMessage(string message)
    {
        if (messagePanel != null && messageText != null)
        {
            messagePanel.SetActive(true);
            messageText.text = message;
        }
    }

    public void HideMessage()
    {
        if (messagePanel != null)
            messagePanel.SetActive(false);
    }

    public void ShowInteractionResult()
    {
        if (interactionResultPanel != null)
            interactionResultPanel.SetActive(true);
    }

    public void HideInteractionResult()
    {
        if (interactionResultPanel != null)
            interactionResultPanel.SetActive(false);
    }

}
