using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HingeLeverReturn : MonoBehaviour
{
    public float returnSpeed = 5f; // Degrees per second, adjust as necessary
    private float originalRotation;
    private HingeJoint hingeJoint;
    private bool isReturning = false;
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        hingeJoint = GetComponent<HingeJoint>();
        originalRotation = hingeJoint.transform.localEulerAngles.z; // Assuming the lever's rotation axis is Z
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectExited.AddListener(ReleaseLever); // Add listener for when the lever is released
    }

    void FixedUpdate()
    {
        if (isReturning)
        {
            // Calculate the current angle and the desired return angle
            float currentAngle = hingeJoint.transform.localEulerAngles.z;
            float angleToReturn = Mathf.MoveTowardsAngle(currentAngle, originalRotation, returnSpeed * Time.deltaTime);

            // Apply the new angle
            hingeJoint.transform.localRotation = Quaternion.Euler(0, 0, angleToReturn);

            // Check if the lever has returned to its original position
            if (Mathf.Abs(angleToReturn - originalRotation) < 0.01f)
            {
                isReturning = false; // Stop returning when the original position is reached
                //grabInteractable.enabled = true; // Optionally re-enable interactions
            }
        }
    }

    // This method is called when the lever is released
    public void ReleaseLever(SelectExitEventArgs arg)
    {
        isReturning = true;
        //grabInteractable.enabled = false; // Disable interaction while the lever is returning
    }
}