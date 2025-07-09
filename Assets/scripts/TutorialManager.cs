using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel;        // The main panel that holds the tutorial UI
    public TMP_Text tutorialText;           // The TMP_Text to display messages
    public Button continueButton;           // The button to continue to the next message
    public string[] messages;               // List of tutorial messages

    private int currentMessageIndex = 0;

    void Start()
    {
        Time.timeScale = 0f; // Pause the game
        tutorialPanel.SetActive(true);
        tutorialText.text = ""; // Clear any default text
        ShowMessage();

        // Connect the Continue button to NextMessage
        continueButton.onClick.AddListener(NextMessage);
    }

    void ShowMessage()
    {
        if (currentMessageIndex < messages.Length)
        {
            // Append the new message with spacing
            tutorialText.text += "\n\n• " + messages[currentMessageIndex];
        }
        else
        {
            EndTutorial();
        }
    }

    public void NextMessage()
    {
        currentMessageIndex++;
        ShowMessage();
    }

    void EndTutorial()
    {
        tutorialPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
