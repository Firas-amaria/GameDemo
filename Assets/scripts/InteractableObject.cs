using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string interactionMessage = "Press X to open the drawer";
    private bool isPlayerNear = false;
    public string openBook = "room1_slot2";
    public string interactionID = "drawer1"; // Must be unique per object
    public GameObject glowVisual;


    void Update()
{
        if (!isPlayerNear || ProgressManager.Instance == null || UIManager.Instance == null)
            return;

        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Drawer interaction triggered!");
            ProgressManager.Instance.UnlockBook(openBook);
            UIManager.Instance.ShowInteractionResult();
        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;

            if (!ProgressManager.Instance.HasSeenInteractionHint(interactionID))
            {
                UIManager.Instance.ShowMessage(interactionMessage);
                ProgressManager.Instance.MarkInteractionHintSeen(interactionID);
            }

            EnableGlowEffect(true);
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            //UIManager.Instance.HideMessage();
            EnableGlowEffect(false);
        }
    }

    private void EnableGlowEffect(bool show)
    {
        if (glowVisual != null)
            glowVisual.SetActive(show);
    }


}
