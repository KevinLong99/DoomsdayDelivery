using System.Collections.Generic;
using UnityEngine;
using TMPro; // Using TextMeshPro for UI elements.
using System.Collections;
using Unity.VisualScripting;

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
    private Vector3 station2Loc;
    private Transform station3Deploy;

    public int numTotalItems = 0;

    public TextMeshProUGUI requirementText;

    private GameObject droneToBeDropped;
    private Animator chuteAnimator;
    private Animator ovenAnimator;

    // Delete and Spawn objects

    public Transform newBoxSpawnLocation;
    public GameObject medBoxPrefab;

    private SwitchReturn switchReturn_Script;
    private Game_Progression gameProg_Script;

    //Drone Stuff
    public GameObject DronePrefab;
    public Transform newDroneLocation;


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

        //Spawn location initialization

        // Assign newBoxSpawnLocation as the transform of the GameObject named "setBoxSpawnLocation" in the scene
        GameObject setBoxSpawnLocation = GameObject.Find("setBoxSpawnLocation");
        if (setBoxSpawnLocation != null)
        {
            newBoxSpawnLocation = setBoxSpawnLocation.transform;
        }
        else
        {
            Debug.LogError("setBoxSpawnLocation GameObject not found in the scene.");
        }

        GameObject myDroneLocation = GameObject.Find("newDroneLocation");
        newDroneLocation = myDroneLocation.transform;

    }

    private void Awake()
    {
        station2Loc = GameObject.Find("MedBoxSpawnLoc").transform.position;
        ovenLocation = GameObject.Find("OvenPos").transform.position;
        station3Deploy = GameObject.Find("PreDeploymentLoc").transform;
        medBoxAnimator = GetComponentInParent<Animator>();
        rotateParentScript = GameObject.Find("STATIONS_MOVABLE").GetComponent<Rotate_Me_Parent>();
        droneToBeDropped = GameObject.Find("Drone_ToBeDropped");
        this.gameObject.transform.parent.transform.parent = GameObject.Find("STATIONS_MOVABLE").transform;
        chuteAnimator = GameObject.Find("Drone_chute_door").GetComponent<Animator>();
        ovenAnimator = GameObject.Find("oven_door").GetComponent<Animator>();
        switchReturn_Script = GameObject.Find("Drone [Remote]").GetComponent<SwitchReturn>();
        gameProg_Script = GameObject.Find("Game_Manager").GetComponent<Game_Progression>();
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
        /*
        if (tagsToCount.Contains(other.tag) && tagCounts[other.tag] > 0) // Check if the object's tag is in the list and the count is greater than 0.
        {
            tagCounts[other.tag]--; // Decrement count for the tag.
            UpdateTagCounts(); // Update public int variables.
        }
        */
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

        //switchReturn_Script.TrySetPositionToTarget();
        //!!! LET GO OF THE CONTROLLER ONCE YOU DELIVER SUPPLIES!!!
        StartCoroutine(ReturnScreenToPos());


    }

    IEnumerator ReturnScreenToPos()
    {
        Debug.Log("in returnscreentopos");
        yield return new WaitForSeconds(5);
        switchReturn_Script.TrySetPositionToTarget();
        rotateParentScript.medKitisCompleted = false;
        gameProg_Script.medboxExists = false;
        gameProg_Script.SpawnNewDrone();
        //Instantiate(DronePrefab, newDroneLocation.position, newDroneLocation.rotation);
        Debug.Log("New Drone Spawned");
        Destroy(this.gameObject.transform.parent.gameObject);

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

    public void PlayStation3EnterAnimation()
    {
        StartCoroutine(Station3EnterCoroutine());
    }

    IEnumerator BoxMoveSequence()
    {
        Vector3 station2InAir = new Vector3(station2Loc.x, station2Loc.y + 0.15f, station2Loc.z);
        StartCoroutine(TranslateLerp(station2Loc, station2InAir, 0.5f));
        yield return new WaitForSeconds(0.75f);
        StartCoroutine(TranslateLerp(station2InAir, ovenLocation, 0.5f));
    }

    IEnumerator TranslateLerp(Vector3 from, Vector3 to, float time)
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

    IEnumerator RotateLerp(Quaternion from, Quaternion to, float time)
    {
        float duration = time;

        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            this.transform.parent.transform.rotation = Quaternion.Lerp(from, to, counter / duration);
            yield return null;
        }
    }

    IEnumerator Station3EnterCoroutine()
    {
        ovenAnimator.Play("Oven_Door_Open");
        yield return new WaitForSeconds(1);

        //play animations for box leaving oven 
        StartCoroutine(TranslateLerp(ovenLocation, station3Deploy.position, 0.5f));
        StartCoroutine(RotateLerp(this.transform.rotation, station3Deploy.rotation, 0.5f));
        yield return new WaitForSeconds(0.5f);

        //and drone falling and attaching on top
        droneToBeDropped.GetComponent<Drone_InsideShip>().DropInStation3();
        yield return new WaitForSeconds(1);

        //make drone the parent of the box
        this.gameObject.transform.parent.transform.parent = droneToBeDropped.transform;

        //player picks up controller Switch and deploys from there (press trigger to deploy)
        yield return new WaitForSeconds(2);       //remove this
        ovenAnimator.Play("Oven_Door_Close");
        chuteAnimator.Play("Drone_chute_open");
        droneToBeDropped.GetComponent<Drone_InsideShip>().DroneDeploy();
        yield return new WaitForSeconds(1);
        //kevin drop delete
        MedBoxDeleteAndSpawn();

        //call this function upon trigger select when picked up drone controller
        yield return new WaitForSeconds(0.75f);
        chuteAnimator.Play("Drone_chute_close");


    }

    //Delete and Spawn function
    public void MedBoxDeleteAndSpawn()
    {
        // Delete the existing "MedBox"
        GameObject medBox = GameObject.FindGameObjectWithTag("Medbox");
        if (medBox != null)
        {
            Destroy(medBox);
        }

        // Spawn the medBoxPrefab at newBoxSpawnLocation
        GameObject newMedBox = Instantiate(medBoxPrefab, newBoxSpawnLocation.position, newBoxSpawnLocation.rotation);
        newMedBox.transform.SetParent(GameObject.Find("_Drone [BumbleBee]").transform, false);

        // Copy the values of SpawnStuff1, SpawnStuff2, and SpawnStuff3 to the new medBoxPrefab
        NewObjectCounter newMedBoxCounter = newMedBox.GetComponent<NewObjectCounter>();
        if (newMedBoxCounter != null)
        {
            newMedBoxCounter.SpawnStuff1 = this.SpawnStuff1;
            newMedBoxCounter.SpawnStuff2 = this.SpawnStuff2;
            newMedBoxCounter.SpawnStuff3 = this.SpawnStuff3;
        }
    }


}