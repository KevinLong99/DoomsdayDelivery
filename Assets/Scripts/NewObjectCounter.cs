using System.Collections.Generic;
using UnityEngine;
using TMPro; // Using TextMeshPro for UI elements.
using System.Collections;
using System.Runtime.CompilerServices;

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

    public Rotate_Me_Parent rotateParentScript;
    private Animator medBoxAnimator;

    private Vector3 ovenLocation;
    private Vector3 station3Loc;

    public int numTotalItems = 0;

    public TextMeshProUGUI requirementText;

    private void Start()
    {
        GameObject requirementUI = GameObject.Find("RequirementUI");
        if (requirementUI != null)
        {
            requirementText = requirementUI.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogError("RequirementUI GameObject not found in the scene. Please make sure it exists and is named correctly.");
        }

        foreach (string tag in tagsToCount)
        {
            tagCounts[tag] = 0; // Initialize count for each tag to 0.
        }
    }

    private void Awake()
    {
        station3Loc = GameObject.Find("MedBoxSpawnLoc").transform.position;
        ovenLocation = GameObject.Find("OvenPos").transform.position;
        medBoxAnimator = GetComponentInParent<Animator>();
        rotateParentScript = GameObject.Find("STATIONS_MOVABLE").GetComponent<Rotate_Me_Parent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (tagsToCount.Contains(other.tag)) // Check if the object's tag is in the list of tags to count.
        {
            tagCounts[other.tag]++; // Increment count for the tag.
            UpdateTagCounts(); // Update public int variables.
        }
        else if (other.gameObject.CompareTag("Medtent"))
        {
            Medtent medtentScript = other.gameObject.GetComponent<Medtent>();
            if (medtentScript != null)
            {
                CheckRequirements(medtentScript); // Check if requirements are met.
            }
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (tagsToCount.Contains(other.tag) && tagCounts[other.tag] > 0) // Check if the object's tag is in the list and the count is greater than 0.
        {
            tagCounts[other.tag]--; // Decrement count for the tag.
            UpdateTagCounts(); // Update public int variables.
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

    private void CheckRequirements(Medtent medtentScript)
    {
        // Check requirements for each SpawnStuff and update UI text
        requirementText.text =
            (SpawnStuff1 >= medtentScript.SpawnStuff1 ? "Requirement met for obj1" : "Requirement not met  for obj1") + "\n" +
            (SpawnStuff2 >= medtentScript.SpawnStuff2 ? "Requirement met  for obj2" : "Requirement not met  for obj2") + "\n" +
            (SpawnStuff3 >= medtentScript.SpawnStuff3 ? "Requirement met  for obj3" : "Requirement not met  for obj3");
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

    public int GetNumTotalItems()
    {
        return numTotalItems;
    }

    public void PlayStation2EnterAnimation()
    {

        if (rotateParentScript.medKitisCompleted == false)
        {
            medBoxAnimator.Play("Box_Fall");
        }
    }

    public void PlayStation2ExitAnimation()
    {
        //play animation when turned to station 3 if medkit is completed
        if (rotateParentScript.medKitisCompleted == true)
        {
            //play animation, time it with Pizza Paddle Launch
            medBoxAnimator.Play("Closing");
            StartCoroutine(BoxMoveSequence());

        }
    }

    IEnumerator BoxMoveSequence()
    {
        Vector3 station2InAir = new Vector3(station3Loc.x, station3Loc.y + 0.15f, station3Loc.z);
        StartCoroutine(TranslateOven(station3Loc, station2InAir, 0.5f));
        yield return new WaitForSeconds(0.75f);
        StartCoroutine(TranslateOven(station2InAir, ovenLocation, 0.5f));
    }

    IEnumerator TranslateOven(Vector3 from, Vector3 to, float time)
    {
        float duration = time;

        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            this.transform.parent.transform.position = Vector3.Lerp(from, to, counter / duration);
            yield return null;
        }
    }


}