using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ConfigurableJointListener : MonoBehaviour
{
    public float positionThreshold = 0.01f; // Threshold to trigger if we reached the limit
    public JointLimitState jointLimitState = JointLimitState.None;

    public UnityEvent OnLowerLimitReached;
    public UnityEvent OnUpperLimitReached;

    public enum JointLimitState { Lower, Upper, None }
    private ConfigurableJoint configurableJoint;

    float currentPosition;
    float startingPosition;

    public bool isPulled;

    public Rotate_Me_Parent rotateParentScript;
    public LeverReturn leverReturnScript;

    public Haptic_Chair_Controller hapChair_Script;

    void Start()
    {
        configurableJoint = GetComponent<ConfigurableJoint>();
        startingPosition = configurableJoint.transform.localPosition.y;
    }

    void FixedUpdate()
    {
        currentPosition = configurableJoint.transform.localPosition.y;

        // Reached Lower Limit
        if (currentPosition < (startingPosition - 0.2f))
        {
            if (jointLimitState != JointLimitState.Lower)
            {
                if (isPulled == false && rotateParentScript.rotating == false && hapChair_Script.doRotateSphere == false)
                {
                    OnLowerLimitReached.Invoke();
                    isPulled = true;
                }  
            }
            jointLimitState = JointLimitState.Lower;
        } else if (currentPosition > (startingPosition - 0.05f) && currentPosition < (startingPosition + 0.05f))
        {
            if (jointLimitState != JointLimitState.Upper)
            {
                isPulled = false;
                OnUpperLimitReached.Invoke();
                leverReturnScript.isReturning = false;
            }
            jointLimitState= JointLimitState.Upper;
        } else
        {

            jointLimitState = JointLimitState.None;
        }

    }

    // Call these methods if you want to manually trigger the events
    public void TriggerLowerLimitReached()
    {
        Debug.Log("lower");
        OnLowerLimitReached.Invoke();
    }

    public void TriggerUpperLimitReached()
    {
        Debug.Log("upper");
        OnUpperLimitReached.Invoke();
    }
}
