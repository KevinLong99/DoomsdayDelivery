using UnityEngine;

public class LeverReturn : MonoBehaviour
{
    public float returnSpeed = 2.0f; // Speed of return, adjustable in the inspector
    private Quaternion originalRotation;
    private bool isReturning = false;

    void Start()
    {
        // Store the original rotation of the lever
        originalRotation = transform.localRotation;
    }

    void Update()
    {
        // If the lever has been released, begin returning it to its original position
        if (isReturning)
        {
            // Lerp the rotation back to the original rotation at the specified return speed
            transform.localRotation = Quaternion.Lerp(transform.localRotation, originalRotation, Time.deltaTime * returnSpeed);

            // Optionally, you can stop the returning if it's "close enough" to the original rotation
            if (Quaternion.Angle(transform.localRotation, originalRotation) < 0.1f)
            {
                transform.localRotation = originalRotation;
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