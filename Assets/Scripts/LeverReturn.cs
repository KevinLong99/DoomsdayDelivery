using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LeverReturn : MonoBehaviour
{
    public float returnSpeed = 0.1f; // Units per second, adjust as necessary
    private Vector3 originalPosition;
    private Rigidbody rb;
    private bool isReturning = false;

    void Start()
    {
        originalPosition = transform.localPosition;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Temporary input to test the movement
        if (Input.GetKey(KeyCode.R))
        {
            TestMoveLever();
        }
    }

    void FixedUpdate()
    {
        if (isReturning)
        {
            // Move towards the original position at a constant speed
            Vector3 nextPosition = Vector3.MoveTowards(rb.position, originalPosition, returnSpeed * Time.fixedDeltaTime);
            rb.MovePosition(nextPosition);

            // Check if the lever has reached the original position
            if (nextPosition == originalPosition)
            {
                isReturning = false;
                // Enable interaction when the lever returns to the original position
                GetComponent<XRGrabInteractable>().enabled = true;
            }
        }
    }

    // Call this method when the lever is released
    public void ReleaseLever()
    {
        isReturning = true;
        // Disable interaction while the lever is returning
        GetComponent<XRGrabInteractable>().enabled = false;
    }

    // Test movement method
    private void TestMoveLever()
    {
        rb.MovePosition(rb.position + new Vector3(0, -0.01f, 0)); // Move down on Y axis
    }
}