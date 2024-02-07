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
        originalPosition = transform.localPosition; // Ensure this is set when the lever is in the up position
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        InvokeRepeating(nameof(LogOriginalPosition), 0f, 1f);
    }

    void FixedUpdate()
    {
        if (isReturning)
        {
            // Move towards the original position at a constant speed
            Vector3 nextPosition = Vector3.MoveTowards(rb.position, originalPosition, returnSpeed * Time.fixedDeltaTime);
            rb.MovePosition(nextPosition);

            // Check if the lever has reached the original position
            if (Vector3.Distance(rb.position, originalPosition) < 0.001f) // Small threshold to account for floating-point imprecision
            {
                //grabInteractable.enabled = true; // Re-enable the interactable component
                rb.MovePosition(originalPosition); // Snap to the exact original position
                isReturning = false;
            }
        }
    }

    // Call this method when the lever is released
    public void ReleaseLever()
    {
        isReturning = true;
        //grabInteractable.enabled = false; // Disable interaction while the lever is returning
    }

    private void LogOriginalPosition()
    {
        Debug.Log("Original Position: " + originalPosition);
    }
}