using UnityEngine;

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
            // Use Rigidbody.MovePosition to move the lever back to the original position
            Vector3 newPosition = Vector3.Lerp(rb.position, originalPosition, returnSpeed * Time.fixedDeltaTime);
            rb.MovePosition(new Vector3(rb.position.x, newPosition.y, rb.position.z));

            // Optionally, stop the returning if it's "close enough" to the original position
            if (Mathf.Abs(rb.position.y - originalPosition.y) < 0.01f) // Threshold value can be adjusted
            {
                rb.MovePosition(new Vector3(rb.position.x, originalPosition.y, rb.position.z)); // Snap to the exact position
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