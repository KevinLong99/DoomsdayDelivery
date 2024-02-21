using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Game_Progression : MonoBehaviour
{
    public Haptic_Chair_Controller hapticChairScript;

    public bool playerMayFly = true;
    public bool somethingIsBroken = false;

    private float timeRemaining = 5;    //time limit
    private bool timerIsRunning = false;
    float minutes, seconds;
    public Text timeText;

    private FadeScreen_DD fadeScreenDD;
    public bool gameOver_outOfFuel = false, gameOver_shipMalfunction = false, gameOver_win = false;
    public Text gameOver_textMessage;

    //private bool tent1IsComplete = false, tent2IsComplete = false, tent3IsComplete = false;

    void Start()
    {
        fadeScreenDD = GameObject.Find("FaderScreen").GetComponent<FadeScreen_DD>(); ;
        hapticChairScript.FlyFunction(5);
        //timerIsRunning = true;
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
                GameOver();
                timeRemaining = 0;
                timerIsRunning= false;
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
        //jerk the chair to a stop
        hapticChairScript.HapticGameOver();
        //fade screen to black as the chair jerks
        fadeScreenDD.FadeOut();
        //"game over"... with
        // "you ran out of fuel!" or "you failed to fix the ship in time!"
        StartCoroutine(GameOverMessage());
    }

    IEnumerator GameOverMessage()
    {
        yield return new WaitForSeconds(3);
        //3 seconds because that's the amount of time it takes for screen to fade to black
        string gameOverText = "GAME OVER! \n (no message...nothing was turned to true)";

        if (gameOver_win == true)
        {
            gameOverText = "Congratulations! You successfully delivered all the supplies!";
        } else if (gameOver_outOfFuel == true)
        {
            gameOverText = "GAME OVER! \n You ran out of fuel!";
        } else if (gameOver_shipMalfunction == true)
        {
            gameOverText = "GAME OVER! \n You failed to fix the ship in time!";
        }
        gameOver_textMessage.gameObject.SetActive(true);
        gameOver_textMessage.text = gameOverText;

    }


}
