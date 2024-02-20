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


    void Start()
    {
        configurableJoint = GetComponent<ConfigurableJoint>();
        currentPosition = configurableJoint.transform.localPosition.y;
        lowerLimit = configurableJoint.linearLimit.limit * -1; // Assuming symmetric limits
        upperLimit = configurableJoint.linearLimit.limit;
    }

    void FixedUpdate()
    {
        // Assuming you're using the joint's linear limit for the y-axis
        

        // Reached Lower Limit
        if (Mathf.Abs(currentPosition - lowerLimit) < positionThreshold)
        {
            if (jointLimitState != JointLimitState.Lower)
            {
                //this happens upon start and ONLY on start.
                Debug.Log("lowerlimitINVOKE");
                OnLowerLimitReached.Invoke();
            }
                

            jointLimitState = JointLimitState.Lower;
        }
        // Reached Upper Limit
        else if (Mathf.Abs(currentPosition - upperLimit) < positionThreshold)
        {
            if (jointLimitState != JointLimitState.Upper)
            {
                Debug.Log("upperlimitINVOKE");
                OnUpperLimitReached.Invoke();
            }
                

            jointLimitState = JointLimitState.Upper;
        }
        // No Limit reached
        else
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
