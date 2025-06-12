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
        messagePanel.SetActive(true);
        messageText.text = message;
    }

    public void HideMessage()
    {
        messagePanel.SetActive(false);
    }

    public void ShowInteractionResult()
    {
        interactionResultPanel.SetActive(true);
    }

    public void HideInteractionResult()
    {
        interactionResultPanel.SetActive(false);
    }
}
