using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LeverReturn : MonoBehaviour
{
    public float returnSpeed = 0.1f; // Units per second, adjust as necessary
    public Transform returnPosition; // Assign the target position in the inspector
    private Rigidbody rb;
    private bool isReturning = false;
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    void FixedUpdate()
    {
        if (isReturning && returnPosition != null)
        {
            // Move towards the target position at a constant speed
            Vector3 nextPosition = Vector3.MoveTowards(rb.position, returnPosition.position, returnSpeed * Time.fixedDeltaTime);
            rb.MovePosition(nextPosition);

            // Check if the lever has reached the target position
            if (Vector3.Distance(rb.position, returnPosition.position) < 0.001f) // Small threshold to account for floating-point imprecision
            {
                grabInteractable.enabled = true; // Re-enable the interactable component
                rb.MovePosition(returnPosition.position); // Snap to the exact target position
                isReturning = false;
            }
        }
    }

    // Call this method when the lever is released
    public void ReleaseLever()
    {
        isReturning = true;
        grabInteractable.enabled = false; // Disable interaction while the lever is returning
    }
}