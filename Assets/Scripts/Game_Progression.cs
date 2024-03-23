using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game_Progression : MonoBehaviour
{
    public Haptic_Chair_Controller hapticChairScript;
    public Rotate_Me_Parent rotateParent_Script;

    public bool playerMayFly = true;
    public bool somethingIsBroken = false;

    private float timeRemaining = 900;   //time limit
    private bool timerIsRunning = false;
    float minutes, seconds;
    public TextMeshProUGUI timeText;

    private FadeScreen_DD fadeScreenDD;
    public static bool gameOver_outOfFuel = false, gameOver_shipMalfunction = false, gameOver_win = false;

    //private bool tent1IsComplete = false, tent2IsComplete = false, tent3IsComplete = false;
    public Medtent medtentObject_Script;

    public bool medboxExists = false;
    public GameObject medBoxToSpawn;
    public Transform medBoxSpawnLoc;

    //Drone Stuff
    public GameObject theDrone;
    public GameObject DronePrefab;
    public Transform newDroneLocation;

    public SwitchMaterialChanger screenChanger;

    void Start()
    {
        fadeScreenDD = GameObject.Find("FaderScreen").GetComponent<FadeScreen_DD>();
        //hapticChairScript.FlyFunction(5);
        timerIsRunning = true;
        //this.gameObject.GetComponent<PlaySounds>().PlayNineSecondsShipFly();
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                gameOver_outOfFuel = true;
                timeRemaining = 0;
                timerIsRunning= false;
                GameOver();
            }
        }
        
    }

    private void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        minutes = Mathf.FloorToInt(timeToDisplay / 60);
        seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void SetSomethingIsBroken(bool value)
    {
        somethingIsBroken = value;
    }

    public void SetPlayerMayFly(bool value)
    {
        playerMayFly = value;
    }

    public void TentAppears()
    {
        //tent spawns on sphere environment and rotates to a visible point to the Mothership 
        //  GameObject newTent = Instantiate(tentPrefab, whereToInstantiate, parent_environmentSphere);
        //requested medical items are transmitted to the ship's screen
        //(different based on the tent number)      ...randomized each game?
        //


    }

    public void LeverPilotStation()
    {
        if (playerMayFly == true && rotateParent_Script.moveValue == 1)
        {
            if (somethingIsBroken == true)
            {
                playerMayFly = false;
                hapticChairScript.HardStopTheShip(3);
                //do malfunction here
                //upon pressing button, it will fix the malfunction
            }
            else
            {
                //player flies to next tent
                hapticChairScript.FlyFunction(6);
                //wait six seconds, send supply request
                StartCoroutine(CallMedTent());

            }
        }
    }

    IEnumerator CallMedTent()
    {
        //make sure seconds wait is less time than it takes for ship to fly
        yield return new WaitForSeconds(3);
        medtentObject_Script.SendSupplyRequestToMothership();
    }

    public void GameOver()
    {
        hapticChairScript.HapticGameOver();
        fadeScreenDD.FadeOut();
        StartCoroutine(GameOverMessage());
    }

    IEnumerator GameOverMessage()
    {
        yield return new WaitForSeconds(3);

        if (gameOver_win == true)
        {
            Scene_Manager_DD.mainScreenTextOption = 1;
        } 
        else if (gameOver_outOfFuel == true)
        {
            Scene_Manager_DD.mainScreenTextOption = 3;
        } 
        else if (gameOver_shipMalfunction == true)
        {
            Scene_Manager_DD.mainScreenTextOption = 2;
        }

        string gameScene = "DoomsdayDelivery_Menu";
        SceneManager.LoadScene(gameScene);
    }

    public void InstantiateNewMedbox()
    {
        //upon turn to station 2, if no medbox is present, instantiate a medbox
        if (medboxExists == false)
        {
            //instantiate a medbox
            StartCoroutine(WaitForRotation());
            
        }

    }

    //Function to spawn new drone
    public void SpawnNewDrone()
    {
        theDrone.transform.position = newDroneLocation.position;
        screenChanger.ChangeMaterialToBlank();
        //GameObject newDrone = Instantiate(DronePrefab, newDroneLocation);

    }

    IEnumerator WaitForRotation()
    {
        yield return new WaitForSeconds(1);
        GameObject spawnMedbox = Instantiate(medBoxToSpawn, medBoxSpawnLoc);
        spawnMedbox.GetComponentInChildren<NewObjectCounter>().rotateParentScript = GameObject.Find("STATIONS_MOVABLE").GetComponent<Rotate_Me_Parent>();
        spawnMedbox.GetComponentInChildren<NewObjectCounter>().PlayStation2EnterAnimation();
        medboxExists = true;
    }

}