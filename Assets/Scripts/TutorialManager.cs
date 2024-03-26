using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    // Public list visible in the inspector
    public List<GameObject> Steps;
    public int LeverValue = 0; // Added public integer LeverValue

    // Function to remove (delete) the game object at the specified index
    public void RemoveStep(int number)
    {
        if (number >= 0 && number < Steps.Count)
        {
            Steps[number].SetActive(false);
            // Other removal logic can be added here if necessary
        }
        else
        {
            // Debug.LogError("Index out of range: " + number);
        }
    }

    // Function to enable the game object at the specified index
    public void AddStep(int number)
    {
        if (number >= 0 && number < Steps.Count)
        {
            if (number == 3)
            {
                switch (LeverValue)
                {
                    case 0:
                        LeverValue = 1; // Increment LeverValue to 1
                        break;
                    case 1:
                        number = 6; // Treat it as if AddStep(6) is called
                        LeverValue = 2; // Increment LeverValue to 2
                        break;
                    case 2:
                        return; // Do nothing
                }
            }

            Steps[number].SetActive(true);
        }
        else
        {
            // Debug.LogError("Index out of range: " + number);
        }
    }

    public void DeleteTutorial()
    {
        foreach (var step in Steps)
        {
            if (step != null)
            {
                Destroy(step);
            }
        }

        Steps.Clear(); // Clear the list after destroying all GameObjects
    }
}