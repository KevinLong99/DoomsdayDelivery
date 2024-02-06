using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Me_Parent : MonoBehaviour
{
    //ROTATE VALUES:    0   120 240 

    private bool rotating = false;
    public GameObject objectToRotate;

    //Station Rotation Values
    private Quaternion rotation1 = Quaternion.Euler(new Vector3(0, -60, 0));
    private Quaternion rotation2 = Quaternion.Euler(new Vector3(0, -180, 0));
    private Quaternion rotation3 = Quaternion.Euler(new Vector3(0, -300, 0));

    public int moveValue = 0;

    private bool nickRotate = false;

    public Haptic_Chair_Controller hapticChairContScript;

    private void Update()
    {
        
        if (moveValue == 1)
        {
            if (nickRotate == false)
            {
                RotateToStationOne();
                nickRotate = true;
            }
            
        }
        else if (moveValue == 2)
        {
            RotateToStationTwo();    
        }
        else if(moveValue == 3)
        {
            RotateToStationThree();
        }
        else
        {
            //do nothing. Do not rotate
        }
    }


    public void RotateToStationOne()
    {
        StartCoroutine(rotateObject(objectToRotate, rotation3, 1f));
        Debug.Log("ROTATE to 1");
        hapticChairContScript.SwitchStationRight();
    }
    public void RotateToStationTwo()
    {
        StartCoroutine(rotateObject(objectToRotate, rotation2, 1f));
        Debug.Log("ROTATE to 2");
        hapticChairContScript.SwitchStationRight();
    }
    public void RotateToStationThree()
    {
        StartCoroutine(rotateObject(objectToRotate, rotation1, 1f));
        Debug.Log("ROTATE to 3");
        hapticChairContScript.SwitchStationRight();
    }


    IEnumerator rotateObject(GameObject gameObjectToMove, Quaternion newRot, float duration)
    {
        if (rotating)
        {
            yield break;
        }
        rotating = true;

        Quaternion currentRot = gameObjectToMove.transform.rotation;

        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            gameObjectToMove.transform.rotation = Quaternion.Lerp(currentRot, newRot, counter / duration);
            yield return null;
        }
        rotating = false;
    }
}



/*
this.transform.rotation = Quaternion.Slerp(from.rotation, Station2Quat, timeCount * 0.01f); // * 0.01f
timeCount = (timeCount * 0.01f) + Time.deltaTime; 

^this breaks^ with the 0.01f

*/


//Script Credits:
//  Rotate GameObject over time
//  https://stackoverflow.com/questions/37586407/rotate-gameobject-over-time