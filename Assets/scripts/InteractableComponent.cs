using UnityEngine;

public enum InteractionType
{
    UnlockBook,
    UnlockCipher,
    Trigger
}

public class InteractableComponent : MonoBehaviour, Interactable
{
    [Header("Interaction Settings")]
    public InteractionType interactionType;
    public string dataKey;
    public string interactionID;

    [Header("UI Panels")]
    public GameObject interactionPanel;
    public GameObject resultPanel;

    [Header("Visuals")]
    public GameObject glowEffect;

    private bool isPlayerNear = false;
    private bool hasShownPanel = false;

    private void Start()
    {
        // Check if already unlocked
        if (!string.IsNullOrEmpty(dataKey))
        {
            bool shouldDisable = false;

            if (interactionType == InteractionType.UnlockBook)
                shouldDisable = ProgressManager.Instance.data.unlockedBooks.Contains(dataKey);

            else if (interactionType == InteractionType.UnlockCipher)
                shouldDisable = ProgressManager.Instance.data.unlockedCiphers.Contains(dataKey);

            if (shouldDisable)
            {
                Debug.Log($"[InteractableComponent] Already unlocked: {dataKey}. Disabling object.");
                transform.parent.gameObject.SetActive(false);
            }
        }
    }

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
                {
                    ProgressManager.Instance.UnlockCipher(dataKey);

                    // ✅ Update GuidePanel UI
                    if (GuidePanelManager.Instance != null)
                        GuidePanelManager.Instance.RefreshGuidePanel();
                    else
                        Debug.LogWarning("[InteractableComponent] GuidePanelManager.Instance is null. Could not refresh guide panel.");
                }
                break;

            case InteractionType.Trigger:
                // Do nothing special
                break;
        }

        if (interactionType != InteractionType.Trigger)
        {
            transform.parent.gameObject.SetActive(false);
        }

        if (resultPanel != null)
        {
            resultPanel.SetActive(true);
            GameManager.Instance.SetUIState(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        isPlayerNear = true;
        glowEffect?.SetActive(true);

        if (!hasShownPanel)
        {
            if (!string.IsNullOrEmpty(interactionID))
            {
                if (!ProgressManager.Instance.HasSeenInteractionHint(interactionID))
                {
                    interactionPanel?.SetActive(true);
                    GameManager.Instance.SetUIState(true);
                    ProgressManager.Instance.MarkInteractionHintSeen(interactionID);
                    hasShownPanel = true;
                }
            }
            else
            {
                interactionPanel?.SetActive(true);
                GameManager.Instance.SetUIState(true);
                hasShownPanel = true;
            }
        }

        if (interactionType == InteractionType.Trigger)
        {
            transform.parent.gameObject.SetActive(false);
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
