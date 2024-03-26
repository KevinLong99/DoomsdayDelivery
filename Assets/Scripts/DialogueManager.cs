using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject DisplayDialogue; // The GameObject that will display the sprite
    public List<string> Dialogues; // List of dialogue sprites

    // Call this function to change the displayed sprite to the specified one in the list
    public void ChangeText(int num)
    {
        // Check if the passed index is within the range of the Dialogues list
        if (num >= 0 && num < Dialogues.Count)
        {
            TextMeshPro textMesh = DisplayDialogue.GetComponent<TextMeshPro>();
            if (textMesh != null)
            {
                textMesh.text = Dialogues[num];
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
    /*
    public void DeleteText()
    {
        if (DisplayDialogue != null)
        {
            TextMeshPro textMesh = DisplayDialogue.GetComponent<TextMeshPro>();
            if (textMesh != null)
            {
                Debug.Log("DialogueManager: Clearing text.");
                textMesh.text = "";
            }
            else
            {
                Debug.LogError("DialogueManager: No TextMeshPro component to clear text from.");
            }
        }
        else
        {
            Debug.LogError("DialogueManager: No DisplayDialogue GameObject to delete text from.");
        }
    }
    */
}