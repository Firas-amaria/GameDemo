using UnityEngine;

public enum InteractionType
{
    UnlockBook,
    UnlockCipher,
    Trigger
}

public class InteractableComponent : MonoBehaviour, IInteractable
{
    [Header("Interaction Settings")]
    public InteractionType interactionType;
    public string dataKey;        // book slotKey or cipherKey (can be empty for Trigger)
    public string interactionID;  // one-time panel trigger ID (can be empty)

    [Header("UI Panels")]
    public GameObject interactionPanel; // shown only once (set manually in scene)
    public GameObject resultPanel;      // shown every time after interaction

    [Header("Visuals")]
    public GameObject glowEffect;

    private bool isPlayerNear = false;
    private bool hasShownPanel = false;

    public void Interact()
    {
        switch (interactionType)
        {
            case InteractionType.UnlockBook:
                if (!string.IsNullOrEmpty(dataKey))
                    ProgressManager.Instance.UnlockBook(dataKey);
                break;

            case InteractionType.UnlockCipher:
                if (!string.IsNullOrEmpty(dataKey))
                    ProgressManager.Instance.UnlockCipher(dataKey);

                // Deactivate parent GameObject
                transform.parent.gameObject.SetActive(false);
                break;

            case InteractionType.Trigger:
                // Trigger type just shows UI
                break;
        }

        if (resultPanel != null)
        {
            resultPanel.SetActive(true);
            GameManager.Instance.SetUIState(true); // Stop input
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        isPlayerNear = true;
        glowEffect?.SetActive(true);

        // Only show interactionPanel once
        if (!hasShownPanel)
        {
            if (!string.IsNullOrEmpty(interactionID))
            {
                if (!ProgressManager.Instance.HasSeenInteractionHint(interactionID))
                {
                    interactionPanel?.SetActive(true);
                    GameManager.Instance.SetUIState(true); // Stop input
                    ProgressManager.Instance.MarkInteractionHintSeen(interactionID);
                    hasShownPanel = true;
                }
            }
            else
            {
                interactionPanel?.SetActive(true);
                GameManager.Instance.SetUIState(true); // Stop input
                hasShownPanel = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        isPlayerNear = false;
        glowEffect?.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.X))
        {
            Interact();
        }
    }
}
