using OpenAI;
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

    private float timeRemaining = 420;   //time limit
    private bool timerIsRunning = false;
    float minutes, seconds;
    public TextMeshProUGUI timeText;
    public Image gasGauge;
    private float startingGasColor = 0;

    private FadeScreen_DD fadeScreenDD;
    public static bool gameOver_outOfFuel = false, gameOver_shipMalfunction = false, gameOver_win = false;

    public bool tent1IsComplete = false, tent2IsComplete = false, tent3IsComplete = false;
    public Medtent medtentObject_Script;

    public bool medboxExists = false;
    public GameObject medBoxToSpawn;
    public Transform medBoxSpawnLoc;

    //Drone Stuff
    public GameObject theDrone;
    public GameObject DronePrefab;
    public Transform newDroneLocation;

    public SwitchMaterialChanger screenChanger;

    public Typewriter_UI typewriter_Script;
    private PlaySounds playSounds_Script;
    private string errorMessage = "ERROR! ERROR! SHIP MALFUNCTION! \n" +
        "Press the big red button to fix the malfunction, and push thruster " +
        "forward to continue flying.";
    private string errorResolvedMessage = "You fixed the ship! Resume flying by pushing" +
        " the thruster forward.";


    private GameObject medtent1, medtent2, medtent3;
    public bool deliveryMessageActive = false;
    public bool arrivedAtTent = false;
    private bool needToFixShip = false;

    private ChatGPT chatGptScript;

    void Start()
    {
        fadeScreenDD = GameObject.Find("FaderScreen").GetComponent<FadeScreen_DD>();
        timerIsRunning = true;
        playSounds_Script = this.gameObject.GetComponent<PlaySounds>();
        chatGptScript = GameObject.Find("ChatGPT").GetComponent<ChatGPT>();

        medtent1 = GameObject.Find("MedTent1");
        medtent2 = GameObject.Find("MedTent2");
        medtent3 = GameObject.Find("MedTent3");

        medtent1.SetActive(false);
        medtent2.SetActive(false);
        medtent3.SetActive(false);

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

        // Gas bar
        if (gasGauge.fillAmount > 0)
        {
            gasGauge.fillAmount = timeToDisplay / 600;
            startingGasColor = Mathf.Lerp(1, 127, timeToDisplay / 600);
            gasGauge.color = Color.HSVToRGB(startingGasColor/255, 1, 1);
        }
        else
            gasGauge.fillAmount = 0;
    }

    public void FixMalfunction()
    {
        if (somethingIsBroken == true && needToFixShip == true)
        {
            playerMayFly = true;
            somethingIsBroken = false;
            needToFixShip = false;

            typewriter_Script.StartTypewriterView(errorResolvedMessage);
        } 
    }

    public void LeverPilotStation()
    {
        if (playerMayFly == true && rotateParent_Script.moveValue == 1)
        {
            if (somethingIsBroken == true)
            {
                typewriter_Script.StartTypewriterView("LOADING.....");
                chatGptScript.SendReply();

                playerMayFly = false;
                hapticChairScript.HardStopTheShip(3);
                needToFixShip = true;
                //do malfunction here
                //upon pressing button, it will fix the malfunction

                //send message to screen saying "error, press button to fix ship and 
                //  push lever to continue to next tent

            }
            else
            {
                if(tent1IsComplete == false)
                {
                    //do this only once
                    medtent1.SetActive(true);
                }

                //player flies to next tent
                hapticChairScript.FlyFunction(6);
                playerMayFly = false;
                arrivedAtTent = true;

            }
        }
    }

    public void CallMedTent()
    {
        //medtentObject_Script.SendSupplyRequestToMothership();
        playSounds_Script.PlayRandomBeeping();
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
        if (medboxExists == false && arrivedAtTent == true)
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

        //set up the next tents or end the game
        TentManager();
    }

    private void TentManager()
    {
        if(!tent3IsComplete && tent2IsComplete)
        {
            gameOver_win = true;
            GameOver();
        }

        if (!tent2IsComplete && tent1IsComplete)
        {
            tent2IsComplete = true;
            medtent3.SetActive(true);
            medtent2.SetActive(false);
            playerMayFly = true;
            somethingIsBroken = true;
        }

        if (!tent1IsComplete)
        {
            tent1IsComplete = true;
            medtent2.SetActive(true);
            medtent1.SetActive(false);
            playerMayFly = true;
            somethingIsBroken = true;
        }

        
    }

    IEnumerator WaitForRotation()
    {
        yield return new WaitForSeconds(1f);
        GameObject spawnMedbox = Instantiate(medBoxToSpawn, medBoxSpawnLoc);
        spawnMedbox.GetComponentInChildren<NewObjectCounter>().rotateParentScript = GameObject.Find("STATIONS_MOVABLE").GetComponent<Rotate_Me_Parent>();
        spawnMedbox.GetComponentInChildren<NewObjectCounter>().PlayStation2EnterAnimation();
        medboxExists = true;
    }

}