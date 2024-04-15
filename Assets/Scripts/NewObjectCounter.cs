using System.Collections.Generic;
using UnityEngine;
using TMPro; // Using TextMeshPro for UI elements.
using System.Collections;
using Unity.VisualScripting;
using System;

public class NewObjectCounter : MonoBehaviour
{
    [SerializeField]
    private List<string> tagsToCount; // List of tags to count objects for.

    private Dictionary<string, int> tagCounts = new Dictionary<string, int>(); // Dictionary to keep track of object counts for each tag.

    // Public integers for each tag to access the counts from other scripts.
    public int BandageTag;
    public int OintmentTag;
    public int SyringeTag;
    public int InsulinTag;
    // Add more public int variables here for each tag you plan to count.

    public Rotate_Me_Parent rotateParentScript;
    private Animator medBoxAnimator;

    private Vector3 ovenLocation;
    private Vector3 station2Loc;
    private Transform station3Deploy;
    private Transform station3DeployLoaded;

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

    public Typewriter_UI typewriteScript_NOC;

    // Requirement dialogue
    public string Tent1YesMsg;
    public string Tent1NoMsg;
    public string Tent2YesMsg;
    public string Tent2NoMsg;
    public string Tent3YesMsg;
    public string Tent3NoMsg;

    private Medtent medtentScript;

    private float thresholdPercentage;

    // Public list of TextMeshPro UI elements
    public List<TextMeshPro> SupplyCountText;

    //Progress Manager
    public ProgressManager myProgressManager;
    public UIManager myUIManager;
    public bool isFirstLoop = false;

    public PlaySounds playSoundScript;

    private void Start()    
    {

        //Initialize the progress manager
        myProgressManager = GameObject.Find("ProgressManager").GetComponent<ProgressManager>();
        myUIManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        foreach (string tag in tagsToCount)
        {
            tagCounts[tag] = 0; // Initialize count for each tag to 0.
        }

        // Initialize the SupplyCountUI list
        //when you switch to the med assembly station, the text you are trying to find below 
        //      needs to become ACTIVE, then these can be found.    --rachel
        SupplyCountText = new List<TextMeshPro>
    {
        GameObject.Find("Bandage_Count").GetComponent<TextMeshPro>(),
        GameObject.Find("Ointment_Count").GetComponent<TextMeshPro>(),
        GameObject.Find("Syringe_Count").GetComponent<TextMeshPro>(),
        GameObject.Find("Insulin_Count").GetComponent<TextMeshPro>()
    };


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
        station3Deploy = GameObject.Find("PreDeploymentLoc_High").transform;
        station3DeployLoaded = GameObject.Find("PreDeploymentLoaded").transform;
        medBoxAnimator = GetComponentInParent<Animator>();
        rotateParentScript = GameObject.Find("STATIONS_MOVABLE").GetComponent<Rotate_Me_Parent>();
        droneToBeDropped = GameObject.Find("Drone_ToBeDropped");
        this.gameObject.transform.parent.transform.parent = GameObject.Find("STATIONS_MOVABLE").transform;
        chuteAnimator = GameObject.Find("Drone_chute_door").GetComponent<Animator>();
        ovenAnimator = GameObject.Find("oven_door").GetComponent<Animator>();
        switchReturn_Script = GameObject.Find("Drone [Remote]").GetComponent<SwitchReturn>();
        gameProg_Script = GameObject.Find("Game_Manager").GetComponent<Game_Progression>();
        
        try
        {
            typewriteScript_NOC = GameObject.Find("Dialogue_Text").GetComponent<Typewriter_UI>();
            playSoundScript = GameObject.Find("Game_Manager").GetComponent<PlaySounds>();
        } catch (Exception e)
        {
            Debug.LogWarning(e + " DD: Could not find Typewriter script. Typewriter gameobject is likely disabled.");
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (tagsToCount.Contains(other.tag)) // Check if the object's tag is in the list of tags to count.
        {
            tagCounts[other.tag]++; // Increment count for the tag.
            UpdateTagCounts(); // Update public int variables.
            numTotalItems++;
        }
        else if (other.gameObject.CompareTag("Medtent"))
        {
            if(isFirstLoop == false)
            {
                myUIManager.ToEnableDropSwitchTip();
                isFirstLoop = true;
            }
            else
            {
                return;
            }
            medtentScript = other.gameObject.GetComponent<Medtent>();
            thresholdPercentage = medtentScript.thresholdPercentage;
            if (medtentScript != null)
            {
                myProgressManager.UpdateMedTent();
                CheckRequirements(medtentScript); // Check if requirements are met.
                gameProg_Script.deliveryMessageActive = false;
                gameProg_Script.arrivedAtTent = false;
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
        //Assign tag counts to UI coutns
        BandageTag = tagCounts.ContainsKey("BandageTag") ? tagCounts["BandageTag"] : 0;
        OintmentTag = tagCounts.ContainsKey("OintmentTag") ? tagCounts["OintmentTag"] : 0;
        SyringeTag = tagCounts.ContainsKey("SyringeTag") ? tagCounts["SyringeTag"] : 0;
        InsulinTag = tagCounts.ContainsKey("InsulinTag") ? tagCounts["InsulinTag"] : 0;

        // Update UI Texts
        if (SupplyCountText.Count >= 4) // Ensure all text elements are assigned
        {
            SupplyCountText[0].text = BandageTag.ToString();
            SupplyCountText[1].text = OintmentTag.ToString();
            SupplyCountText[2].text = SyringeTag.ToString();
            SupplyCountText[3].text = InsulinTag.ToString();
        }
    }

    private void CheckRequirements(Medtent medtentScript)
    {
        // Individual supply and requirement checks
        bool bandageMet = (float)BandageTag / medtentScript.BandageTag >= thresholdPercentage;
        bool ointmentMet = (float)OintmentTag / medtentScript.OintmentTag >= thresholdPercentage;
        bool syringeMet = (float)SyringeTag / medtentScript.SyringeTag >= thresholdPercentage;
        bool insulinMet = (float)InsulinTag / medtentScript.InsulinTag >= thresholdPercentage;

        // Count how many supplies have met the required threshold
        int suppliesAtThresholdCount = 0;
        if (bandageMet) suppliesAtThresholdCount++;
        if (ointmentMet) suppliesAtThresholdCount++;
        if (syringeMet) suppliesAtThresholdCount++;
        if (insulinMet) suppliesAtThresholdCount++;

        myUIManager.ToResetDialogue();
        typewriteScript_NOC = GameObject.Find("Dialogue_Text").GetComponent<Typewriter_UI>();

        if (suppliesAtThresholdCount >= 3)
        {
            myUIManager.ToSuccessDelivery();
            if (!gameProg_Script.tent3IsComplete && gameProg_Script.tent2IsComplete)
            {
                gameProg_Script.SetSurv3Active();
                playSoundScript.PlayAnaSuccess();
                typewriteScript_NOC.StartTypewriterView(Tent3YesMsg);
            }
            if (!gameProg_Script.tent2IsComplete && gameProg_Script.tent1IsComplete)
            {
                gameProg_Script.SetSurv2Active();
                playSoundScript.PlayJJSuccess();
                typewriteScript_NOC.StartTypewriterView(Tent2YesMsg);
            } 
            if (!gameProg_Script.tent1IsComplete)
            {
                gameProg_Script.SetSurv1Active();
                playSoundScript.PlayRachelSuccess();
                typewriteScript_NOC.StartTypewriterView(Tent1YesMsg);
            }
        }
        else
        {
            myUIManager.ToFailDelivery();
            if (!gameProg_Script.tent3IsComplete && gameProg_Script.tent2IsComplete)
            {
                gameProg_Script.SetSurv3Active();
                playSoundScript.PlayAnaFail();
                typewriteScript_NOC.StartTypewriterView(Tent3NoMsg);
            }
            if (!gameProg_Script.tent2IsComplete && gameProg_Script.tent1IsComplete)
            {
                gameProg_Script.SetSurv2Active();
                playSoundScript.PlayJJFail();
                typewriteScript_NOC.StartTypewriterView(Tent2NoMsg);
            }
            if (!gameProg_Script.tent1IsComplete)
            {
                gameProg_Script.SetSurv1Active();
                playSoundScript.PlayRachelFail();
                typewriteScript_NOC.StartTypewriterView(Tent1NoMsg);
            }
        }



        /*
        // Perform actions based on the number of supplies that meet the threshold
        if (suppliesAtThresholdCount >= 3)
        {
            // Code to execute if at least three tags reached the threshold- Success
            myUIManager.ToSuccessDelivery();
            if (medtentScript.tentNum == 1)
            {
                typewriteScript_NOC.StartTypewriterView(Tent1YesMsg);
            }
            else if (medtentScript.tentNum == 2)
            {
                typewriteScript_NOC.StartTypewriterView(Tent2YesMsg);
            }
            else if (medtentScript.tentNum == 3)
            {
                typewriteScript_NOC.StartTypewriterView(Tent3YesMsg);
            }
        }
        else
        {
            // Code to execute if fewer than three tags reached the threshold- Failed
            myUIManager.ToFailDelivery();
            if (medtentScript.tentNum == 1)
            {
                typewriteScript_NOC.StartTypewriterView(Tent1NoMsg);
            }
            else if (medtentScript.tentNum == 2)
            {
                typewriteScript_NOC.StartTypewriterView(Tent2NoMsg);
            }
            else if (medtentScript.tentNum == 3)
            {
                typewriteScript_NOC.StartTypewriterView(Tent3NoMsg);
            }
        }
        */

        ResetCounts();
        
        StartCoroutine(ReturnScreenToPos());


    }

    IEnumerator ReturnScreenToPos()
    {
        yield return new WaitForSeconds(3);
        switchReturn_Script.TrySetPositionToTarget();
        rotateParentScript.medKitisCompleted = false;
        gameProg_Script.medboxExists = false;
        gameProg_Script.SpawnNewDrone();
        //Instantiate(DronePrefab, newDroneLocation.position, newDroneLocation.rotation);
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
        StartCoroutine(TranslateLerp(this.gameObject.transform.position, ovenLocation, 1f));
        ovenAnimator.Play("Oven_Door_Open");
        yield return new WaitForSeconds(1);

        //play animations for box leaving oven 
        chuteAnimator.Play("Drone_chute_open");
        StartCoroutine(TranslateLerp(ovenLocation, station3Deploy.position, 0.5f));
        StartCoroutine(RotateLerp(this.transform.parent.transform.rotation, station3Deploy.rotation, 0.5f));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(TranslateLerp(station3Deploy.position, station3DeployLoaded.position, 0.25f));

        //and drone falling and attaching on top
        droneToBeDropped.GetComponent<Drone_InsideShip>().DropInStation3();
        yield return new WaitForSeconds(1);

        //make drone the parent of the box
        this.gameObject.transform.parent.transform.parent = droneToBeDropped.transform;

        //player picks up controller Switch and deploys from there (press trigger to deploy)
        yield return new WaitForSeconds(0.25f);       //remove this
        ovenAnimator.Play("Oven_Door_Close");
        droneToBeDropped.GetComponent<Drone_InsideShip>().DroneDeploy();
        yield return new WaitForSeconds(0.5f);
        chuteAnimator.Play("Drone_chute_close");

        MedBoxDeleteAndSpawn();
    }

    //Delete and Spawn function
    private void MedBoxDeleteAndSpawn()
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
            newMedBoxCounter.BandageTag = this.BandageTag;
            newMedBoxCounter.OintmentTag = this.OintmentTag;
            newMedBoxCounter.SyringeTag = this.SyringeTag;
            newMedBoxCounter.InsulinTag = this.InsulinTag;

            //newMedBoxCounter.typewriteScript_NOC = this.typewriteScript_NOC;
        }
    }
    public void DeleteAllSupply()
    {
        // Array of tags to check for deletion
        string[] tagsToDelete = { "BandageTag", "OintmentTag", "SyringeTag", "InsulinTag" };

        // Iterate through each tag and delete all GameObjects with that tag
        foreach (var tag in tagsToDelete)
        {
            GameObject[] objectsToDelete = GameObject.FindGameObjectsWithTag(tag);
            foreach (var obj in objectsToDelete)
            {
                Destroy(obj);
            }
        }
    }
    //Reset the supply counts and their UI displays to zero
    private void ResetCounts()
    {
        BandageTag = OintmentTag = SyringeTag = InsulinTag = 0;
        UpdateTagCounts();
    }
}