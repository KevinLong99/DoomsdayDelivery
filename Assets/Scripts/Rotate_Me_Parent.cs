using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Me_Parent : MonoBehaviour
{
    //ROTATE VALUES:    0   120 240 

    public bool rotating = false;
    public GameObject objectToRotate;

    //Station Rotation Values
    private Quaternion rotation1 = Quaternion.Euler(new Vector3(0, -60, 0));
    private Quaternion rotation2 = Quaternion.Euler(new Vector3(0, -180, 0));
    private Quaternion rotation3 = Quaternion.Euler(new Vector3(0, -300, 0));

    public int moveValue = 0;

    private bool nickRotate = false;

    //list of levers that need ConnectedBodies to be modified
    [SerializeField] GameObject[] leverConnectedBodies;

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
            if (nickRotate == false)
            {
                RotateToStationTwo();
                nickRotate = true;
            }
        }
        else if(moveValue == 3)
        {
            if (nickRotate == false)
            {
                RotateToStationThree();
                nickRotate = true;
            }
        }
        else
        {
            //do nothing. Do not rotate
        }
    }

    //RotateLever(objectToRotate, rotation3, 1f);
    public void RotateToStationOne()
    {
        for (int i = 0; i < leverConnectedBodies.Length; i++)
        {
            leverConnectedBodies[i].GetComponent<HingeJointListener>().enabled = false;
            leverConnectedBodies[i].GetComponent<RotateLever>().RotateLeverCall(objectToRotate, rotation3, 1f);
        }
        StartCoroutine(rotateObject(objectToRotate, rotation3, 1f));
    }
    public void RotateToStationTwo()
    {
        for (int i = 0; i < leverConnectedBodies.Length; i++)
        {
            leverConnectedBodies[i].GetComponent<HingeJointListener>().enabled = false;
            leverConnectedBodies[i].GetComponent<RotateLever>().RotateLeverCall(objectToRotate, rotation2, 1f);
        }
        StartCoroutine(rotateObject(objectToRotate, rotation2, 1f));
    }
    public void RotateToStationThree()
    {
        for (int i = 0; i < leverConnectedBodies.Length; i++)
        {
            leverConnectedBodies[i].GetComponent<HingeJointListener>().enabled = false;
            leverConnectedBodies[i].GetComponent<RotateLever>().RotateLeverCall(objectToRotate, rotation1, 1f);
        }
        StartCoroutine(rotateObject(objectToRotate, rotation1, 1f));
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
        for (int i = 0; i < leverConnectedBodies.Length; i++)
        {
            leverConnectedBodies[i].GetComponent<HingeJointListener>().enabled = true;
        }
    }
}


//Script Credits:
//  Rotate GameObject over time
//  https://stackoverflow.com/questions/37586407/rotate-gameobject-over-time