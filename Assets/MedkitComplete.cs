using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class MedkitComplete : MonoBehaviour
{
    public Rotate_Me_Parent rotateParentScript;
    public NewObjectCounter objectCounterMedbox;
    [SerializeField] private Animator pizzaPaddleAnimator;
    [SerializeField] private Animator ovenDoorAnimator;


    public void Placeholder()
    {
        objectCounterMedbox = GameObject.Find("Object Detector").GetComponent<NewObjectCounter>();

        if (objectCounterMedbox.GetNumTotalItems() > -1)        //change to zero
        {
            //if there is stuff in the medkit, then button can be activated and box is pushed into oven
            rotateParentScript.medKitisCompleted = true;
            //play box going into oven animation
            pizzaPaddleAnimator.Play("paddle_launch");
            ovenDoorAnimator.Play("Oven_Door_Open");

            objectCounterMedbox.PlayStation2ExitAnimation();
            StartCoroutine(WaitForBoxToEnterOven());

        } else
        {
            Debug.Log("ERROR! No supplies are in the medkit!");
        }
    }

    IEnumerator WaitForBoxToEnterOven()
    {
        yield return new WaitForSeconds(1.75f);
        ovenDoorAnimator.Play("Oven_Door_Close");
    }

}
