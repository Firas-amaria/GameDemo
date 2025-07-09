using UnityEngine;

public class InteractionPanel : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Close()
    {
        gameObject.SetActive(false);
        GameManager.Instance?.SetUIState(false); 
    }
} 

