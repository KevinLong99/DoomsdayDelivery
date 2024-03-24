using UnityEngine;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public GameObject DisplayDialogue; // The GameObject that will display the sprite
    public List<Sprite> Dialogues; // List of dialogue sprites

    // Call this function to change the displayed sprite to the specified one in the list
    public void ChangeText(int num)
    {
        // Check if the passed index is within the range of the Dialogues list
        if (num >= 0 && num < Dialogues.Count)
        {
            SpriteRenderer spriteRenderer = DisplayDialogue.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = Dialogues[num];
            }
            else
            {
                Debug.LogError("TutorialTextManager: The DisplayDialogue GameObject does not have a SpriteRenderer component.");
            }
        }
        else
        {
            Debug.LogError("TutorialTextManager: Index out of range when calling ChangeText.");
        }
    }
}