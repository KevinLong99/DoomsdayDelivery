using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game_Progression : MonoBehaviour
{
    public Haptic_Chair_Controller hapticChairScript;

    public bool playerMayFly = true;
    public bool somethingIsBroken = false;

    private float timeRemaining = 15;    //time limit
    private bool timerIsRunning = false;
    float minutes, seconds;
    public Text timeText;

    private FadeScreen_DD fadeScreenDD;
    public static bool gameOver_outOfFuel = false, gameOver_shipMalfunction = false, gameOver_win = false;

    //private bool tent1IsComplete = false, tent2IsComplete = false, tent3IsComplete = false;

    void Start()
    {
        fadeScreenDD = GameObject.Find("FaderScreen").GetComponent<FadeScreen_DD>(); ;
        hapticChairScript.FlyFunction(5);
        timerIsRunning = true;
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
        if (playerMayFly == true)
        {
            if (somethingIsBroken == true)
            {
                playerMayFly = false;
                hapticChairScript.HardStopTheShip(6);
            }
            else
            {
                hapticChairScript.FlyFunction(4);
            }
        }
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
            Scene_Manager_DD.mainScreenTextOption = 2;
        } 
        else if (gameOver_shipMalfunction == true)
        {
            Scene_Manager_DD.mainScreenTextOption = 3;
        }

        string gameScene = "DoomsdayDelivery_Menu";
        SceneManager.LoadScene(gameScene);
    }
}
