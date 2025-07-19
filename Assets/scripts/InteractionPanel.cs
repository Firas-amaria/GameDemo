using UnityEngine;

public class InteractionPanel : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("UI Panels")]
    public GameObject nextPanel;
    public void Close()
    {
        gameObject.SetActive(false);
        GameManager.Instance?.SetUIState(false); 
    }

    public void Next()
    {
        gameObject.SetActive(false);
        nextPanel?.SetActive(true);

    }
} 

