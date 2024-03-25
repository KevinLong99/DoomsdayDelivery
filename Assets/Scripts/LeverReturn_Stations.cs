using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LeverReturn_Stations : MonoBehaviour
{
    public bool isReturning = false;

    private Quaternion startRot;
    void Start()
    {
        startRot = this.transform.localRotation;
    }

    // Call this method when the lever is released
    public void ResetLever()
    {
        isReturning = false;
        StartCoroutine(ResetOriginalOrientation());
    }

    IEnumerator ResetOriginalOrientation()
    {
        float duration = 0.5f;
        if (isReturning)
        {
            yield break;
        }
        isReturning = true;

        Quaternion currentRot = this.transform.localRotation;

        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            this.transform.localRotation = Quaternion.Lerp(currentRot, startRot, counter / duration);
            yield return null;
        }
        this.transform.localRotation = startRot;
        isReturning = false;
    }

}

//dealing with rotation (instead of local rotation) makes the lever look ugly while resetting
//try using localrotation in the future
