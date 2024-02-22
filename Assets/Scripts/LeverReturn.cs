using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LeverReturn : MonoBehaviour
{
    public float returnSpeed; // Units per second, adjust as necessary
    private float originalPosition;
    private Rigidbody rb;
    public bool isReturning = false;
    private XRGrabInteractable grabInteractable;

    public ConfigurableJointListener jointListener;

    private Vector3 startingPos;

    void Start()
    {
        startingPos = this.transform.position;
        originalPosition = this.transform.position.y; // Ensure this is set when the lever is in the up position
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    void FixedUpdate()
    {
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




    }

    // Call this method when the lever is released
    public void ReleaseLever()
    {
        isReturning = true;
        jointListener.isPulled = false;
        Debug.Log("jointlistener is false");

        //grabInteractable.enabled = false; // Disable interaction while the lever is returning
    }


}

/*
if (Mathf.Abs(this.transform.position.y - originalPosition.y) < 0.01f)
            {
                //grabInteractable.enabled = true;
                isReturning = false;
            } else
            {
                isReturning = true;
            }
 */