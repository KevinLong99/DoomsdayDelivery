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

    //spawner

    public Spawner mySpawner;

    float currentPosition, lowerLimit, upperLimit;
    //lowerLimit and upperLimit never change.

    float startingPosition;

    public bool isPulled;

    public Rotate_Me_Parent rotateParentScript;
    public LeverReturn leverReturnScript;

    void Start()
    {
        configurableJoint = GetComponent<ConfigurableJoint>();
        startingPosition = configurableJoint.transform.localPosition.y;
        lowerLimit = configurableJoint.linearLimit.limit * -1; // Assuming symmetric limits
        upperLimit = configurableJoint.linearLimit.limit + 1;
    }

    void FixedUpdate()
    {
        currentPosition = configurableJoint.transform.localPosition.y;

        // Reached Lower Limit
        //if (Mathf.Abs(currentPosition - lowerLimit) > positionThreshold)    //<--problem
        if (currentPosition < (startingPosition + lowerLimit + 0.05f) && currentPosition > (startingPosition + lowerLimit - 0.05f))
        {
            if (jointLimitState != JointLimitState.Lower)
            {
                if (isPulled == false && rotateParentScript.rotating == false)
                {
                    Debug.Log("lowerlimitINVOKE");
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
                Debug.Log("upperlimitINVOKE");
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

    public void spawnStuff()
    {
        mySpawner.spawn();
    }
}
