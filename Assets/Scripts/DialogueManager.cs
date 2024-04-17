using UnityEngine;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using System;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public GameObject DisplayDialogue; // The GameObject that will display the sprite
    public List<string> Dialogues; // List of dialogue sprites
    public int LeverValue = 0; // Public integer LeverValue with default 0

    public GameObject TutorialArea;

    //To tell if the tutorial is passed
    public bool tutorialPassed = false;
    private bool isLevelErrorActive = false; // Flag to check if LevelError coroutine is running


    // Call this function to change the displayed sprite to the specified one in the list

    //Change text to blank
    public void ChangeTextToBlank()
    {
        TextMeshPro textMesh = DisplayDialogue.GetComponent<TextMeshPro>();
        textMesh.text = "";
    }
    public void ChangeText(int num)
    {
        // Check if the passed index is within the range of the Dialogues list
        if (num >= 0 && num < Dialogues.Count)
        {
            TextMeshPro textMesh = DisplayDialogue.GetComponent<TextMeshPro>();
            //Tutorial part dialogue, would not trigger after tutorial is passed
            if (num < 10 && tutorialPassed == false)
            {
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
                                tutorialPassed = true;
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

            //Malfunction part arrow tips, trigger any time malfunctions happen
            if (num >= 10)
            {
                textMesh.text = Dialogues[num];
            }
        }


    }

    public void DeleteTutorial()
    {
        if (TutorialArea != null)
        {
            TutorialArea.SetActive(false); // Disable the tutorial UI game object
        }

        //Dialogues.Clear(); // Clear all items from the Dialogues list
    }


    //Temporarily display lever pull error message

    IEnumerator LevelError()
    {
        if (isLevelErrorActive)
            yield break; // Exit if the coroutine is already running

        isLevelErrorActive = true; // Set the flag to true indicating the coroutine is running

        TextMeshPro textMesh = DisplayDialogue.GetComponent<TextMeshPro>();
        string originalText = textMesh.text;

        if (Dialogues.Count > 12)
        {
            textMesh.text = Dialogues[12];
        }
        else
        {
            Debug.LogError("DialogueManager: Index 12 out of range.");
            isLevelErrorActive = false; // Reset the flag as we exit early
            yield break;
        }

        yield return new WaitForSeconds(2);

        textMesh.text = originalText;
        isLevelErrorActive = false; // Reset the flag after the coroutine completes
    }


    public void CallLevelError()
    {
        StartCoroutine(LevelError());
    }
}