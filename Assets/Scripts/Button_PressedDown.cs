using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_PressedDown : MonoBehaviour
{
    private string buttonName;

    private GameObject buttonVisual;
    private GameObject buttonOutside;

    public bool isPressedDownFully = false;

    public Rotate_Me_Parent rotateMeParentScript;
    public Haptic_Chair_Controller hapticChairControllerScript;

    void Start()
    {
        buttonName = this.gameObject.name;
        buttonVisual = this.gameObject.transform.GetChild(1).gameObject;
        buttonOutside = this.gameObject.transform.GetChild(2).gameObject;
    }

    void Update()
    {
        //hapticChairControllerScript.SwitchStationRight();
        //if i want to implement haptics ^ i have to define if chair is moving left or right

        //rotate TO station 1
        if (buttonVisual.transform.position.y < buttonOutside.transform.position.y
            && isPressedDownFully == false
            && (buttonName == "Button_Stat2_Right" || buttonName == "Button_Stat3_Left"))
        {
            isPressedDownFully = true;
            rotateMeParentScript.RotateToStationOne();
            if (buttonName.Contains("Right"))
            {
                hapticChairControllerScript.SwitchStationRight();
            } else
            {   //if not right, must be left
                hapticChairControllerScript.SwitchStationLeft();
            }
            isPressedDownFully = false;
        }

        //rotate TO station 2
        if (buttonVisual.transform.position.y < buttonOutside.transform.position.y
            && isPressedDownFully == false
            && (buttonName == "Button_Stat3_Right" || buttonName == "Button_Stat1_Left"))
        {
            isPressedDownFully = true;
            rotateMeParentScript.RotateToStationTwo();
            if (buttonName.Contains("Right"))
            {
                hapticChairControllerScript.SwitchStationRight();
            }
            else
            {   //if not right, must be left
                hapticChairControllerScript.SwitchStationLeft();
            }
            isPressedDownFully = false;
        }

        //rotate TO station 3
        if (buttonVisual.transform.position.y < buttonOutside.transform.position.y
            && isPressedDownFully == false
            && (buttonName == "Button_Stat1_Right" || buttonName == "Button_Stat2_Left"))
        {
            isPressedDownFully = true;
            rotateMeParentScript.RotateToStationThree();
            if (buttonName.Contains("Right"))
            {
                hapticChairControllerScript.SwitchStationRight();
            }
            else
            {   //if not right, must be left
                hapticChairControllerScript.SwitchStationLeft();
            }
            isPressedDownFully = false;
        }



    }
}
