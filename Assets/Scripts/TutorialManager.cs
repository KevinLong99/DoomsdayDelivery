using System.Collections.Generic;
using UnityEngine;


public class TutorialManager : MonoBehaviour
{
    // Public list visible in the inspector
    public List<GameObject> Steps;

    //bool for lever so it's only triggered once
    public bool leverIsPulled = false;

    // Function to remove (delete) the game object at the specified index
    public void RemoveStep(int number)
    {
        if (number >= 0 && number < Steps.Count)
        {
            Steps[number].SetActive(false);
            //Destroy(Steps[number]);
            //Steps.RemoveAt(number);
        }
        else
        {
            Debug.LogError("Index out of range: " + number);
        }
    }

    // Function to enable the game object at the specified index
    public void AddStep(int number)
    {
        if (number >= 0 && number < Steps.Count)
        {
            if (number == 3 && leverIsPulled)
            {
                // If leverIsPulled is true and AddStep(3) is called again, do nothing
                return;
            }

            Steps[number].SetActive(true);

            if (number == 3)
            {
                leverIsPulled = true; // Set leverIsPulled to true after AddStep(3) is called
            }
        }
        else
        {
            Debug.LogError("Index out of range: " + number);
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