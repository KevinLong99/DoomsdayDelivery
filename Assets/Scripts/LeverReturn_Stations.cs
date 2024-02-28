using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LeverReturn_Stations : MonoBehaviour
{
    public bool isReturning = false;

    //private Vector3 startingPos;
    private Quaternion startRot;
    void Start()
    {
        //startingPos = this.transform.position;
        startRot = this.transform.rotation;
    }

    // Call this method when the lever is released
    public void ReleaseLever()
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

        Quaternion currentRot = this.transform.rotation;

        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            this.transform.rotation = Quaternion.Lerp(currentRot, startRot, counter / duration);
            yield return null;
        }
        isReturning = false;
    }
}
