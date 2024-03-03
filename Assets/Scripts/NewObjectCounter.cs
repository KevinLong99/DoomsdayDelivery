using System.Collections.Generic;
using UnityEngine;
using TMPro; // Using TextMeshPro for UI elements.

public class NewObjectCounter : MonoBehaviour
{
    [SerializeField]
    private List<string> tagsToCount; // List of tags to count objects for.

    private Dictionary<string, int> tagCounts = new Dictionary<string, int>(); // Dictionary to keep track of object counts for each tag.

    // Public integers for each tag to access the counts from other scripts.
    public int SpawnStuff1;
    public int SpawnStuff2;
    public int SpawnStuff3;
    // Add more public int variables here for each tag you plan to count.

    public TextMeshProUGUI countDisplay; // Reference to UI element to display counts.

    private void Start()
    {
        foreach (string tag in tagsToCount)
        {
            tagCounts[tag] = 0; // Initialize count for each tag to 0.
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (tagsToCount.Contains(other.tag)) // Check if the object's tag is in the list of tags to count.
        {
            tagCounts[other.tag]++; // Increment count for the tag.
            UpdateTagCounts(); // Update public int variables.
            UpdateCountDisplay(); // Update the UI display.
        }


        // Comparing to MedTent

        if (other.gameObject.CompareTag("Medtent")) // Assuming "Medtent" is the tag used for the Medtent object
        {
            Medtent medtentScript = other.gameObject.GetComponent<Medtent>();
            if (medtentScript != null)
            {
                // Compare Object1
                if (GetCountForTag("SpawnStuff1") == medtentScript.SpawnStuff1)
                {
                    // Trigger function for matching count
                    Debug.Log("Amount Mached G1");
                }
                else
                {
                    // Trigger function for non-matching count
                    Debug.Log("Amount do not match G1");
                }

                //Compare Object2
                if (GetCountForTag("SpawnStuff2") == medtentScript.SpawnStuff2)
                {
                    // Trigger function for matching count
                    Debug.Log("Amount Mached G2");
                }
                else
                {
                    // Trigger function for non-matching count
                    Debug.Log("Amount do not match G2");
                }

                //Compare Object3
                if (GetCountForTag("SpawnStuff3") == medtentScript.SpawnStuff3)
                {
                    // Trigger function for matching count
                    Debug.Log("Amount Mached G3");
                }
                else
                {
                    // Trigger function for non-matching count
                    Debug.Log("Amount do not match G3");
                }
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (tagsToCount.Contains(other.tag) && tagCounts[other.tag] > 0) // Check if the object's tag is in the list and the count is greater than 0.
        {
            tagCounts[other.tag]--; // Decrement count for the tag.
            UpdateTagCounts(); // Update public int variables.
            UpdateCountDisplay(); // Update the UI display.
        }
    }

    private void UpdateTagCounts()
    {
        // Update the public int variables based on the current counts in tagCounts.
        SpawnStuff1 = tagCounts.ContainsKey("SpawnStuff1") ? tagCounts["SpawnStuff1"] : 0;
        SpawnStuff2 = tagCounts.ContainsKey("SpawnStuff2") ? tagCounts["SpawnStuff2"] : 0;
        SpawnStuff3 = tagCounts.ContainsKey("SpawnStuff3") ? tagCounts["SpawnStuff3"] : 0;
        // Repeat for additional tags.
    }

    private void UpdateCountDisplay()
    {
        countDisplay.text = ""; // Clear current display.
        foreach (var tag in tagsToCount)
        {
            if (tagCounts.ContainsKey(tag))
            {
                // Update display with current count for each tag.
                countDisplay.text += $"{tag}: {tagCounts[tag]}\n";
            }
        }
    }

    // Comparing to MedTent
    public int GetCountForTag(string tag)
    {
        if (tagCounts.ContainsKey(tag))
        {
            return tagCounts[tag];
        }
        return 0; // Return 0 if the tag is not found
    }


}