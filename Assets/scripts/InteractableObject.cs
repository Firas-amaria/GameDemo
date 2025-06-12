using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string interactionMessage = "Press X to open the drawer";
    private bool isPlayerNear = false;

    void Update()
{
    if (isPlayerNear && Input.GetKeyDown(KeyCode.X))
    {
        Debug.Log("Drawer interaction triggered!");
        UIManager.Instance.ShowInteractionResult();
    }
}


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            UIManager.Instance.ShowMessage(interactionMessage);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            UIManager.Instance.HideMessage();
        }
    }
}
