using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class MedkitComplete : MonoBehaviour
{

    public Rotate_Me_Parent rotateParentScript;
    public ObjectCounter objectCounterMedbox;
    public void Placeholder()
    {
        objectCounterMedbox = GameObject.Find("Object Detector").GetComponent<ObjectCounter>();

        rotateParentScript.medKitisCompleted = true;
        objectCounterMedbox.PlayStation2ExitAnimation();

        if (objectCounterMedbox.GetNumTotalItems() > 0)
        {
            //if there is stuff in the medkit, then button can be activated and box is pushed into oven
            rotateParentScript.medKitisCompleted = true;
            //play box going into oven animation
            objectCounterMedbox.PlayStation2ExitAnimation();

        } else
        {
            Debug.Log("ERROR! No supplies are in the medkit!");
        }
        Debug.Log("placeholder for button medkit complete!");
        
    }

}
