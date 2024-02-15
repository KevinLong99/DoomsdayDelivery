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

    void Start()
    {
        configurableJoint = GetComponent<ConfigurableJoint>();
    }

    void FixedUpdate()
    {
        // Assuming you're using the joint's linear limit for the y-axis
        float currentPosition = configurableJoint.transform.localPosition.y;
        float lowerLimit = configurableJoint.linearLimit.limit * -1; // Assuming symmetric limits
        float upperLimit = configurableJoint.linearLimit.limit;

        // Reached Lower Limit
        if (Mathf.Abs(currentPosition - lowerLimit) < positionThreshold)
        {
            if (jointLimitState != JointLimitState.Lower)
                OnLowerLimitReached.Invoke();

            jointLimitState = JointLimitState.Lower;
        }
        // Reached Upper Limit
        else if (Mathf.Abs(currentPosition - upperLimit) < positionThreshold)
        {
            if (jointLimitState != JointLimitState.Upper)
                OnUpperLimitReached.Invoke();

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
        OnLowerLimitReached.Invoke();
    }

    public void TriggerUpperLimitReached()
    {
        OnUpperLimitReached.Invoke();
    }

    public void spawnStuff()
    {
        mySpawner.spawn();
    }
}
