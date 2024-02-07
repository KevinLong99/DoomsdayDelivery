using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LeverReturn : MonoBehaviour
{
    public float returnSpeed = 0.1f; // Units per second, adjust as necessary
    private Vector3 originalPosition;
    private Rigidbody rb;
    private bool isReturning = false;
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        originalPosition = transform.position; // Ensure this is set when the lever is in the up position
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    void FixedUpdate()
    {
        if (isReturning)
        {
            // Move towards the original position at a constant speed
            Vector3 nextPosition = Vector3.MoveTowards(this.transform.position, originalPosition, returnSpeed * Time.deltaTime);
            rb.MovePosition(originalPosition);

            this.transform.position = originalPosition;


            
            
            

        }
    }

    // Call this method when the lever is released
    public void ReleaseLever()
    {
        isReturning = true;
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