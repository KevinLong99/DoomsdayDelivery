using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TutorialTextManager : MonoBehaviour
{
    public Text tutorialText; // Assign this in the Inspector
    public List<string> dialogues; // Populate this list in the Inspector

    private int currentDialogueIndex = 0;

    public void ChangeText()
    {
        if (tutorialText == null)
        {
            Debug.LogError("TutorialTextManager: No UI Text component assigned!");
            return;
        }

        if (currentDialogueIndex < dialogues.Count)
        {
            tutorialText.text = dialogues[currentDialogueIndex];
            currentDialogueIndex++;
        }
        else
        {
            Debug.LogWarning("TutorialTextManager: No more dialogues to display.");
        }
    }

    // Optional: Reset the dialogue to the beginning
    public void ResetDialogue()
    {
        currentDialogueIndex = 0;
        ChangeText(); // Call this if you want to reset and display the first dialogue immediately
    }
}