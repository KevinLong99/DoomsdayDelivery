using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Progression : MonoBehaviour
{
    public Haptic_Chair_Controller hapticChairScript;

    public bool playerMayFly = true;
    public bool somethingIsBroken = false;

    //private bool tent1IsComplete = false, tent2IsComplete = false, tent3IsComplete = false;

    void Start()
    {
        hapticChairScript.FlyFunction(5);
    }

    void Update()
    {
        
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
}
