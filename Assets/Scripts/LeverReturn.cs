using UnityEngine;

public class LeverReturn : MonoBehaviour
{
    public float returnSpeed = 2.0f; // Speed of return, adjustable in the inspector
    private Quaternion originalRotation;
    private Rigidbody rb;
    private bool isReturning = false;

    void Start()
    {
        // Store the original rotation of the lever
        originalRotation = transform.localRotation;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (isReturning)
        {
            // Use Rigidbody.MoveRotation to move the lever back to the original rotation
            Quaternion newRotation = Quaternion.Lerp(rb.rotation, originalRotation, returnSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(newRotation);

            // Optionally, stop the returning if it's "close enough" to the original rotation
            if (Quaternion.Angle(rb.rotation, originalRotation) < 0.1f)
            {
                rb.MoveRotation(originalRotation); // Snap to the exact rotation
                isReturning = false;
            }
        }
    }

    // Call this method when the lever is released
    public void ReleaseLever()
    {
        isReturning = true;
    }
}