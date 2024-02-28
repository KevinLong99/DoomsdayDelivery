using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LeverReturn_Stations : MonoBehaviour
{
    public float returnSpeed; // Units per second, adjust as necessary
    private float originalPosition;
    private Rigidbody rb;
    public bool isReturning = false;
    private XRGrabInteractable grabInteractable;

    public ConfigurableJointListener jointListener;

    private Vector3 startingPos;
    private Quaternion startRot;
    void Start()
    {
        startingPos = this.transform.position;
        startRot = this.transform.rotation;
        originalPosition = this.transform.position.y; // Ensure this is set when the lever is in the up position
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    void FixedUpdate()
    {
        /*
        if (isReturning)
        {
            // Move towards the original position at a constant speed
            Vector3 nextPosition = Vector3.MoveTowards(this.transform.position, startingPos, returnSpeed * Time.deltaTime);
            rb.MovePosition(nextPosition);

            //this.transform.position = originalPosition;
            if (this.transform.position.y < (originalPosition - 0.05f) && this.transform.position.y > (originalPosition + 0.05f))
            {
                isReturning = false;
                this.transform.position = startingPos;
            }
        }
        */
    }

    // Call this method when the lever is released
    public void ReleaseLever()
    {
        isReturning = false;     //might need this to be false
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
