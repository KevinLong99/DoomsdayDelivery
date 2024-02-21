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

    void Start()
    {
        configurableJoint = GetComponent<ConfigurableJoint>();
        startingPosition = configurableJoint.transform.localPosition.y;
        lowerLimit = configurableJoint.linearLimit.limit * -1; // Assuming symmetric limits
        upperLimit = configurableJoint.linearLimit.limit;
    }

    void FixedUpdate()
    {
        currentPosition = configurableJoint.transform.localPosition.y;

        // Assuming you're using the joint's linear limit for the y-axis
        //Debug.Log(currentPosition + "\n" + lowerLimit + "\n" + upperLimit + "\n");


        // Reached Lower Limit
        //if (Mathf.Abs(currentPosition - lowerLimit) > positionThreshold)    //<--problem
        
        //Debug.Log("CURRENTPOS" + currentPosition);
        //goes into this if statement on first pull, and doesnt meet the requirements again EVER, so it never re-enters
        //THIS ISSUE ONLY HAPPENS FOR THE RIGHT SIDE LEVER!!!!
        if (currentPosition < (startingPosition + lowerLimit + 0.075f) && currentPosition > (startingPosition + lowerLimit - 0.075f))
        {

            //Debug.Log("CURRENTPOS--INSIDE!!!!" + currentPosition);
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
        }
        // Reached Upper Limit
        //else if (Mathf.Abs(currentPosition - upperLimit) < positionThreshold)   //<--problem
        if (currentPosition < (startingPosition + upperLimit + 0.05f) && currentPosition > (startingPosition + upperLimit - 0.05f))
        {
            if (jointLimitState != JointLimitState.Upper)
            {
                Debug.Log("upperlimitINVOKE");
                OnUpperLimitReached.Invoke();
            }
                

            jointLimitState = JointLimitState.Upper;
        }
        // No Limit reached
        //else
        else if (currentPosition > (startingPosition - 0.05f) && currentPosition < (startingPosition + 0.05f))
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
