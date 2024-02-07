using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LeverReturn : MonoBehaviour
{
    public float returnSpeed = 2.0f; // Speed of return, adjustable in the inspector
    private Vector3 originalPosition;
    private Rigidbody rb;
    private bool isReturning = false;

    void Start()
    {
        // Store the original position of the lever
        originalPosition = transform.localPosition;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (isReturning)
        {
            // Calculate the new position using Lerp, moving only on the Y axis
            Vector3 newPosition = Vector3.Lerp(rb.position, originalPosition, returnSpeed * Time.fixedDeltaTime);

            // Apply the new position
            rb.MovePosition(newPosition);

            // Check if the lever is close enough to the original position to stop returning
            if (Vector3.Distance(newPosition, originalPosition) < 0.01f)
            {
                rb.MovePosition(originalPosition); // Snap to the exact original position
                isReturning = false;
                // Enable the interactable component so it can be grabbed again
                GetComponent<XRGrabInteractable>().enabled = true;
            }
        }
    }

    // Call this method when the lever is released
    public void ReleaseLever()
    {
        isReturning = true;
        // Disable the interactable component to prevent grabbing while returning
        GetComponent<XRGrabInteractable>().enabled = false;
    }
}