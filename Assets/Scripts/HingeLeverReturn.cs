using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HingeLeverReturn : MonoBehaviour
{
    public float returnSpeed; // Speed at which the lever returns, in degrees per second
    private HingeJoint returnHingeJoint;
    private bool isReturning = false;
    private float originalAngle = 90f; // Original angle you want to return to

    void Start()
    {
        returnHingeJoint = GetComponent<HingeJoint>();
        // Optional: Set up the hinge joint limits here if needed
    }

    void FixedUpdate()
    {
        if (isReturning)
        {
            // Calculate the current angle and the desired return angle
            Vector3 currentRotation = returnHingeJoint.transform.localEulerAngles;
            float currentAngle = currentRotation.x; // The axis the hinge is turning on!!!! Needs to be CORRECT!!!!!
            float targetAngle = Mathf.MoveTowardsAngle(currentAngle, originalAngle, returnSpeed * Time.fixedDeltaTime);

            // Apply the new angle
            returnHingeJoint.transform.localEulerAngles = new Vector3(currentRotation.x, currentRotation.y, targetAngle);

            // Stop returning if the target angle is reached
            if (Mathf.Abs(targetAngle - originalAngle) < 1f)
            {
                isReturning = false;
            }
        }
    }

    // This method should be called by the XRGrabInteractable when the lever is released
    public void ReleaseLever()
    {
        isReturning = true;
        Debug.Log("HingeReturns");
    }
}