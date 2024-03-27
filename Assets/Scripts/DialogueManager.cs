using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject DisplayDialogue; // The GameObject that will display the sprite
    public List<string> Dialogues; // List of dialogue sprites
    public int LeverValue = 0; // Public integer LeverValue with default 0

    public GameObject TutorialArea;
    // Call this function to change the displayed sprite to the specified one in the list
    public void ChangeText(int num)
    {
        // Check if the passed index is within the range of the Dialogues list
        if (num >= 0 && num < Dialogues.Count)
        {
            TextMeshPro textMesh = DisplayDialogue.GetComponent<TextMeshPro>();
            if (textMesh != null)
            {
                // Handling the special conditions for num == 3
                if (num == 3)
                {
                    switch (LeverValue)
                    {
                        case 0:
                            LeverValue = 1; // Increase LeverValue to 1
                            textMesh.text = Dialogues[num];
                            break;
                        case 1:
                            LeverValue = 2; // Increase LeverValue to 2
                            // Treat it as if ChangeText(6) is called
                            if (6 < Dialogues.Count) // Ensure index 6 exists
                            {
                                textMesh.text = Dialogues[6];
                            }
                            else
                            {
                                Debug.LogError("DialogueManager: Index 6 out of range.");
                            }
                            break;
                        case 2:
                            DeleteTutorial();
                            break;
                    }
                }
                else
                {
                    textMesh.text = Dialogues[num];
                }
            }
            else
            {
                Debug.LogError("DialogueManager: The DisplayDialogue GameObject does not have a TextMeshPro component.");
            }
        }
        else
        {
            Debug.LogError("DialogueManager: Index out of range when calling ChangeText.");
        }
    }

    public void DeleteTutorial()
    {
        if (TutorialArea != null)
        {
            TutorialArea.SetActive(false); // Delete the TutorialArea GameObject
        }

        Dialogues.Clear(); // Clear all items from the Dialogues list
    }
}