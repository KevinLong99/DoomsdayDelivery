using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLever : MonoBehaviour
{
    private HingeJoint hinge;
    private Rigidbody rbHingeConnectedBody;
    private bool rotatingHinge = false;
    private Rigidbody leverRb;
    private Vector3 anchorYValue;
    public Vector3 conAnchor;
    private float anchorMinLim;
    private float anchorMaxLim;
    private float anchorBounceVel;
    private JointLimits oldJointLimits;
    private Vector3 axisOrientation;
    private float springDamper;

    void Start()
    {
        hinge = GetComponent<HingeJoint>();
        rbHingeConnectedBody = hinge.connectedBody;
        RemoveHingeConnectedBody();
        leverRb = GetComponent<Rigidbody>();

        anchorYValue = hinge.anchor;
        conAnchor = hinge.connectedAnchor;
        anchorMinLim = hinge.limits.min;
        anchorMaxLim = hinge.limits.max;
        anchorBounceVel = hinge.limits.bounceMinVelocity;
        axisOrientation = hinge.axis;
        springDamper = hinge.spring.damper;

        oldJointLimits = hinge.limits;


    }

    public void DestroyHingeAndRigidbody()
    {
        Destroy(hinge);
        leverRb.isKinematic = true;
    }

    public void AddHingeAndRigidbody()
    {
        hinge = this.gameObject.AddComponent<HingeJoint>();
        JointLimits secondJointLims = hinge.limits;
        JointSpring twoJointSpring = hinge.spring;

        secondJointLims.min = -45;
        secondJointLims.max = 42;
        secondJointLims.bounceMinVelocity = 0.5f;

        twoJointSpring.damper = springDamper;

        hinge.limits = secondJointLims;
        hinge.spring = twoJointSpring;

        this.gameObject.GetComponent<HingeJoint>().anchor = anchorYValue;
        this.gameObject.GetComponent<HingeJoint>().connectedAnchor = conAnchor;
        this.gameObject.GetComponent<HingeJoint>().useLimits = true;
        this.gameObject.GetComponent<HingeJoint>().useSpring = true;
        this.gameObject.GetComponent<HingeJoint>().axis = axisOrientation;

        leverRb.isKinematic = false;

        this.gameObject.GetComponent<HingeJointListener>().RetrieveHingeJoint();

    }

    public void RemoveHingeConnectedBody()
    {
        hinge.connectedBody = null;
        
    }

    public void AttachHingeConnectedBody()
    {

        hinge.connectedBody = rbHingeConnectedBody;
    }

    public void RotateLeverCall(GameObject gameObjectToMove, Quaternion newRot, float duration)
    {
        //AttachHingeConnectedBody();
        StartCoroutine(RotateLeverCoroutine(gameObjectToMove, newRot, duration));
    }

    IEnumerator RotateLeverCoroutine(GameObject gameObjectToMove, Quaternion newRot, float duration)
    {
        if (rotatingHinge)
        {
            yield break;
        }
        rotatingHinge = true;

        Quaternion currentRot = this.transform.rotation;

        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            this.transform.rotation = Quaternion.Lerp(currentRot, newRot, counter / duration);
            yield return null;
        }
        rotatingHinge = false;
        //RemoveHingeConnectedBody();
    }
}
