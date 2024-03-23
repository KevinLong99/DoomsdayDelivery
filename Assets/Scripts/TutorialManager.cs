using System.Collections.Generic;
using UnityEngine;


public class TutorialManager : MonoBehaviour
{
    // Public list visible in the inspector
    public List<GameObject> Steps;

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
            Steps[number].SetActive(true);
        }
        else
        {
            Debug.LogError("Index out of range: " + number);
        }
    }
}