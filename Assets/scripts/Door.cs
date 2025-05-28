using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Door : MonoBehaviour
{
    public int sceneBuildIndex;
    public GameObject nameInputPanel;
    public TMP_InputField nameInputField;

    private bool nameEntered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !nameEntered)
        {
            Time.timeScale = 0f;
            nameInputPanel.SetActive(true);
        }
    }

    public void OnNameSubmit() 
    {
        string playerName = nameInputField.text;

        if (!string.IsNullOrWhiteSpace(playerName))
        {
            Debug.Log("Player name: " + playerName);
            nameEntered = true;
            Time.timeScale = 1f;
            SceneManager.LoadScene(sceneBuildIndex);
        }
        else
        {
            Debug.LogWarning("Name cannot be empty");
        }
    }
}
