using UnityEngine;

public enum InteractionType
{
    UnlockBook,
    UnlockCipher
}

public class InteractableComponent : MonoBehaviour, IInteractable
{
    public InteractionType interactionType;
    public string dataKey; // book slotKey or cipherKey
    public string interactionID; // for one-time popup
    public string interactionMessage = "Press X to interact";

    public GameObject glowEffect;

    private bool isPlayerNear = false;

    public void Interact()
    {
        switch (interactionType)
        {
            case InteractionType.UnlockBook:
                ProgressManager.Instance.UnlockBook(dataKey);
                break;
            case InteractionType.UnlockCipher:
                ProgressManager.Instance.UnlockCipher(dataKey);
                break;
        }

        UIManager.Instance.ShowInteractionResult();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        isPlayerNear = true;
        glowEffect?.SetActive(true);

        if (!ProgressManager.Instance.HasSeenInteractionHint(interactionID))
        {
            UIManager.Instance.ShowMessage(interactionMessage);
            ProgressManager.Instance.MarkInteractionHintSeen(interactionID);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        isPlayerNear = false;
        glowEffect?.SetActive(false);
        UIManager.Instance.HideMessage();
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.X))
        {
            Interact();
        }
    }
}
