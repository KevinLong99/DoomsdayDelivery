using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System;

public class HingeJointListener : MonoBehaviour
{
    //angle threshold to trigger if we reached limit
    public float angleBetweenThreshold = 1f;
    //State of the hinge joint : either reached min or max or none if in between
    public HingeJointState hingeJointState = HingeJointState.None;

    //Event called on min reached
    public UnityEvent OnMinLimitReached;
    //Event called on max reached
    public UnityEvent OnMaxLimitReached;

    public enum HingeJointState { Min, Max, None }
    private HingeJoint hinge;

    private Material unActivatedMaterial;

    // Start is called before the first frame update
    void Start()
    {
        hinge = GetComponent<HingeJoint>();
        try 
        { 
            unActivatedMaterial = this.GetComponent<MeshRenderer>().material; 
        } catch (Exception ex)
        {
            Debug.LogException(ex, this);
        }

        
    }

    private void FixedUpdate()
    {
        if (hinge != null)
        {
            float angleWithMinLimit = Mathf.Abs(hinge.angle - hinge.limits.min);
            float angleWithMaxLimit = Mathf.Abs(hinge.angle - hinge.limits.max);

            //Reached Min
            if (angleWithMinLimit < angleBetweenThreshold)
            {
                if (hingeJointState != HingeJointState.Min)
                    OnMinLimitReached.Invoke();

                hingeJointState = HingeJointState.Min;
            }
            //Reached Max
            else if (angleWithMaxLimit < angleBetweenThreshold)
            {
                if (hingeJointState != HingeJointState.Max)
                    OnMaxLimitReached.Invoke();

                hingeJointState = HingeJointState.Max;
            }
            //No Limit reached
            else
            {
                hingeJointState = HingeJointState.None;
                this.GetComponent<MeshRenderer>().material = unActivatedMaterial;
            }
        }       

    }


    public void RetrieveHingeJoint()
    {
        hinge = GetComponent<HingeJoint>();
    }
}