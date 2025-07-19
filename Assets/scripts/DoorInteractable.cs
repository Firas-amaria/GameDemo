using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

public class DoorInteractable : MonoBehaviour, IInteractable
{
    public string sceneToLoad;
    public string correctPassword;
    public bool isSolved = false;

    [Header("UI")]
    public GameObject doorPanel; // shared panel in Canvas
    [Header("Visuals")]
    public GameObject glowEffect;

    private bool isPlayerNear = false;

    public void Interact()
    {
        if (isSolved)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            if (doorPanel != null)
            {
                doorPanel.SetActive(true);
                GameManager.Instance?.SetUIState(true);

                // Set this door as the active reference for the panel controller
                Debug.Log(sceneToLoad);

                DoorPanelController controller = doorPanel.GetComponent<DoorPanelController>();
                if (controller != null)
                {
                    Debug.Log("2nd"+sceneToLoad);
                    controller.SetLinkedDoor(this);
                }

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        isPlayerNear = true;
        glowEffect?.SetActive(true);


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
