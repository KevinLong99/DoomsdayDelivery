using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Me_Parent : MonoBehaviour
{
    public bool rotating = false;
    public GameObject objectToRotate;

    //Station Rotation Values
    private Quaternion rotation1 = Quaternion.Euler(new Vector3(0, -120, 0));
    private Quaternion rotation2 = Quaternion.Euler(new Vector3(0, 0, 0));
    private Quaternion rotation3 = Quaternion.Euler(new Vector3(0, -240, 0));

    [SerializeField] int moveValue = 1;

    //list of levers that need ConnectedBodies to be modified
    [SerializeField] GameObject[] leverConnectedBodies;

    public void RotateLeft()
    {
        //deal with levers first
        for (int i = 0; i < leverConnectedBodies.Length; i++)
        {
            //leverConnectedBodies[i].GetComponent<HingeJointListener>().enabled = false;
            // leverConnectedBodies[i].GetComponent<RotateLever>().RotateLeverCall(objectToRotate, rotation3, 1f);
            leverConnectedBodies[i].GetComponent<RotateLever>().AttachHingeConnectedBody();
        }

        if (moveValue == 1)
        {
            RotateToStationTwo();
            moveValue = 2;
        }
        else if (moveValue == 2)
        {
            RotateToStationThree();
            moveValue = 3;
        }
        else if (moveValue == 3)
        {
            RotateToStationOne();
            moveValue = 1;
        }
    }

    public void RotateRight()
    {
        //deal with levers first
        for (int i = 0; i < leverConnectedBodies.Length; i++)
        {
            //leverConnectedBodies[i].GetComponent<HingeJointListener>().enabled = false;
            // leverConnectedBodies[i].GetComponent<RotateLever>().RotateLeverCall(objectToRotate, rotation3, 1f);
            leverConnectedBodies[i].GetComponent<RotateLever>().AttachHingeConnectedBody();
        }

        if (moveValue == 1)
        {
            RotateToStationThree();
            moveValue = 3;
        }
        else if (moveValue == 3)
        {
            RotateToStationTwo();
            moveValue = 2;
        }
        else if (moveValue == 2)
        {
            RotateToStationOne();
            moveValue = 1;
        }
    }

    private void RotateToStationOne()
    {
        StartCoroutine(rotateObject(objectToRotate, rotation1, 1f));
    }
    private void RotateToStationTwo()
    {
        StartCoroutine(rotateObject(objectToRotate, rotation2, 1f));
    }
    private void RotateToStationThree()
    {
        StartCoroutine(rotateObject(objectToRotate, rotation3, 1f));
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

        //end with levers
        for (int i = 0; i < leverConnectedBodies.Length; i++)
        {
            leverConnectedBodies[i].GetComponent<RotateLever>().RemoveHingeConnectedBody();
        }
    }
}


//Script Credits:
//  Rotate GameObject over time
//  https://stackoverflow.com/questions/37586407/rotate-gameobject-over-time