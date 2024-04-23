using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class MedkitComplete : MonoBehaviour
{
    public Rotate_Me_Parent rotateParentScript;
    public NewObjectCounter objectCounterMedbox;
    public PlaySounds soundScript;
    [SerializeField] private Animator pizzaPaddleAnimator;
    [SerializeField] private Animator ovenDoorAnimator;
    [SerializeField] private GameObject[] conveyorColliders;

    public Typewriter_UI typewriter_script_medkit;


    public void SendMedkitToOven()
    {
        objectCounterMedbox = GameObject.Find("Object Detector").GetComponent<NewObjectCounter>();

        if (objectCounterMedbox.GetNumTotalItems() > 0 && rotateParentScript.medKitisCompleted == false)        //change to zero
        {
            //if there is stuff in the medkit, then button can be activated and box is pushed into oven
            rotateParentScript.medKitisCompleted = true;
            //play box going into oven animation
            pizzaPaddleAnimator.Play("paddle_launch");
            ovenDoorAnimator.Play("Oven_Door_Open");
            soundScript.PlayMechDoor1();

            //deactivate colliders
            for (int i = 0; i < conveyorColliders.Length; i++)
            {
                conveyorColliders[i].gameObject.SetActive(false);
            }


            objectCounterMedbox.PlayStation2ExitAnimation();
            StartCoroutine(WaitForBoxToEnterOven());

        } else
        {
            typewriter_script_medkit.StartTypewriterView("ERROR! No supplies are in the medkit!");
        }
    }

    IEnumerator WaitForBoxToEnterOven()
    {
        yield return new WaitForSeconds(1.75f);
        ovenDoorAnimator.Play("Oven_Door_Close");
        soundScript.PlayMechDoor3();

        //activate colliders
        for (int j = 0; j < conveyorColliders.Length; j++)
        {
            conveyorColliders[j].gameObject.SetActive(true);
        }
    }

}
