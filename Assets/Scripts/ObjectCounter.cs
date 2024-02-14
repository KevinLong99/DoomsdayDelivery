using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectCounter : MonoBehaviour
{
    // Dictionary to keep track of counts of different tags
    private Dictionary<string, int> tagCounts = new Dictionary<string, int>();

    // Reference to the UI Text component that displays the count
    public TextMeshProUGUI countDisplay;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object has a tag
        if (tagCounts.ContainsKey(other.tag))
        {
            // Increment the count for that tag
            tagCounts[other.tag]++;
        }
        else
        {
            // Add the new tag with a count of 1
            tagCounts.Add(other.tag, 1);
        }

        // Update the UI
        UpdateCountDisplay();
    }

    private void OnTriggerExit(Collider other)
    {
        // Optional: Handle objects leaving the box if needed
        if (tagCounts.ContainsKey(other.tag) && tagCounts[other.tag] > 0)
        {
            // Decrement the count for that tag
            tagCounts[other.tag]--;

            // Update the UI
            UpdateCountDisplay();
        }
    }

    private void UpdateCountDisplay()
    {
        // Clear existing text
        countDisplay.text = "";

        // Display counts for all tracked tags
        foreach (var tagCount in tagCounts)
        {
            countDisplay.text += $"{tagCount.Key}: {tagCount.Value}\n";
        }
    }
}