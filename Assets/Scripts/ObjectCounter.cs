using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectCounter : MonoBehaviour
{
    // List of tags to be counted
    [SerializeField]
    private List<string> tagsToCount;

    // Dictionary to keep track of counts of different tags
    private Dictionary<string, int> tagCounts = new Dictionary<string, int>();

    // Reference to the TextMeshPro component
    public TextMeshProUGUI countDisplay;

    private void Start()
    {
        // Initialize the dictionary with the tags to count
        foreach (string tag in tagsToCount)
        {
            tagCounts[tag] = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object has a tag you're interested in and count it
        if (tagsToCount.Contains(other.tag))
        {
            // Increment the count for that tag
            tagCounts[other.tag]++;

            // Update the UI
            UpdateCountDisplay();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Optional: Handle objects leaving the box if needed
        if (tagsToCount.Contains(other.tag) && tagCounts[other.tag] > 0)
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
        foreach (var tag in tagsToCount)
        {
            if (tagCounts.ContainsKey(tag))
            {
                countDisplay.text += $"{tag}: {tagCounts[tag]}\n";
            }
        }
    }
}

//delete objects once box closes (so they don't move around in box